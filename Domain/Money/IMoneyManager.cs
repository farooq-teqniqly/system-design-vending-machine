namespace Domain.Money;

public interface IMoneyManager
{
    bool IsValidBillDenomination(int amount);
    IDictionary<int, int> DispenseChange(int amount);
}
