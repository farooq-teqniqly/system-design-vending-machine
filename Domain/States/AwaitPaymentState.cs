using Domain.Money;

namespace Domain.States;
public sealed class AwaitPaymentState : IState
{
    private readonly VendingMachine _vendingMachine;

    public AwaitPaymentState(VendingMachine vendingMachine)
    {
        _vendingMachine = vendingMachine;
    }
    public void SelectItem(string itemId)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }
}
