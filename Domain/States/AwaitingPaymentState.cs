using Domain.EventArgs;
using Domain.Inventory;

namespace Domain.States;
/// <summary>
/// Represents the state of a vending machine when it is awaiting payment for a selected item.
/// </summary>
/// <remarks>
/// This state handles the insertion of cash, cancellation of the transaction, and transitions to other states
/// based on the payment status. It ensures that the vending machine processes payments correctly and dispenses
/// items and change as needed.
/// </remarks>
public sealed class AwaitingPaymentState : IState
{
    private readonly VendingMachine _vendingMachine;
    private int _totalInsertedAmount;

    /// <summary>
    /// Initializes a new instance of the <see cref="AwaitingPaymentState"/> class.
    /// </summary>
    /// <param name="vendingMachine">
    /// The <see cref="VendingMachine"/> instance that this state is associated with.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the <paramref name="vendingMachine"/> is <c>null</c>.
    /// </exception>
    /// <remarks>
    /// This constructor sets up the state to manage payment processing for the vending machine.
    /// </remarks>
    public AwaitingPaymentState(VendingMachine vendingMachine)
    {
        ArgumentNullException.ThrowIfNull(vendingMachine);

        _vendingMachine = vendingMachine;
    }

    /// <summary>
    /// Handles the selection of an item when the vending machine is in the awaiting payment state.
    /// </summary>
    /// <param name="item">
    /// The <see cref="Item"/> to be selected.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the <paramref name="item"/> is <c>null</c>.
    /// </exception>
    /// <remarks>
    /// This method raises an event indicating that an item has already been selected.
    /// To select a different item, the transaction must be canceled first.
    /// </remarks>
    public void SelectItem(Item item)
    {
        ArgumentNullException.ThrowIfNull(item);

        _vendingMachine.RaiseEvent(new VendingMachineEventArgs("an item was already selected; if you'd like to select a different item, CANCEL the transaction first."));
    }

    /// <summary>
    /// Handles the insertion of cash into the vending machine while in the awaiting payment state.
    /// </summary>
    /// <param name="amount">The amount of cash being inserted. Must be a non-negative value.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the <paramref name="amount"/> is negative.</exception>
    /// <remarks>
    /// This method updates the total inserted amount and raises appropriate events based on the payment status:
    /// <list type="bullet">
    /// <item><description>Raises a <see cref="Domain.EventArgs.BillAcceptedEventArgs"/> event when cash is inserted.</description></item>
    /// <item><description>Raises a <see cref="Domain.EventArgs.InsertMoreMoneyEventArgs"/> event if more money is needed to complete the payment.</description></item>
    /// <item><description>Raises a <see cref="Domain.EventArgs.PaymentCompleteEventArgs"/> event when the payment is complete.</description></item>
    /// </list>
    /// If the payment is complete, the method dispenses the selected item and any change, and transitions the vending machine to the idle state.
    /// </remarks>
    public void InsertCash(int amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(amount);

        _totalInsertedAmount += amount;
        _vendingMachine.RaiseEvent(new BillAcceptedEventArgs(amount));

        var itemPrice = _vendingMachine.SelectedItem!.Price;

        if (_totalInsertedAmount < itemPrice)
        {
            var needed = itemPrice - _totalInsertedAmount;
            _vendingMachine.RaiseEvent(new InsertMoreMoneyEventArgs(_totalInsertedAmount, needed));

            return;
        }

        var change = _totalInsertedAmount - itemPrice;

        _vendingMachine.RaiseEvent(new PaymentCompleteEventArgs(_totalInsertedAmount, change));

        _vendingMachine.DispenseItem();
        _vendingMachine.DispenseChange((int)(change * 100));

        _vendingMachine.CurrentState = new IdleState(_vendingMachine);
    }

    /// <summary>
    /// Cancels the current transaction and transitions the vending machine to the idle state.
    /// </summary>
    /// <remarks>
    /// This method refunds the total amount of cash inserted during the transaction and resets the vending machine
    /// to its idle state, allowing it to accept new transactions.
    /// </remarks>
    public void CancelTransaction()
    {
        _vendingMachine.Refund(_totalInsertedAmount * 100);
        _vendingMachine.CurrentState = new IdleState(_vendingMachine);
    }
}
