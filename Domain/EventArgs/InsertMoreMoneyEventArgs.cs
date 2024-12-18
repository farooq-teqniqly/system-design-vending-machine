namespace Domain.EventArgs;

public class InsertMoreMoneyEventArgs : VendingMachineEventArgs
{
    public InsertMoreMoneyEventArgs(
        decimal totalAmountInserted,
        decimal amountNeeded) : base($"an additional {amountNeeded} is needed; total amount inserted: {totalAmountInserted}")
    {
    }
}
