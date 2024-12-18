namespace Domain;
public class VendingMachineEventArgs : EventArgs
{
    public string Message { get; }

    public VendingMachineEventArgs(string message)
    {
        Message = message;
    }
}
