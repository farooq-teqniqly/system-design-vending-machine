using Domain.Inventory;
using Domain.Money;
using FluentAssertions;

namespace Domain.Tests;
public class VendingMachineTests
{
    private readonly InventoryManager _inventoryManager = new();
    private readonly MoneyManager _moneyManager = new ();
    private readonly VendingMachine _vendingMachine;

    public VendingMachineTests()
    {
        _vendingMachine = new VendingMachine(_inventoryManager, _moneyManager);
    }
    [Fact]
    public void Can_Buy_An_Item_And_Get_Correct_Change()
    {
        var item = new Item("A1", "Chips", 2.29m, 1);
        _inventoryManager.AddItem(item);

        var eventArgsList = new List<VendingMachineEventArgs>();
        _vendingMachine.OnMessageRaised += (sender, args) => eventArgsList.Add(args);

        _vendingMachine.SelectItem(item.ItemId);
        _vendingMachine.InsertCash(1);
        _vendingMachine.InsertCash(1);
        _vendingMachine.InsertCash(1);

        eventArgsList.Single(a => a.GetType() == typeof(ItemSelectedEventArgs)).As<ItemSelectedEventArgs>().Item
            .Should().Be(item);

        var paymentCompleteEventArgs = eventArgsList.Single(a => a.GetType() == typeof(PaymentCompleteEventArgs))
            .As<PaymentCompleteEventArgs>();

        paymentCompleteEventArgs.Amount.Should().Be(3);
        paymentCompleteEventArgs.ChangeDue.Should().Be(0.71m);

        var changeDispensedEventArgs = eventArgsList.Single(a => a.GetType() == typeof(ChangeDispensedEventArgs))
            .As<ChangeDispensedEventArgs>();


        var change = changeDispensedEventArgs.Change;
        change.Quarters.Should().Be(2);
        change.Dimes.Should().Be(2);
        change.Nickels.Should().Be(0);
        change.Pennies.Should().Be(1);
    }

    [Fact]
    public void Can_Buy_An_Item_With_No_Change_Back()
    {
        var item = new Item("A1", "Chips", 1m, 1);
        _inventoryManager.AddItem(item);

        var eventArgsList = new List<VendingMachineEventArgs>();
        _vendingMachine.OnMessageRaised += (sender, args) => eventArgsList.Add(args);

        _vendingMachine.SelectItem(item.ItemId);
        _vendingMachine.InsertCash(1);

        var paymentCompleteEventArgs = eventArgsList.Single(a => a.GetType() == typeof(PaymentCompleteEventArgs))
            .As<PaymentCompleteEventArgs>();

        paymentCompleteEventArgs.Amount.Should().Be(1);
        paymentCompleteEventArgs.ChangeDue.Should().Be(0);

        var changeDispensedEventArgs = eventArgsList.Single(a => a.GetType() == typeof(ChangeDispensedEventArgs))
            .As<ChangeDispensedEventArgs>();

        changeDispensedEventArgs.Change.Should().BeOfType<NoChange>();
    }

    [Fact]
    public void Quantity_Is_Updated_After_Item_Dispensed()
    {
        var item = new Item("A1", "Chips", 1m, 2);
        _inventoryManager.AddItem(item);

        
        _vendingMachine.SelectItem(item.ItemId);
        _vendingMachine.InsertCash(1);

        _inventoryManager.GetItem(item.ItemId).Quantity.Should().Be(1);
    }

    [Fact]
    public void Can_Cancel_Transaction()
    {
        var item = new Item("A1", "Chips", 2.49m, 2);
        _inventoryManager.AddItem(item);
        
        var eventArgsList = new List<VendingMachineEventArgs>();
        _vendingMachine.OnMessageRaised += (sender, args) => eventArgsList.Add(args);

        _vendingMachine.SelectItem(item.ItemId);
        _vendingMachine.InsertCash(1);
        _vendingMachine.InsertCash(1);
        _vendingMachine.CancelTransaction();

        var changeDispensedEventArgs = eventArgsList.Single(a => a.GetType() == typeof(ChangeDispensedEventArgs))
            .As<ChangeDispensedEventArgs>();

        var refund = changeDispensedEventArgs.Change;

        refund.Quarters.Should().Be(8);
        refund.Dimes.Should().Be(0);
        refund.Nickels.Should().Be(0);
        refund.Pennies.Should().Be(0);
    }

    [Fact]
    public void Invalid_Bill_Denominations_Are_Rejected()
    {
        var item = new Item("A1", "Chips", 2.49m, 2);
        _inventoryManager.AddItem(item);
        
        var eventArgsList = new List<VendingMachineEventArgs>();
        _vendingMachine.OnMessageRaised += (sender, args) => eventArgsList.Add(args);

        _vendingMachine.SelectItem(item.ItemId);
        _vendingMachine.InsertCash(50);

        var billRejectedEventArgs = eventArgsList.Single(a => a.GetType() == typeof(BillRejectedEventArgs))
            .As<BillRejectedEventArgs>();

        billRejectedEventArgs.Denomination.Should().Be(50);
    }
}
