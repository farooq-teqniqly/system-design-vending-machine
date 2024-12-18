namespace Domain.Money;
public sealed class MoneyManager : IMoneyManager
{
    private static readonly int[] _validBillDenominations = new[] { 1, 5, 10, 20 };
    private static readonly int[] _validCoinDenominations = new[] { 1, 5, 10, 25 };

    public bool IsValidBillDenomination(int amount) => _validBillDenominations.Contains(amount);
}
