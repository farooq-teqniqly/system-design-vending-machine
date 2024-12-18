using System.IO.Enumeration;
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

        switch (item)
        {
            case InvalidItem:
                RaiseEvent(new InvalidItemSelectedEventArgs(itemId));
                return;
            case OutOfStockItem:
                RaiseEvent(new OutOfStockItemEventArgs(item));
                break;
        }

        SelectedItem = item;
        CurrentState.SelectItem(SelectedItem);
    }

    public void InsertCash(int amount)
    {
        if (!_moneyManager.IsValidBillDenomination(amount))
        {
            RaiseEvent(new BillRejectedEventArgs(amount));
            return;
        }

        CurrentState.InsertCash(amount);
    }

    public void DispenseItem()
    {
        OnMessageRaised?.Invoke(this, new ItemDispensedEventArgs(SelectedItem!));

        _inventoryManager.ItemSold(SelectedItem!.ItemId);
        var lowInventoryItems = _inventoryManager.GetLowInventoryItems();

        foreach (var lowInventoryItem in lowInventoryItems)
        {
            OnMessageRaised?.Invoke(this, new LowInventoryItemEventArgs(lowInventoryItem));
        }
    }

    public void DispenseChange(int amount)
    {
        Refund(amount);
    }

    public void Refund(int amount)
    {
        if (amount == 0)
        {
            OnMessageRaised?.Invoke(this, new ChangeDispensedEventArgs(new NoChange()));
            return;
        }

        var change = _moneyManager.MakeChange(amount);

        OnMessageRaised?.Invoke(this, new ChangeDispensedEventArgs(change));
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
