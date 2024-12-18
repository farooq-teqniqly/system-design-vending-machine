using System.Text;

namespace Domain.Money;

/// <summary>
/// Represents the change dispensed in the form of coins.
/// </summary>
/// <remarks>
/// This class encapsulates the quantities of quarters, dimes, nickels, and pennies
/// that make up the change. It provides functionality to initialize and display
/// the coin breakdown.
/// </remarks>
public class Change
{
    /// <summary>
    /// Gets the number of quarters included in the change.
    /// </summary>
    /// <remarks>
    /// This property represents the count of 25-cent coins dispensed as part of the change.
    /// The value is guaranteed to be non-negative.
    /// </remarks>
    public int Quarters { get; }
    /// <summary>
    /// Gets the number of dimes included in the change.
    /// </summary>
    /// <remarks>
    /// A dime is a coin worth 10 cents. This property represents the count of dimes
    /// dispensed as part of the change.
    /// </remarks>
    public int Dimes { get; }
    /// <summary>
    /// Gets the number of nickels in the change.
    /// </summary>
    /// <remarks>
    /// This property represents the quantity of nickels included in the dispensed change.
    /// It is initialized during the creation of the <see cref="Change"/> object and is immutable.
    /// </remarks>
    public int Nickels { get; }

    /// <summary>
    /// Gets the number of pennies included in the change.
    /// </summary>
    /// <value>
    /// The number of pennies, which must be a non-negative integer.
    /// </value>
    /// <remarks>
    /// This property represents the smallest denomination of coins in the change.
    /// </remarks>
    public int Pennies { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Change"/> class with the specified quantities of coins.
    /// </summary>
    /// <param name="quarters">The number of quarters in the change. Must be non-negative.</param>
    /// <param name="dimes">The number of dimes in the change. Must be non-negative.</param>
    /// <param name="nickels">The number of nickels in the change. Must be non-negative.</param>
    /// <param name="pennies">The number of pennies in the change. Must be non-negative.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when any of the provided coin quantities is negative.
    /// </exception>
    public Change(int quarters, int dimes, int nickels, int pennies)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(quarters);
        ArgumentOutOfRangeException.ThrowIfNegative(dimes);
        ArgumentOutOfRangeException.ThrowIfNegative(nickels);
        ArgumentOutOfRangeException.ThrowIfNegative(pennies);

        Quarters = quarters;
        Dimes = dimes;
        Nickels = nickels;
        Pennies = pennies;
    }

    /// <summary>
    /// Returns a string representation of the current <see cref="Change"/> instance.
    /// </summary>
    /// <returns>
    /// A string that lists the quantities of quarters, dimes, nickels, and pennies
    /// in the current <see cref="Change"/> instance.
    /// </returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Quarters: {Quarters}");
        sb.AppendLine($"Dimes: {Dimes}");
        sb.AppendLine($"Nickels: {Nickels}");
        sb.AppendLine($"Pennies: {Pennies}");

        return sb.ToString();
    }
}
