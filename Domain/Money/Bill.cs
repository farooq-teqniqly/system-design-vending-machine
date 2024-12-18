namespace Domain.Money;

public record Bill(decimal Value) : Denomination(Value);
