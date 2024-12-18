namespace Domain.EventArgs;

public class TransactionCancelledEventArgs : VendingMachineEventArgs
{
    public TransactionCancelledEventArgs() : base("transaction cancelled")
    {
    }
}
