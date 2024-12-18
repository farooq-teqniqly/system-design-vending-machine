namespace Domain.Inventory;

/// <summary>
/// Represents the configuration settings for the <see cref="InventoryManager"/>.
/// </summary>
/// <remarks>
/// This class provides configuration options, such as the threshold for low inventory levels, 
/// which are used by the <see cref="InventoryManager"/> to manage inventory operations effectively.
/// </remarks>
public class InventoryManagerConfiguration
{
    /// <summary>
    /// Gets the threshold value that determines when an inventory item is considered to have low stock.
    /// </summary>
    /// <value>
    /// An integer representing the minimum quantity of an item in stock before it is flagged as low inventory.
    /// </value>
    /// <remarks>
    /// This property is used by the <see cref="InventoryManager"/> to identify items with low inventory levels.
    /// It ensures that inventory operations can proactively address stock shortages.
    /// </remarks>
    public int LowInventoryThreshold { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="InventoryManagerConfiguration"/> class with the specified low inventory threshold.
    /// </summary>
    /// <param name="lowInventoryThreshold">
    /// The threshold value below which inventory is considered low. Must be a non-negative integer.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="lowInventoryThreshold"/> is negative.
    /// </exception>
    public InventoryManagerConfiguration(int lowInventoryThreshold)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(lowInventoryThreshold);

        LowInventoryThreshold = lowInventoryThreshold;
    }
}
