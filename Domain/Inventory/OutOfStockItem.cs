namespace Domain.Inventory;

public sealed record OutOfStockItem(Item item) : Item(item.ItemId, item.Name, item.Price, item.Quantity);
