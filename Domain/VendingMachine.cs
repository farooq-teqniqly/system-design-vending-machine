using Domain.EventArgs;
using Domain.Inventory;
using Domain.Money;
using Domain.States;

namespace Domain;
/// <summary>
/// Represents a vending machine that manages item selection, payment processing, 
/// and item dispensing functionalities.
/// </summary>
/// <remarks>
/// This class encapsulates the core behavior of a vending machine, including 
/// managing inventory, handling monetary transactions, and raising events for 
/// various states and actions. It operates in conjunction with state objects 
/// implementing the <see cref="IState"/> interface to handle specific behaviors 
/// based on the current state of the machine.
/// </remarks>
/// <example>
/// To use the vending machine:
/// <code>
/// var inventoryManager = new InventoryManager();
/// var moneyManager = new MoneyManager();
/// var vendingMachine = new VendingMachine(inventoryManager, moneyManager);
/// vendingMachine.SelectItem("item123");
/// vendingMachine.InsertCash(100);
/// vendingMachine.DispenseItem();
/// </code>
/// </example>
public sealed class VendingMachine
{
    private readonly IInventoryManager _inventoryManager;
    private readonly IMoneyManager _moneyManager;

    /// <summary>
    /// Gets the currently selected item in the vending machine.
    /// </summary>
    /// <value>
    /// The <see cref="Item"/> that has been selected by the user. If no item is selected, 
    /// this property will return a default or null-like item (e.g., <see cref="NullItem"/>).
    /// </value>
    /// <remarks>
    /// This property reflects the item chosen by the user for purchase. It is updated 
    /// when the <see cref="SelectItem(string)"/> method is called. The selected item 
    /// remains set until the transaction is completed, canceled, or another item is selected.
    /// </remarks>
    /// <example>
    /// To retrieve the currently selected item:
    /// <code>
    /// var selectedItem = vendingMachine.SelectedItem;
    /// Console.WriteLine($"Selected Item: {selectedItem.Name}, Price: {selectedItem.Price}");
    /// </code>
    /// </example>
    public Item SelectedItem { get; private set; }

    /// <summary>
    /// Occurs when the vending machine raises a message or notification.
    /// </summary>
    /// <remarks>
    /// This event can be used to handle various notifications or messages generated by the vending machine,
    /// such as item selection, payment completion, or transaction cancellation.
    /// </remarks>
    /// <example>
    /// <code>
    /// var vendingMachine = new VendingMachine(inventoryManager, moneyManager);
    /// vendingMachine.OnMessageRaised += (sender, args) =>
    /// {
    ///     Console.WriteLine($"Message: {args.Message}");
    /// };
    /// </code>
    /// </example>
    public event EventHandler<VendingMachineEventArgs>? OnMessageRaised;

    /// <summary>
    /// Gets or sets the current operational state of the vending machine.
    /// </summary>
    /// <remarks>
    /// The state determines the behavior of the vending machine and transitions 
    /// dynamically based on user interactions or internal processes. 
    /// Implementations of <see cref="Domain.States.IState"/> define the specific 
    /// actions available in each state.
    /// </remarks>
    public IState CurrentState { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="VendingMachine"/> class.
    /// </summary>
    /// <param name="inventoryManager">
    /// The inventory manager responsible for managing the items in the vending machine.
    /// </param>
    /// <param name="moneyManager">
    /// The money manager responsible for handling cash transactions in the vending machine.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="inventoryManager"/> or <paramref name="moneyManager"/> is <c>null</c>.
    /// </exception>
    public VendingMachine(IInventoryManager inventoryManager, IMoneyManager moneyManager)
    {
        ArgumentNullException.ThrowIfNull(inventoryManager);
        ArgumentNullException.ThrowIfNull(moneyManager);

        _inventoryManager = inventoryManager;
        _moneyManager = moneyManager;
        CurrentState = new IdleState(this);
        SelectedItem = new NullItem();
    }

    /// <summary>
    /// Selects an item in the vending machine based on the provided item identifier.
    /// </summary>
    /// <param name="itemId">
    /// The unique identifier of the item to be selected. This parameter must not be null, empty, or consist only of whitespace.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="itemId"/> is null, empty, or contains only whitespace.
    /// </exception>
    /// <remarks>
    /// This method updates the <see cref="SelectedItem"/> property with the item corresponding to the provided identifier.
    /// If an item is already selected, an event is raised to notify that an item is already selected.
    /// If the specified item is invalid or out of stock, appropriate events are raised to handle these scenarios.
    /// </remarks>
    /// <example>
    /// To select an item in the vending machine:
    /// <code>
    /// vendingMachine.SelectItem("ITEM123");
    /// Console.WriteLine($"Selected Item: {vendingMachine.SelectedItem.Name}");
    /// </code>
    /// </example>
    public void SelectItem(string itemId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(itemId);

        if (SelectedItem is not NullItem)
        {
            RaiseEvent(new ItemAlreadySelectedEventArgs(SelectedItem));
            return;
        }

        var item = _inventoryManager.GetItem(itemId);

        switch (item)
        {
            case InvalidItem:
                RaiseEvent(new InvalidItemSelectedEventArgs(itemId));
                return;
            case OutOfStockItem:
                RaiseEvent(new OutOfStockItemEventArgs(item));
                break;
        }

        SelectedItem = item;
        CurrentState.SelectItem(SelectedItem);
    }

    /// <summary>
    /// Inserts a specified amount of cash into the vending machine.
    /// </summary>
    /// <param name="amount">The amount of cash to insert. Must be a non-negative integer.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the specified amount is negative.</exception>
    /// <remarks>
    /// If the inserted cash is not a valid bill denomination, the cash will be rejected, 
    /// and a corresponding event will be raised. Otherwise, the current state of the vending machine 
    /// will handle the cash insertion.
    /// </remarks>
    public void InsertCash(int amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(amount);

        if (!_moneyManager.IsValidBillDenomination(amount))
        {
            RaiseEvent(new BillRejectedEventArgs(amount));
            return;
        }

        CurrentState.InsertCash(amount);
    }

    /// <summary>
    /// Dispenses the currently selected item from the vending machine.
    /// </summary>
    /// <remarks>
    /// This method performs the following actions:
    /// <list type="bullet">
    /// <item><description>Raises an <see cref="ItemDispensedEventArgs"/> event to indicate that the selected item has been dispensed.</description></item>
    /// <item><description>Notifies the inventory manager that the item has been sold.</description></item>
    /// <item><description>Checks for items with low inventory levels and raises appropriate events, such as <see cref="OutOfStockItemEventArgs"/> or <see cref="LowInventoryItemEventArgs"/>.</description></item>
    /// <item><description>Resets the selected item to a <see cref="NullItem"/>.</description></item>
    /// </list>
    /// </remarks>
    public void DispenseItem()
    {
        OnMessageRaised?.Invoke(this, new ItemDispensedEventArgs(SelectedItem!));

        _inventoryManager.ItemSold(SelectedItem!.ItemId);
        var lowInventoryItems = _inventoryManager.GetLowInventoryItems();

        foreach (var lowInventoryItem in lowInventoryItems)
        {
            if (lowInventoryItem.Quantity == 0)
            {
                OnMessageRaised?.Invoke(this, new OutOfStockItemEventArgs(lowInventoryItem));
            }
            else
            {
                OnMessageRaised?.Invoke(this, new LowInventoryItemEventArgs(lowInventoryItem));
            }
        }

        SelectedItem = new NullItem();
    }

    /// <summary>
    /// Dispenses the specified amount of change to the user.
    /// </summary>
    /// <param name="amount">
    /// The amount of change to be dispensed, in the smallest currency unit (e.g., cents).
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the specified <paramref name="amount"/> is negative.
    /// </exception>
    /// <remarks>
    /// This method ensures that the vending machine refunds the specified amount of change
    /// to the user. It validates the input to prevent negative values and delegates the 
    /// refund operation to the <see cref="Refund(int)"/> method.
    /// </remarks>
    public void DispenseChange(int amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(amount);

        Refund(amount);
    }

    /// <summary>
    /// Refunds the specified amount of money to the user.
    /// </summary>
    /// <param name="amount">
    /// The amount of money to be refunded, in the smallest currency unit (e.g., cents).
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the specified <paramref name="amount"/> is negative.
    /// </exception>
    /// <remarks>
    /// This method validates the input to ensure the amount is non-negative. If the amount is zero,
    /// it raises an event indicating no change is dispensed. Otherwise, it delegates the change-making
    /// process to the <see cref="Domain.Money.IMoneyManager.MakeChange(int)"/> method and raises an event
    /// with the dispensed change.
    /// </remarks>
    public void Refund(int amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(amount);

        if (amount == 0)
        {
            OnMessageRaised?.Invoke(this, new ChangeDispensedEventArgs(new NoChange()));
            return;
        }

        var change = _moneyManager.MakeChange(amount);

        OnMessageRaised?.Invoke(this, new ChangeDispensedEventArgs(change));
    }

    /// <summary>
    /// Cancels the current transaction in the vending machine.
    /// </summary>
    /// <remarks>
    /// This method performs the following actions:
    /// <list type="bullet">
    /// <item>Raises the <see cref="OnMessageRaised"/> event with a <see cref="TransactionCancelledEventArgs"/> instance.</item>
    /// <item>Transitions the vending machine's state to cancel the transaction.</item>
    /// <item>Resets the selected item to a default, unselected state.</item>
    /// </list>
    /// </remarks>
    public void CancelTransaction()
    {
        OnMessageRaised?.Invoke(this, new TransactionCancelledEventArgs());
        CurrentState.CancelTransaction();
        SelectedItem = new NullItem();
    }

    /// <summary>
    /// Raises an event with the specified event arguments.
    /// </summary>
    /// <param name="args">The event arguments containing details about the event to be raised.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="args"/> is <c>null</c>.</exception>
    /// <remarks>
    /// This method triggers the <see cref="OnMessageRaised"/> event, allowing subscribers to respond to 
    /// specific actions or states within the vending machine. The event arguments provide context about 
    /// the event being raised.
    /// </remarks>
    /// <example>
    /// To raise an event:
    /// <code>
    /// var eventArgs = new VendingMachineEventArgs("Item selected");
    /// vendingMachine.RaiseEvent(eventArgs);
    /// </code>
    /// </example>
    public void RaiseEvent(VendingMachineEventArgs args)
    {
        ArgumentNullException.ThrowIfNull(args);

        OnMessageRaised?.Invoke(this, args);
    }

}
