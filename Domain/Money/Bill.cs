namespace Domain.Money;

public record Bill(string Name, decimal Value) : Denomination(Name, Value);