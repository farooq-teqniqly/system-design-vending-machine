using Domain.Inventory;
using Domain.Money;

namespace Domain.States;
public sealed class AwaitingPaymentState : IState
{
    private readonly VendingMachine _vendingMachine;
    private decimal _totalInsertedAmount;

    public AwaitingPaymentState(VendingMachine vendingMachine)
    {
        _vendingMachine = vendingMachine;
    }
    public void SelectItem(Item item)
    {
        throw new NotSupportedException();
    }

    public void InsertCash(int amount)
    {
        _totalInsertedAmount += amount;
        _vendingMachine.RaiseEvent(new VendingMachineEventArgs($"bill inserted: {amount}"));

        var itemPrice = _vendingMachine.SelectedItem!.Price;

        if (_totalInsertedAmount < itemPrice)
        {
            var needed = itemPrice - _totalInsertedAmount;
            _vendingMachine.RaiseEvent(new InsertMoreMoneyEventArgs(_totalInsertedAmount, needed));

            return;
        }

        _vendingMachine.ChangeToDispense = (int)((itemPrice - _totalInsertedAmount) * 100);
        _vendingMachine.RaiseEvent(new VendingMachineEventArgs("payment complete"));
        _vendingMachine.CurrentState = new DispenseState(_vendingMachine);
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
