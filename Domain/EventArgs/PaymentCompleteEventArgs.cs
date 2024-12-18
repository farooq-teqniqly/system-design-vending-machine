namespace Domain.EventArgs;

public class PaymentCompleteEventArgs : VendingMachineEventArgs
{
    public int Amount { get; }
    public decimal ChangeDue { get; }

    public PaymentCompleteEventArgs(int amount, decimal changeDue) : base($"payment complete; amount: {amount}; change due: {changeDue}")
    {
        Amount = amount;
        ChangeDue = changeDue;
    }
}