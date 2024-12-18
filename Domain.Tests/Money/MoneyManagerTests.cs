using Domain.Money;
using FluentAssertions;

namespace Domain.Tests.Money;
public class MoneyManagerTests
{
    private readonly MoneyManager _moneyManager = new();
    
    [Fact]
    public void MakeChange_Greedily_Dispenses_Coins_1()
    {
        // Act
        var change = _moneyManager.MakeChange(87);

        // Assert
        change.Quarters.Should().Be(3);
        change.Dimes.Should().Be(1);
        change.Nickels.Should().Be(0);
        change.Pennies.Should().Be(2);
    }

    [Fact]
    public void DispenseChange_Greedily_Dispenses_Coins_2()
    {
        // Act
        var change = _moneyManager.MakeChange(1);

        // Assert
        change.Pennies.Should().Be(1);
    }

    [Fact]
    public void DispenseChange_Greedily_Dispenses_Coins_3()
    {
        // Act
        var change = _moneyManager.MakeChange(43);

        // Assert
        change.Quarters.Should().Be(1);
        change.Dimes.Should().Be(1);
        change.Nickels.Should().Be(1);
        change.Pennies.Should().Be(3);
    }
}
