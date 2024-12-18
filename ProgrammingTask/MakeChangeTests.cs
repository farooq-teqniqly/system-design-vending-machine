using FluentAssertions;

namespace ProgrammingTask;
public class MakeChangeTests
{
    [Theory]
    [InlineData(-10, 10)]
    [InlineData(10, -10)]
    public void Negative_Amounts_For_Parameters_Throws_Exception(int centsDeposited, int sodaCostInCents)
    {
        // Act
        var act = () => Change.MakeChange(centsDeposited, sodaCostInCents);

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(10, 10)]
    [InlineData(10, 20)]
    public void When_No_Change_Due_All_Coins_Are_Zero(int centsDeposited, int sodaCostInCents)
    {
        // Act
        var change = Change.MakeChange(centsDeposited, sodaCostInCents);

        // Assert
        change.Quarters.Should().Be(0);
        change.Dimes.Should().Be(0);
        change.Nickels.Should().Be(0);
    }

    [Theory]
    [InlineData(125, 100, 1, 0, 0)]
    [InlineData(150, 100, 2, 0, 0)]
    [InlineData(90, 60, 1, 0, 1)]
    [InlineData(155, 100, 2, 0, 1)]
    [InlineData(170, 100, 2, 2, 0)]
    [InlineData(80, 40, 1, 1, 1)]
    [InlineData(20, 5, 0, 1, 1)]
    [InlineData(100, 95, 0, 0, 1)]
    public void MakeChange_With_Valid_Values_Returns_Expected_Change(
        int centsDeposited,
        int sodaCostInCents,
        int expectedQuarters,
        int expectedDimes,
        int expectedNickels)
    {
        // Act
        var result = Change.MakeChange(centsDeposited, sodaCostInCents);

        // Assert
        result.Quarters.Should().Be(expectedQuarters);
        result.Dimes.Should().Be(expectedDimes);
        result.Nickels.Should().Be(expectedNickels);
    }

    [Theory]
    [InlineData(125, 101)]
    [InlineData(150, 101)]
    [InlineData(101, 78)]
    public void MakeChange_InvalidValues_ThrowsInvalidOperationException(int centsDeposited, int sodaCostInCents)
    {
        // Act
        var act = () => Change.MakeChange(centsDeposited, sodaCostInCents);

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }
}
