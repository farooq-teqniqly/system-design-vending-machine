using Domain.Inventory;

namespace Domain.EventArgs;

public class LowInventoryItemEventArgs : VendingMachineEventArgs
{
    public Item Item { get; }

    public LowInventoryItemEventArgs(Item item) : base($"item inventory low: {item}")
    {
        Item = item;
    }
}