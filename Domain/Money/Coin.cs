namespace Domain.Money;

public record Coin(decimal Value) : Denomination(Value);
