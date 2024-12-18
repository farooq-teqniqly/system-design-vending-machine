using Domain.Inventory;

namespace Domain.EventArgs;

/// <summary>
/// Represents the event arguments for a scenario where an item has already been selected in the vending machine.
/// </summary>
/// <remarks>
/// This class provides details about the already selected item and is used to notify that a new selection cannot be made
/// unless the current transaction is canceled.
/// </remarks>
public class ItemAlreadySelectedEventArgs : VendingMachineEventArgs
{
    public Item Item { get; }

    public ItemAlreadySelectedEventArgs(Item item) : base($"item already selected: {item}; cancel the transaction if you'd like to select a different item")
    {
        Item = item;
    }
}
