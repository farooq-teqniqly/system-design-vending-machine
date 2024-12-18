namespace Domain.EventArgs;
public class VendingMachineEventArgs : System.EventArgs
{
    public string Message { get; }

    public VendingMachineEventArgs(string message)
    {
        Message = message;
    }
}
