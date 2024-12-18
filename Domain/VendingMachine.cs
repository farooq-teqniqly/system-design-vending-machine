using Domain.Inventory;
using Domain.Money;
using Domain.States;

namespace Domain;
public sealed class VendingMachine
{
    private readonly IInventoryManager _inventoryManager;
    private readonly IMoneyManager _moneyManager;

    public Item? SelectedItem { get; private set; }

    public event EventHandler<VendingMachineEventArgs>? OnMessageRaised;

    public IState CurrentState { get; set; }
    public int ChangeToDispense { get; set; }

    public VendingMachine(IInventoryManager inventoryManager, IMoneyManager moneyManager)
    {
        _inventoryManager = inventoryManager;
        _moneyManager = moneyManager;
        CurrentState = new IdleState(this);
    }

    public void SelectItem(string itemId)
    {
        var item = _inventoryManager.GetItem(itemId);

        if (item is NullItem)
        {
            RaiseEvent(new VendingMachineEventArgs("invalid selection"));
            return;
        }

        SelectedItem = item;
        CurrentState.SelectItem(SelectedItem);
    }

    public void InsertCash(int amount)
    {
        if (!_moneyManager.IsValidBillDenomination(amount))
        {
            RaiseEvent(new VendingMachineEventArgs("invalid bill denomination"));
            return;
        }

        CurrentState.InsertCash(amount);
    }

    public void RaiseEvent(VendingMachineEventArgs args)
    {
        OnMessageRaised?.Invoke(this, args);
    }
}
