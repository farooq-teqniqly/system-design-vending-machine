namespace Domain;

public abstract class BaseState : IState
{
    protected VendingMachine VendingMachine { get; }
    protected ButtonToProductMapping Mapping { get; }

    protected BaseState(VendingMachine vendingMachine, ButtonToProductMapping mapping)
    {
        VendingMachine = vendingMachine;
        Mapping = mapping;
    }

    protected void SetState(IState newState)
    {
        VendingMachine.SetState(newState);
    }

    public abstract void PressStartButton();

    public abstract void PressCancelButton();

    public abstract void PressBackButton();

    public abstract void PressItemSelectionButton(string buttonId);
}
