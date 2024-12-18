using Domain.Inventory;
using Domain.States;

namespace Domain;
public sealed class VendingMachine
{
    private readonly IInventoryManager _inventoryManager;
    private Item? _selectedItem;

    public event EventHandler<VendingMachineEventArgs>? OnMessageRaised;

    public IState CurrentState { get; set; }

    public VendingMachine(IInventoryManager inventoryManager)
    {
        _inventoryManager = inventoryManager;
        CurrentState = new IdleState(this);
    }

    public void SelectItem(string itemId)
    {
        var item = _inventoryManager.GetItem(itemId);

        if (item is NullItem)
        {
            RaiseEvent(new VendingMachineEventArgs("invalid selection"));
            return;
        }

        _selectedItem = item;
        CurrentState.SelectItem(_selectedItem);
    }

    public void RaiseEvent(VendingMachineEventArgs args)
    {
        OnMessageRaised?.Invoke(this, args);
    }
}
