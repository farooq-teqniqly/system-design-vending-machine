using Domain.Inventory;

namespace Domain.EventArgs;

public class ItemSelectedEventArgs : VendingMachineEventArgs
{
    public Item Item { get; set; }

    public ItemSelectedEventArgs(Item item) : base($"item selected: {item}")
    {
        Item = item;
    }
}