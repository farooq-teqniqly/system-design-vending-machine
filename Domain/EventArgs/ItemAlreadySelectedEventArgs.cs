using Domain.Inventory;

namespace Domain.EventArgs;

public class ItemAlreadySelectedEventArgs : VendingMachineEventArgs
{
    public Item Item { get; }

    public ItemAlreadySelectedEventArgs(Item item) : base($"item already selected: {item}; cancel the transaction if you'd like to select a different item")
    {
        Item = item;
    }
}
