namespace Domain.EventArgs;

/// <summary>
/// Represents the event data for a transaction cancellation in the vending machine.
/// </summary>
/// <remarks>
/// This event is raised when a transaction is cancelled, either before or after an item is selected.
/// </remarks>
public class TransactionCancelledEventArgs : VendingMachineEventArgs
{
    public TransactionCancelledEventArgs() : base("transaction cancelled")
    {
    }
}
