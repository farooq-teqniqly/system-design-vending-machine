namespace Domain.EventArgs;

/// <summary>
/// Provides data for the event that is raised when a bill with an invalid denomination is rejected.
/// </summary>
/// <remarks>
/// This event argument is used to notify about an invalid bill denomination inserted into the vending machine.
/// </remarks>
public class BillRejectedEventArgs : VendingMachineEventArgs
{
    public int Denomination { get; }

    public BillRejectedEventArgs(int denomination) : base($"invalid denomination: {denomination}")
    {
        Denomination = denomination;
    }
}
