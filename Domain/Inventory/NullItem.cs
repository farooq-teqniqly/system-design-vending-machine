namespace Domain.Inventory;

/// <summary>
/// Represents a null or placeholder item in the inventory.
/// </summary>
/// <remarks>
/// This record is used to signify the absence of a valid item, typically in scenarios where an item
/// has not been selected or is being reset to a default state.
/// </remarks>
public sealed record NullItem() : Item(string.Empty, string.Empty, default, default);
