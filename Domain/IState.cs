namespace Domain;

public interface IState
{
    void PressStartButton();
    void PressCancelButton();
    void PressBackButton();
    void PressItemSelectionButton(string buttonId);

}
