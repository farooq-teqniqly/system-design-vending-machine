namespace Domain.EventArgs;

/// <summary>
/// Represents the event arguments for an invalid item selection in the vending machine.
/// </summary>
/// <remarks>
/// This class is used to provide details about an invalid item selection event, including the ID of the invalid item.
/// </remarks>
public class InvalidItemSelectedEventArgs : VendingMachineEventArgs
{
    public string ItemId { get; }

    public InvalidItemSelectedEventArgs(string itemId) : base($"invalid item selected: {itemId}")
    {
        ItemId = itemId;
    }
}
