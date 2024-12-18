using Domain.EventArgs;
using Domain.Inventory;

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
        _vendingMachine.RaiseEvent(new ItemSelectedEventArgs(item));
        _vendingMachine.CurrentState = new AwaitingPaymentState(_vendingMachine);
    }

    public void InsertCash(int amount)
    {
        _vendingMachine.RaiseEvent(new VendingMachineEventArgs("select an item first"));
    }

    public void CancelTransaction()
    {
        _vendingMachine.CurrentState = new IdleState(_vendingMachine);
    }

    public void NotifyLowStockItems()
    {
        throw new NotSupportedException();
    }
}
