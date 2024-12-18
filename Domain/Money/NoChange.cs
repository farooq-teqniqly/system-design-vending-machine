namespace Domain.Money;

/// <summary>
/// Represents a scenario where no change is to be dispensed.
/// </summary>
/// <remarks>
/// This class is a specialized form of <see cref="Change"/> that indicates the absence of any coins.
/// It is typically used when the amount of change due is zero.
/// </remarks>
public sealed class NoChange : Change
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NoChange"/> class.
    /// </summary>
    /// <remarks>
    /// This constructor creates a <see cref="NoChange"/> instance, representing a scenario where no coins are dispensed.
    /// It sets all coin quantities (quarters, dimes, nickels, and pennies) to zero.
    /// </remarks>
    public NoChange() : base(0, 0, 0, 0)
    {
    }
}
