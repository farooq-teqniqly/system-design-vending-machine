namespace Domain.Inventory;

/// <summary>
/// Represents an item that is out of stock in the inventory.
/// </summary>
/// <remarks>
/// This record is used to indicate that a specific item exists in the inventory
/// but currently has a quantity of zero. It inherits from <see cref="Item"/> 
/// and retains all the properties of the original item.
/// </remarks>
public sealed record OutOfStockItem(Item item) : Item(item.ItemId, item.Name, item.Price, item.Quantity);
