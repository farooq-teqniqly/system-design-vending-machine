namespace Domain.Inventory;

public sealed record InvalidItem() : Item(string.Empty, string.Empty, default, default);
