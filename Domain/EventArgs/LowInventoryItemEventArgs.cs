using Domain.Inventory;

namespace Domain.EventArgs;

/// <summary>
/// Represents event data for a low inventory item in the vending machine.
/// </summary>
/// <remarks>
/// This event is triggered when the inventory level of an item falls below a defined threshold.
/// </remarks>
public class LowInventoryItemEventArgs : VendingMachineEventArgs
{
    public Item Item { get; }

    public LowInventoryItemEventArgs(Item item) : base($"item inventory low: {item}")
    {
        Item = item;
    }
}
