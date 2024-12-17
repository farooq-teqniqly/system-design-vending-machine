namespace Domain;

public sealed class IdleState : BaseState
{
    public IdleState(
        VendingMachine vendingMachine,
        ButtonToProductMapping mapping) : base(vendingMachine, mapping)
    {
        Console.WriteLine("Type START to begin");
    }

    public override void PressStartButton()
    {
        Console.WriteLine("start button pressed");
        SetState(new ItemSelectionState(VendingMachine, Mapping));
    }

    public override void PressCancelButton()
    {
        throw new InvalidStateTransitionException();
    }

    public override void PressBackButton()
    {
        throw new InvalidStateTransitionException();
    }

    public override void PressItemSelectionButton(string buttonId)
    {
        throw new InvalidStateTransitionException();
    }
}
