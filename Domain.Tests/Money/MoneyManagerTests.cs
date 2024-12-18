using Domain.Money;
using FluentAssertions;

namespace Domain.Tests.Money;
public class MoneyManagerTests
{
    private readonly IMoneyManager _moneyManager = new MoneyManager();
    
    [Fact]
    public void DispenseChange_Greedily_Dispenses_Coins_1()
    {
        // Act
        var change = _moneyManager.DispenseChange(87);

        // Assert
        change.Should().HaveCount(3);
        change[25].Should().Be(3);
        change[10].Should().Be(1);
        change[1].Should().Be(2);
    }

    [Fact]
    public void DispenseChange_Greedily_Dispenses_Coins_2()
    {
        // Act
        var change = _moneyManager.DispenseChange(1);

        // Assert
        change.Should().HaveCount(1);
        change[1].Should().Be(1);
    }

    [Fact]
    public void DispenseChange_Greedily_Dispenses_Coins_3()
    {
        // Act
        var change = _moneyManager.DispenseChange(43);

        // Assert
        change.Should().HaveCount(4);
        change[25].Should().Be(1);
        change[10].Should().Be(1);
        change[5].Should().Be(1);
        change[1].Should().Be(3);
    }
}
