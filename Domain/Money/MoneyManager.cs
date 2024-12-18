namespace Domain.Money;
/// <summary>
/// Provides functionality for managing monetary operations, such as validating bill denominations
/// and calculating change, within the vending machine system.
/// </summary>
/// <remarks>
/// This class implements the <see cref="IMoneyManager"/> interface and ensures proper handling of
/// monetary transactions. It supports validation of bill denominations and the creation of change
/// for a given amount.
/// </remarks>
public sealed class MoneyManager : IMoneyManager
{
    private static readonly int[] _validBillDenominations = new[] { 1, 5, 10, 20 };

    /// <summary>
    /// Determines whether the specified amount corresponds to a valid bill denomination.
    /// </summary>
    /// <param name="amount">The monetary amount to validate.</param>
    /// <returns>
    /// <see langword="true"/> if the specified amount is a valid bill denomination; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// This method checks if the provided amount matches one of the predefined valid bill denominations
    /// within the vending machine system. It ensures that only acceptable denominations are processed.
    /// </remarks>
    public bool IsValidBillDenomination(int amount) => _validBillDenominations.Contains(amount);

    /// <summary>
    /// Calculates the change for a specified amount in cents.
    /// </summary>
    /// <param name="amount">The total amount in cents for which change is to be calculated. Must be non-negative.</param>
    /// <returns>
    /// A <see cref="Change"/> instance representing the breakdown of quarters, dimes, nickels, and pennies.
    /// If the amount is zero, a <see cref="NoChange"/> instance is returned.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the provided <paramref name="amount"/> is negative.
    /// </exception>
    /// <remarks>
    /// This method calculates change using the largest denominations first (quarters, dimes, nickels, and pennies).
    /// </remarks>
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

        return new Change(quarters, dimes, nickels, pennies);
    }

}
