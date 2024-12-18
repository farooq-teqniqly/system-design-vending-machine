using System.Text;
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
            RaiseEvent(new VendingMachineEventArgs("bill rejected - invalid denomination"));
            return;
        }

        CurrentState.InsertCash(amount);
    }

    public void DispenseItem()
    {
        OnMessageRaised?.Invoke(this, new VendingMachineEventArgs($"item dispensed: {SelectedItem}"));

        _inventoryManager.ItemSold(SelectedItem!.ItemId);
    }

    public void DispenseChange(int change)
    {
        Refund(change);
    }

    public void Refund(decimal totalInsertedAmount)
    {
        var refundInCents = (int)totalInsertedAmount * 100;
        Refund(refundInCents);
    }
    public void Refund(int amount)
    {
        var change = _moneyManager.DispenseChange(amount);

        var sb = new StringBuilder("change dispensed (Denomination, Number of coins):\n");

        foreach (var keyValuePair in change)
        {
            sb.AppendLine($"({keyValuePair.Key}, {keyValuePair.Value})");
        }

        OnMessageRaised?.Invoke(this, new VendingMachineEventArgs(sb.ToString()));
    }

    public void CancelTransaction()
    {
        CurrentState.CancelTransaction();
    }

    public void RaiseEvent(VendingMachineEventArgs args)
    {
        OnMessageRaised?.Invoke(this, args);
    }

}
