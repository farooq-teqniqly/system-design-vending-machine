namespace Domain.EventArgs;

public class BillRejectedEventArgs : VendingMachineEventArgs
{
    public int Denomination { get; }

    public BillRejectedEventArgs(int denomination) : base($"invalid denomination: {denomination}")
    {
        Denomination = denomination;
    }
}
