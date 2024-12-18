using Domain.Inventory;
using Domain.Money;

namespace Domain.States;
public sealed class AwaitingPaymentState : IState
{
    private readonly VendingMachine _vendingMachine;

    public AwaitingPaymentState(VendingMachine vendingMachine)
    {
        _vendingMachine = vendingMachine;
    }
    public void SelectItem(Item item)
    {
        throw new NotSupportedException();
    }

    public void InsertCash(Bill bill)
    {
        throw new NotImplementedException();
    }

    public void CancelTransaction()
    {
        throw new NotImplementedException();
    }

    public void NotifyLowStockItems()
    {
        throw new NotSupportedException();
    }
}
