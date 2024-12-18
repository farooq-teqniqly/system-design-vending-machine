namespace Domain.Inventory;

/// <summary>
/// Represents an item in the inventory with its unique identifier, name, price, and quantity.
/// </summary>
public record Item(string ItemId, string Name, decimal Price, int Quantity);
