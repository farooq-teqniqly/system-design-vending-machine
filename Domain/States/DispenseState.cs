using Domain.Inventory;

namespace Domain.States;
public sealed class DispenseState : IState
{
    private readonly VendingMachine _vendingMachine;

    public DispenseState(VendingMachine vendingMachine)
    {
        _vendingMachine = vendingMachine;
    }
    public void SelectItem(Item item)
    {
        throw new NotImplementedException();
    }

    public void InsertCash(int amount)
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
