namespace Domain.Money;
public sealed class MoneyManager : IMoneyManager
{
    private static readonly int[] _validBillDenominations = new[] { 1, 5, 10, 20 };
    private static readonly int[] _validCoinDenominations = new[] { 1, 5, 10, 25 };

    public bool IsValidBillDenomination(int amount) => _validBillDenominations.Contains(amount);

    public IDictionary<int, int> DispenseChange(int amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);

        var orderedCoinDenominations = _validCoinDenominations.AsEnumerable().OrderDescending().ToArray();

        var change = new Dictionary<int, int>();

        var amountLeft = amount;

        foreach (var denomination in orderedCoinDenominations)
        {
            if (amountLeft == 0)
            {
                break;
            }

            var coinCount = amountLeft / denomination;

            if (coinCount == 0)
            {
                continue;
            }

            change.Add(denomination, coinCount);
            amountLeft -= denomination * coinCount;
        }

        return change;
    }
}
