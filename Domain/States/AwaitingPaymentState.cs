using Domain.EventArgs;
using Domain.Inventory;
using Domain.Money;

namespace Domain.States;
public sealed class AwaitingPaymentState : IState
{
    private readonly VendingMachine _vendingMachine;
    private int _totalInsertedAmount;

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
        _vendingMachine.RaiseEvent(new BillAcceptedEventArgs(amount));

        var itemPrice = _vendingMachine.SelectedItem!.Price;

        if (_totalInsertedAmount < itemPrice)
        {
            var needed = itemPrice - _totalInsertedAmount;
            _vendingMachine.RaiseEvent(new InsertMoreMoneyEventArgs(_totalInsertedAmount, needed));

            return;
        }

        var change = _totalInsertedAmount - itemPrice;

        _vendingMachine.RaiseEvent(new PaymentCompleteEventArgs(_totalInsertedAmount, change));

        _vendingMachine.DispenseItem();
        _vendingMachine.DispenseChange((int)(change * 100));

        _vendingMachine.CurrentState = new IdleState(_vendingMachine);
    }

    public void CancelTransaction()
    {
        _vendingMachine.Refund(_totalInsertedAmount * 100);
        _vendingMachine.CurrentState = new IdleState(_vendingMachine);
    }

    public void NotifyLowStockItems()
    {
        throw new NotSupportedException();
    }
}
