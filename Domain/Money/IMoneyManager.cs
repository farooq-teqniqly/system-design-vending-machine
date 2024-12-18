namespace Domain.Money;

public interface IMoneyManager
{
    bool IsValidBillDenomination(int amount);
}
