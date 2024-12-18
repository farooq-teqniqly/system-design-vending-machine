using Domain.Inventory;
using Domain.Money;

namespace Domain;
public class VendingMachineEventArgs : EventArgs
{
    public string Message { get; }

    public VendingMachineEventArgs(string message)
    {
        Message = message;
    }
}

public class ItemSelectedEventArgs : VendingMachineEventArgs
{
    public Item Item { get; set; }

    public ItemSelectedEventArgs(Item item) : base($"item selected: {item}")
    {
        Item = item;
    }
}

public class BillAcceptedEventArgs : VendingMachineEventArgs
{
    public int Amount { get; set; }
    public BillAcceptedEventArgs(int amount) : base($"bill accepted; amount: {amount}")
    {
        Amount = amount;
    }
}

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

public class ItemDispensedEventArgs : VendingMachineEventArgs
{
    public Item Item { get; }

    public ItemDispensedEventArgs(Item item) : base($"item dispensed: {item}")
    {
        Item = item;
    }
}

public class ChangeDispensedEventArgs : VendingMachineEventArgs
{
    public Change Change { get; }

    public ChangeDispensedEventArgs(Change change) : base($"change dispensed: {change}")
    {
        Change = change;
    }
} 

public class BillRejectedEventArgs : VendingMachineEventArgs
{
    public int Denomination { get; }

    public BillRejectedEventArgs(int denomination) : base($"invalid denomination: {denomination}")
    {
        Denomination = denomination;
    }
}
