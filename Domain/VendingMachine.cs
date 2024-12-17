namespace Domain;

public sealed class VendingMachine
{
    public IState CurrentState { get; private set; }

    public VendingMachine(ButtonToProductMapping mapping)
    {
        CurrentState = new IdleState(this, mapping);
    }

    public void PressStartButton()
    {
        CurrentState.PressStartButton();
    }

    public void PressItemSelectionButton(string buttonId)
    {
        CurrentState.PressItemSelectionButton(buttonId);
    }

    public void SetState(IState newState)
    {
        CurrentState = newState;
    }
}
