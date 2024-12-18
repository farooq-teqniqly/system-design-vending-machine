using Domain.Inventory;
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

    public IdleStateTests()
    {
        _vendingMachine = new VendingMachine(_mockInventoryManager);
        _vendingMachine.OnMessageRaised += (sender, args) => _args = args;
        _idleState = new IdleState(_vendingMachine, _mockInventoryManager);
    }

    [Fact]
    public void Selecting_Item_Transitions_Machine_To_AwaitingPayment_State()
    {
        // Arrange
        var itemId = "A1";
        _mockInventoryManager.ItemExists(itemId).Returns(true);

        // Act
        _idleState.SelectItem(itemId);

        // Assert
        _args.Should().NotBeNull();
        _args!.Message.Should().Contain(itemId);
        _vendingMachine.CurrentState.Should().BeOfType<AwaitPaymentState>();
    }
}
