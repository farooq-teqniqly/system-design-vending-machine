using Domain.EventArgs;
using Domain.Inventory;

namespace Domain.States;

/// <summary>
/// Represents the idle state of the vending machine, where no item is selected, 
/// and the machine is waiting for user interaction.
/// </summary>
/// <remarks>
/// In this state, the vending machine allows the user to select an item. 
/// Attempting to insert cash or perform other actions without selecting an item first 
/// will result in appropriate feedback.
/// </remarks>
public sealed class IdleState : IState
{
    private readonly VendingMachine _vendingMachine;

    /// <summary>
    /// Initializes a new instance of the <see cref="IdleState"/> class.
    /// </summary>
    /// <param name="vendingMachine">The vending machine instance associated with this state.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the <paramref name="vendingMachine"/> is <c>null</c>.
    /// </exception>
    public IdleState(VendingMachine vendingMachine)
    {
        ArgumentNullException.ThrowIfNull(vendingMachine);

        _vendingMachine = vendingMachine;
    }
    /// <summary>
    /// Handles the selection of an item in the vending machine while in the idle state.
    /// </summary>
    /// <param name="item">The <see cref="Domain.Inventory.Item"/> to be selected by the user.</param>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="item"/> is <c>null</c>.</exception>
    /// <remarks>
    /// This method transitions the vending machine from the idle state to the awaiting payment state 
    /// after raising an event to notify that an item has been selected.
    /// </remarks>
    public void SelectItem(Item item)
    {
        ArgumentNullException.ThrowIfNull(item);

        _vendingMachine.RaiseEvent(new ItemSelectedEventArgs(item));
        _vendingMachine.CurrentState = new AwaitingPaymentState(_vendingMachine);
    }

    /// <summary>
    /// Handles the insertion of cash while the vending machine is in the idle state.
    /// </summary>
    /// <param name="amount">The amount of cash to insert.</param>
    /// <remarks>
    /// Since no item is selected in the idle state, this method notifies the user to select an item first.
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the <paramref name="amount"/> is negative.
    /// </exception>
    public void InsertCash(int amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(amount);

        _vendingMachine.RaiseEvent(new VendingMachineEventArgs("select an item first"));
    }

    /// <summary>
    /// Cancels the current transaction and resets the vending machine to the idle state.
    /// </summary>
    /// <remarks>
    /// This method transitions the vending machine back to the idle state, effectively canceling any ongoing transaction.
    /// It is typically invoked when the user decides to abort the transaction before completing the payment.
    /// </remarks>
    public void CancelTransaction()
    {
        _vendingMachine.CurrentState = new IdleState(_vendingMachine);
    }
}
