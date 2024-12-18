namespace Domain.Inventory;

public class InventoryManagerConfiguration
{
    public int LowInventoryThreshold { get; }

    public InventoryManagerConfiguration(int lowInventoryThreshold)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(lowInventoryThreshold);

        LowInventoryThreshold = lowInventoryThreshold;
    }
}
