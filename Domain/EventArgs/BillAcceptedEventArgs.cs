namespace Domain.EventArgs;

/// <summary>
/// Represents the event arguments for a bill accepted event in the vending machine.
/// </summary>
/// <remarks>
/// This class provides information about the accepted bill, including the amount.
/// It inherits from <see cref="VendingMachineEventArgs"/> to include a descriptive message.
/// </remarks>
public class BillAcceptedEventArgs : VendingMachineEventArgs
{
    public int Amount { get; set; }
    public BillAcceptedEventArgs(int amount) : base($"bill accepted; amount: {amount}")
    {
        Amount = amount;
    }
}
