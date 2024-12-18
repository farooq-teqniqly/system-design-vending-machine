using Domain.Inventory;

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

        var change = _totalInsertedAmount - itemPrice;

        _vendingMachine.RaiseEvent(new VendingMachineEventArgs("payment complete"));

        _vendingMachine.RaiseEvent(new VendingMachineEventArgs($"paid: {_totalInsertedAmount}"));

        _vendingMachine.RaiseEvent(new VendingMachineEventArgs($"change: {change}"));

        _vendingMachine.DispenseItem();

        if (change != 0)
        {
            _vendingMachine.DispenseChange((int)((_totalInsertedAmount - itemPrice) * 100));
        }

        _vendingMachine.CurrentState = new IdleState(_vendingMachine);
    }

    public void CancelTransaction()
    {
        _vendingMachine.RaiseEvent(new VendingMachineEventArgs("transaction cancelled"));

        if (_totalInsertedAmount != 0)
        {
            _vendingMachine.RaiseEvent(new VendingMachineEventArgs($"paid: {_totalInsertedAmount}"));

            _vendingMachine.RaiseEvent(new VendingMachineEventArgs($"refund: {_totalInsertedAmount}"));

            _vendingMachine.Refund(_totalInsertedAmount);
        }

        _vendingMachine.CurrentState = new IdleState(_vendingMachine);
    }

    public void NotifyLowStockItems()
    {
        throw new NotSupportedException();
    }
}
