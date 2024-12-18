namespace Domain.EventArgs;

public class InvalidItemSelectedEventArgs : VendingMachineEventArgs
{
    public string ItemId { get; }

    public InvalidItemSelectedEventArgs(string itemId) : base($"invalid item selected: {itemId}")
    {
        ItemId = itemId;
    }
}
