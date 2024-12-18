using Domain.Inventory;

namespace Domain.EventArgs;

public class OutOfStockItemEventArgs : VendingMachineEventArgs
{
    public Item Item { get; }

    public OutOfStockItemEventArgs(Item item) : base($"item out of stock: {item}")
    {
        Item = item;
    }
}