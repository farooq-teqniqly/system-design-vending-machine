using Domain.Inventory;
using Domain.Money;

namespace Domain.States;

public sealed class IdleState : IState
{
    private readonly VendingMachine _vendingMachine;
    private readonly IInventoryManager _inventoryManager;

    public IdleState(
        VendingMachine vendingMachine,
        IInventoryManager inventoryManager)
    {
        _vendingMachine = vendingMachine;
        _inventoryManager = inventoryManager;
    }
    public void SelectItem(string itemId)
    {
        if (!_inventoryManager.ItemExists(itemId))
        {
            _vendingMachine.RaiseEvent(new VendingMachineEventArgs($"invalid item `{itemId}`"));
        }

        _vendingMachine.RaiseEvent(new VendingMachineEventArgs($"select item `{itemId}`"));
        _vendingMachine.CurrentState = new AwaitPaymentState(_vendingMachine);
    }

    public void InsertCash(Bill bill)
    {
        throw new NotSupportedException();
    }

    public void CancelTransaction()
    {
        throw new NotSupportedException();
    }

    public void NotifyLowStockItems()
    {
        throw new NotSupportedException();
    }
}
