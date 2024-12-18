namespace Domain.Money;

public sealed class NoChange : Change
{
    public NoChange() : base(0, 0, 0, 0)
    {
    }
}
