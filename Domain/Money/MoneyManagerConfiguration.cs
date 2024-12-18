namespace Domain.Money;

/// <summary>
/// Represents the configuration settings for the <see cref="MoneyManager"/> class.
/// </summary>
/// <remarks>
/// This class defines the valid bill denominations that the <see cref="MoneyManager"/> can handle.
/// It ensures that at least one valid bill denomination is provided during initialization.
/// </remarks>
public sealed class MoneyManagerConfiguration
{
    /// <summary>
    /// Gets the collection of valid bill denominations that the <see cref="MoneyManager"/> can process.
    /// </summary>
    /// <value>
    /// A collection of integers representing the valid bill denominations.
    /// </value>
    /// <remarks>
    /// This property defines the acceptable bill denominations for monetary transactions
    /// within the vending machine system. It is initialized during the construction of the
    /// <see cref="MoneyManagerConfiguration"/> and ensures that only valid denominations
    /// are used in operations.
    /// </remarks>
    public IEnumerable<int> ValidBillDenominations { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MoneyManagerConfiguration"/> class with the specified valid bill denominations.
    /// </summary>
    /// <param name="validBillDenominations">
    /// An array of integers representing the valid bill denominations that the <see cref="MoneyManager"/> can accept.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="validBillDenominations"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="validBillDenominations"/> is empty.
    /// </exception>
    public MoneyManagerConfiguration(int[] validBillDenominations)
    {
        ArgumentNullException.ThrowIfNull(validBillDenominations);

        if (validBillDenominations.Length == 0)
        {
            throw new ArgumentException("provide at least one valid bill denomination");
        }

        ValidBillDenominations = validBillDenominations;
    }
}
