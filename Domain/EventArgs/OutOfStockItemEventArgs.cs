using Domain.Inventory;

namespace Domain.EventArgs;

/// <summary>
/// Represents the event data for an out-of-stock item in the vending machine.
/// </summary>
/// <remarks>
/// This event is triggered when an item in the vending machine is out of stock.
/// It provides access to the <see cref="Domain.Inventory.Item"/> that is out of stock.
/// </remarks>
public class OutOfStockItemEventArgs : VendingMachineEventArgs
{
    public Item Item { get; }

    public OutOfStockItemEventArgs(Item item) : base($"item out of stock: {item}")
    {
        Item = item;
    }
}
