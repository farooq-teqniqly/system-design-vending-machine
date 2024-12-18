using Domain.Inventory;
using Domain.States;

namespace Domain;
public sealed class VendingMachine
{
    public event EventHandler<VendingMachineEventArgs>? OnMessageRaised;

    public IState CurrentState { get; set; }

    public VendingMachine(IInventoryManager inventoryManager)
    {
        CurrentState = new IdleState(this, inventoryManager);
    }

    public void RaiseEvent(VendingMachineEventArgs args)
    {
        OnMessageRaised?.Invoke(this, args);
    }
}
