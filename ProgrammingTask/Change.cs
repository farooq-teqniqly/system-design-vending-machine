namespace ProgrammingTask;

/// <summary>
/// Represents the change returned after a transaction, consisting of quarters, dimes, and nickels.
/// </summary>
/// <remarks>
/// This class provides functionality to calculate the change based on the amount deposited and the cost of an item.
/// It ensures that the change can be represented using only quarters, dimes, and nickels.
/// </remarks>
public class Change
{
    /// <summary>
    /// Gets the number of quarters included in the change.
    /// </summary>
    /// <remarks>
    /// This property represents the count of 25-cent coins returned as part of the change.
    /// It is calculated based on the total amount due after a transaction.
    /// </remarks>
    public int Quarters { get; }
    /// <summary>
    /// Gets the number of dimes included in the change.
    /// </summary>
    /// <remarks>
    /// A dime is worth 10 cents. This property represents the count of dimes
    /// returned as part of the change after a transaction.
    /// </remarks>
    public int Dimes { get; }
    /// <summary>
    /// Gets the number of nickels in the change.
    /// </summary>
    /// <remarks>
    /// This property represents the count of nickels included in the change.
    /// It is calculated as part of the <see cref="MakeChange"/> method, ensuring that the change
    /// can be represented using only quarters, dimes, and nickels.
    /// </remarks>
    public int Nickels { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Change"/> class with the specified number of quarters, dimes, and nickels.
    /// </summary>
    /// <param name="quarters">The number of quarters in the change.</param>
    /// <param name="dimes">The number of dimes in the change.</param>
    /// <param name="nickels">The number of nickels in the change.</param>
    private Change(int quarters, int dimes, int nickels)
    {
        Quarters = quarters;
        Dimes = dimes;
        Nickels = nickels;
    }

    /// <summary>
    /// Calculates the change to be returned after a transaction, represented in quarters, dimes, and nickels.
    /// </summary>
    /// <param name="centsDeposited">The total amount of cents deposited by the user. Must be non-negative.</param>
    /// <param name="sodaCostInCents">The cost of the soda in cents. Must be greater than zero.</param>
    /// <returns>
    /// An instance of the <see cref="Change"/> class representing the calculated change.
    /// If no change is due, all coin counts will be zero.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="centsDeposited"/> is negative or <paramref name="sodaCostInCents"/> is zero or negative.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the change cannot be represented using only quarters, dimes, and nickels.
    /// </exception>
    /// <remarks>
    /// This method ensures that the change is calculated accurately and can be represented using only quarters, dimes, and nickels.
    /// If the deposited amount is less than or equal to the soda cost, no change is returned.
    /// </remarks>
    public static Change MakeChange(int centsDeposited, int sodaCostInCents)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(centsDeposited);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(sodaCostInCents);

        var amountDue = centsDeposited - sodaCostInCents;

        if (amountDue <= 0)
        {
            return new Change(0, 0, 0);
        }

        var quarters = amountDue / 25;
        amountDue %= 25;

        var dimes = amountDue / 10;
        amountDue %= 10;

        var nickels = amountDue / 5;
        amountDue %= 5;

        if (amountDue != 0)
        {
            throw new InvalidOperationException("unable to make change");
        }

        return new Change(quarters, dimes, nickels);
    }
}

