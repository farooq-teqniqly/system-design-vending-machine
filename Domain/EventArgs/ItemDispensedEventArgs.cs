using Domain.Inventory;

namespace Domain.EventArgs;

/// <summary>
/// Provides data for the event raised when an item is dispensed from the vending machine.
/// </summary>
/// <remarks>
/// This event argument contains details about the dispensed item, allowing subscribers
/// to access information such as the item's identifier, name, price, and quantity.
/// </remarks>
public class ItemDispensedEventArgs : VendingMachineEventArgs
{
    public Item Item { get; }

    public ItemDispensedEventArgs(Item item) : base($"item dispensed: {item}")
    {
        Item = item;
    }
}
