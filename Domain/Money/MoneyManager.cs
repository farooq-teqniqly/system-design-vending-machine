namespace Domain.Money;
public sealed class MoneyManager : IMoneyManager
{
    private static readonly int[] _validBillDenominations = new[] { 1, 5, 10, 20 };
    
    public bool IsValidBillDenomination(int amount) => _validBillDenominations.Contains(amount);

    public Change MakeChange(int amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(amount);

        if (amount == 0)
        {
            return new NoChange();
        }

        var amountLeft = amount;

        var quarters = amountLeft / 25;
        amountLeft %= 25;

        var dimes = amountLeft / 10;
        amountLeft %= 10;

        var nickels = amountLeft / 5;
        amountLeft %= 5;

        var pennies = amountLeft / 1;
        amountLeft %= 1;

        if (amountLeft != 0)
        {
            throw new InvalidOperationException("unable to make change");
        }

        return new Change(quarters, dimes, nickels, pennies);
    }

}
