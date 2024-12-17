namespace Domain;

public record Item(string Sku, string Description, decimal UnitPrice, int RemainingStock);

public record NullItem()
    : Item(string.Empty, string.Empty, default, default);
