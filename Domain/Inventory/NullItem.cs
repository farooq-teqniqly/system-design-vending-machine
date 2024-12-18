namespace Domain.Inventory;

public sealed record NullItem() : Item(string.Empty, string.Empty, default, default);
