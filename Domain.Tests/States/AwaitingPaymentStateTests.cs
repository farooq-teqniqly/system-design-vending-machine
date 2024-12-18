using Domain.Inventory;
using Domain.Money;
using Domain.States;
using FluentAssertions;
using NSubstitute;

namespace Domain.Tests.States;
public class AwaitingPaymentStateTests
{
    private readonly VendingMachine _vendingMachine;
    private readonly AwaitingPaymentState _awaitingPaymentState;
    private VendingMachineEventArgs? _args;
    private readonly IInventoryManager _mockInventoryManager = Substitute.For<IInventoryManager>();
    private readonly IMoneyManager _moneyManager = new MoneyManager();

    public AwaitingPaymentStateTests()
    {
        _vendingMachine = new VendingMachine(_mockInventoryManager, _moneyManager);
        _vendingMachine.OnMessageRaised += (sender, args) => _args = args;
        _awaitingPaymentState = new AwaitingPaymentState(_vendingMachine);
    }

    [Fact]
    public void Insert_Cash_One_Bill_Is_Enough()
    {
        // Arrange
        var item = new Item("A1", "Chips", 1.99m, 1);

        _mockInventoryManager.GetItem(item.ItemId).Returns(item);
        _vendingMachine.SelectItem(item.ItemId);

        // Act
        _awaitingPaymentState.InsertCash(5);

        // Assert
        _args.Should().NotBeNull();
        _args!.Message.Should().Be("payment complete");
        _vendingMachine.CurrentState.Should().BeOfType<DispenseState>();
    }

    [Fact]
    public void Insert_Cash_Need_Multiple_Bills()
    {
        // Arrange
        var item = new Item("A1", "Chips", 1.99m, 1);

        _mockInventoryManager.GetItem(item.ItemId).Returns(item);
        _vendingMachine.SelectItem(item.ItemId);

        // Act
        _awaitingPaymentState.InsertCash(1);

        // Assert
        _args.Should().NotBeNull();
        _args!.Should().BeOfType<InsertMoreMoneyEventArgs>();

        // Act again
        _awaitingPaymentState.InsertCash(2);
        _args.Should().NotBeNull();
        _args!.Message.Should().Be("payment complete");
    }
}
