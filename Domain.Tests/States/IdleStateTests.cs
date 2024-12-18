using Domain.Inventory;
using Domain.Money;
using Domain.States;
using FluentAssertions;
using NSubstitute;

namespace Domain.Tests.States;

public class IdleStateTests
{
    private readonly VendingMachine _vendingMachine;
    private readonly IdleState _idleState;
    private VendingMachineEventArgs? _args;
    private readonly IInventoryManager _mockInventoryManager = Substitute.For<IInventoryManager>();
    private readonly IMoneyManager _moneyManager = new MoneyManager();

    public IdleStateTests()
    {
        _vendingMachine = new VendingMachine(_mockInventoryManager, _moneyManager);
        _vendingMachine.OnMessageRaised += (sender, args) => _args = args;
        _idleState = new IdleState(_vendingMachine);
    }

    [Fact]
    public void Selecting_Item_Transitions_Machine_To_AwaitingPayment_State()
    {
        // Arrange
        var item = new Item("A1", "Chips", 1.99m, 1);

        _mockInventoryManager.GetItem(item.ItemId).Returns(item);

        // Act
        _idleState.SelectItem(item);

        // Assert
        _args.Should().NotBeNull();
        _args!.Message.Should().Contain(item.ItemId);
        _vendingMachine.CurrentState.Should().BeOfType<AwaitingPaymentState>();
    }
}
