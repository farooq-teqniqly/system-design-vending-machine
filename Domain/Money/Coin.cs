namespace Domain.Money;

public record Coin(string Name, decimal Value) : Denomination(Name, Value);