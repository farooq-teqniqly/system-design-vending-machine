namespace Domain.EventArgs;

public class BillAcceptedEventArgs : VendingMachineEventArgs
{
    public int Amount { get; set; }
    public BillAcceptedEventArgs(int amount) : base($"bill accepted; amount: {amount}")
    {
        Amount = amount;
    }
}