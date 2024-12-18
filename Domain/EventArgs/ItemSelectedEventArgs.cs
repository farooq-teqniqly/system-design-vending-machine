using Domain.Inventory;

namespace Domain.EventArgs;

/// <summary>
/// Represents the event arguments for an item selection event in the vending machine.
/// </summary>
/// <remarks>
/// This class provides details about the selected item and inherits from <see cref="VendingMachineEventArgs"/> 
/// to include a message describing the event.
/// </remarks>
public class ItemSelectedEventArgs : VendingMachineEventArgs
{
    public Item Item { get; set; }

    public ItemSelectedEventArgs(Item item) : base($"item selected: {item}")
    {
        Item = item;
    }
}
