using Domain.Inventory;
using Domain.Money;

namespace Domain.States;

public sealed class IdleState : IState
{
    private readonly VendingMachine _vendingMachine;

    public IdleState(VendingMachine vendingMachine)
    {
        _vendingMachine = vendingMachine;
    }
    public void SelectItem(Item item)
    {
        _vendingMachine.RaiseEvent(new VendingMachineEventArgs($"selected item: `{item}`"));
        _vendingMachine.CurrentState = new AwaitingPaymentState(_vendingMachine);
    }

    public void InsertCash(int amount)
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
