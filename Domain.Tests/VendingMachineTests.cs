using Domain.Inventory;
using Domain.Money;
using FluentAssertions;

namespace Domain.Tests;
public class VendingMachineTests
{
    private readonly InventoryManager _inventoryManager = new(new InventoryManagerConfiguration(1));
    private readonly MoneyManager _moneyManager = new ();
    private readonly VendingMachine _vendingMachine;

    public VendingMachineTests()
    {
        _vendingMachine = new VendingMachine(_inventoryManager, _moneyManager);
    }

    [Fact]
    public void Can_Buy_An_Item_And_Get_Correct_Change()
    {
        // Arrange
        var item = new Item("A1", "Chips", 2.29m, 1);
        _inventoryManager.AddItem(item);

        var eventArgsList = new List<VendingMachineEventArgs>();
        _vendingMachine.OnMessageRaised += (_, args) => eventArgsList.Add(args);

        // Act
        _vendingMachine.SelectItem(item.ItemId);
        _vendingMachine.InsertCash(1);
        _vendingMachine.InsertCash(1);
        _vendingMachine.InsertCash(1);

        // Assert
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
        // Arrange
        var item = new Item("A1", "Chips", 1m, 1);
        _inventoryManager.AddItem(item);

        var eventArgsList = new List<VendingMachineEventArgs>();
        _vendingMachine.OnMessageRaised += (_, args) => eventArgsList.Add(args);

        // Act
        _vendingMachine.SelectItem(item.ItemId);
        _vendingMachine.InsertCash(1);

        // Assert
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
        // Arrange
        var item = new Item("A1", "Chips", 1m, 2);
        _inventoryManager.AddItem(item);

        // Act
        _vendingMachine.SelectItem(item.ItemId);
        _vendingMachine.InsertCash(1);

        // Assert
        _inventoryManager.GetItem(item.ItemId).Quantity.Should().Be(1);
    }

    [Fact]
    public void Can_Cancel_Transaction()
    {
        // Arrange
        var item = new Item("A1", "Chips", 2.49m, 2);
        _inventoryManager.AddItem(item);
        
        var eventArgsList = new List<VendingMachineEventArgs>();
        _vendingMachine.OnMessageRaised += (_, args) => eventArgsList.Add(args);

        // Act
        _vendingMachine.SelectItem(item.ItemId);
        _vendingMachine.InsertCash(1);
        _vendingMachine.InsertCash(1);
        _vendingMachine.CancelTransaction();

        // Assert
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
        // Arrange
        var item = new Item("A1", "Chips", 2.49m, 2);
        _inventoryManager.AddItem(item);
        
        var eventArgsList = new List<VendingMachineEventArgs>();
        _vendingMachine.OnMessageRaised += (_, args) => eventArgsList.Add(args);

        // Act
        _vendingMachine.SelectItem(item.ItemId);
        _vendingMachine.InsertCash(50);

        // Assert
        var billRejectedEventArgs = eventArgsList.Single(a => a.GetType() == typeof(BillRejectedEventArgs))
            .As<BillRejectedEventArgs>();

        billRejectedEventArgs.Denomination.Should().Be(50);
    }

    [Fact]
    public void Cannot_Select_A_Non_Existent_Item()
    {
        // Arrange
        var item = new Item("A1", "Chips", 2.49m, 2);
        _inventoryManager.AddItem(item);

        var eventArgsList = new List<VendingMachineEventArgs>();
        _vendingMachine.OnMessageRaised += (_, args) => eventArgsList.Add(args);

        // Act
        _vendingMachine.SelectItem("Z1");

        // Assert
        var invalidItemSelectedEventArgs = eventArgsList.Single(a => a.GetType() == typeof(InvalidItemSelectedEventArgs))
            .As<InvalidItemSelectedEventArgs>();

        invalidItemSelectedEventArgs.ItemId.Should().Be("Z1");
    }

    [Fact]
    public void Cannot_Select_An_Out_Of_Stock_Item()
    {
        // Arrange
        var item = new Item("A1", "Chips", 2.49m, 1);
        _inventoryManager.AddItem(item);

        var eventArgsList = new List<VendingMachineEventArgs>();
        _vendingMachine.OnMessageRaised += (_, args) => eventArgsList.Add(args);

        // Act
        _vendingMachine.SelectItem("A1");
        _vendingMachine.InsertCash(5);

        _vendingMachine.SelectItem("A1");
        _vendingMachine.InsertCash(5);

        // Assert
        var outOfStockItemEventArgs = eventArgsList.First(a => a.GetType() == typeof(OutOfStockItemEventArgs))
            .As<OutOfStockItemEventArgs>();

        var outOfStockItem = outOfStockItemEventArgs.Item;

        outOfStockItem.ItemId.Should().Be(item.ItemId);
        outOfStockItem.Quantity.Should().Be(0);
    }

    [Fact]
    public void Can_Receive_Notifications_When_Item_Is_Low_On_Stock()
    {
        // Arrange
        var item1 = new Item("A1", "Chips", 2.49m, 2);
        var item2 = new Item("A2", "Soda", 1.50m, 2);

        _inventoryManager.AddItem(item1);
        _inventoryManager.AddItem(item2);

        var eventArgsList = new List<VendingMachineEventArgs>();
        _vendingMachine.OnMessageRaised += (_, args) => eventArgsList.Add(args);

        // Act
        _vendingMachine.SelectItem("A1");
        _vendingMachine.InsertCash(5);

        _vendingMachine.SelectItem("A2");
        _vendingMachine.InsertCash(5);

        // Assert
        var lowInventoryItemEventArgs = eventArgsList.Where(a => a.GetType() == typeof(LowInventoryItemEventArgs))
            .Cast<LowInventoryItemEventArgs>()
            .ToList();

        lowInventoryItemEventArgs.Should().HaveCount(3);

        var a1Notifications = lowInventoryItemEventArgs.Where(a => a.Item.ItemId == "A1");

        a1Notifications.All(a => a.Item.Quantity == 1).Should().BeTrue();

        lowInventoryItemEventArgs.Single(a => a.Item.ItemId == "A2").Item.Quantity.Should().Be(1);


    }

    [Fact]
    public void Can_Receive_Notifications_When_Item_Is_Out_Of_Stock()
    {
        // Arrange
        var item = new Item("A1", "Chips", 2.49m, 1);

        _inventoryManager.AddItem(item);

        var eventArgsList = new List<VendingMachineEventArgs>();
        _vendingMachine.OnMessageRaised += (_, args) => eventArgsList.Add(args);

        // Act
        _vendingMachine.SelectItem("A1");
        _vendingMachine.InsertCash(5);

        // Assert
        var outOfStockItemEventArgs = eventArgsList.Single(a => a.GetType() == typeof(OutOfStockItemEventArgs))
            .As<OutOfStockItemEventArgs>();

        outOfStockItemEventArgs.Item.ItemId.Should().Be("A1");
        outOfStockItemEventArgs.Item.Quantity.Should().Be(0);

    }
}
