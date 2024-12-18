namespace Domain.Money;

/// <summary>
/// Defines the contract for managing monetary operations within the system.
/// </summary>
/// <remarks>
/// This interface provides methods to validate bill denominations and calculate change.
/// It is designed to ensure proper handling of monetary transactions in the system.
/// </remarks>
public interface IMoneyManager
{
    /// <summary>
    /// Determines whether the specified amount corresponds to a valid bill denomination.
    /// </summary>
    /// <param name="amount">The monetary amount to validate.</param>
    /// <returns>
    /// <see langword="true"/> if the specified amount is a valid bill denomination; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// This method checks if the provided amount matches one of the predefined valid bill denominations.
    /// It is used to ensure that only acceptable denominations are processed by the system.
    /// </remarks>
    bool IsValidBillDenomination(int amount);

    /// <summary>
    /// Calculates the change for a given monetary amount.
    /// </summary>
    /// <param name="amount">The total amount for which change is to be calculated. Must be a non-negative value.</param>
    /// <returns>
    /// An instance of <see cref="Domain.Money.Change"/> representing the breakdown of the change in coins.
    /// </returns>
    Change MakeChange(int amount);
}
