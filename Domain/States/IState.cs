using Domain.Inventory;

namespace Domain.States;

/// <summary>
/// Defines the contract for a state in the vending machine's state management system.
/// </summary>
/// <remarks>
/// Implementations of this interface represent specific states of the vending machine, 
/// such as idle, awaiting payment, or dispensing items. Each state encapsulates the behavior 
/// and transitions associated with that particular state.
/// </remarks>
public interface IState
{
    /// <summary>
    /// Handles the selection of an item in the vending machine.
    /// </summary>
    /// <param name="item">The <see cref="Domain.Inventory.Item"/> to be selected by the user.</param>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="item"/> is <c>null</c>.</exception>
    /// <remarks>
    /// Implementations of this method define the behavior of the vending machine when an item is selected, 
    /// depending on the current state. For example, in the idle state, this may transition the machine to 
    /// the awaiting payment state, while in other states, it may raise an event or prevent further selection.
    /// </remarks>
    void SelectItem(Item item);

    /// <summary>
    /// Handles the insertion of cash into the vending machine.
    /// </summary>
    /// <param name="amount">The amount of cash being inserted. Must be a non-negative value.</param>
    /// <remarks>
    /// The behavior of this method depends on the current state of the vending machine:
    /// <list type="bullet">
    /// <item><description>In the <see cref="AwaitingPaymentState"/>, this method updates the total inserted amount, raises appropriate events, and may complete the payment process.</description></item>
    /// <item><description>In the <see cref="IdleState"/>, this method raises an event prompting the user to select an item first.</description></item>
    /// </list>
    /// </remarks>
    void InsertCash(int amount);

    /// <summary>
    /// Cancels the current transaction in the vending machine.
    /// </summary>
    /// <remarks>
    /// This method is responsible for aborting the ongoing transaction. It may include actions such as refunding 
    /// the inserted cash, resetting the selected item, and transitioning the vending machine to an appropriate state, 
    /// such as idle. The specific behavior depends on the current state of the vending machine.
    /// </remarks>
    void CancelTransaction();
}
