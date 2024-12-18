using Domain.Inventory;

namespace Domain.EventArgs;

public class ItemDispensedEventArgs : VendingMachineEventArgs
{
    public Item Item { get; }

    public ItemDispensedEventArgs(Item item) : base($"item dispensed: {item}")
    {
        Item = item;
    }
}