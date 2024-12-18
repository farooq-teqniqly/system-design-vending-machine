namespace Domain.EventArgs;

/// <summary>
/// Represents the event data for a payment completion in the vending machine.
/// </summary>
/// <remarks>
/// This event is raised when the payment process is completed, providing details about the total amount inserted 
/// and the change due to the customer.
/// </remarks>
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
