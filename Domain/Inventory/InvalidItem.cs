namespace Domain.Inventory;

/// <summary>
/// Represents an invalid item in the inventory.
/// </summary>
/// <remarks>
/// This record is used to indicate that an item does not exist in the inventory.
/// It serves as a placeholder for invalid or unrecognized items.
/// </remarks>
public sealed record InvalidItem() : Item(string.Empty, string.Empty, default, default);
