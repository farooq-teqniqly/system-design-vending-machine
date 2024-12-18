namespace Domain.Inventory;

public sealed record Item(string ItemId, string Name, decimal Price, int Quantity);