namespace Domain.EventArgs;
/// <summary>
/// Serves as the base class for event arguments related to vending machine events.
/// </summary>
/// <remarks>
/// This class provides a common structure for all vending machine-related event arguments,
/// encapsulating a descriptive message about the event.
/// Derived classes can extend this functionality to include additional event-specific data.
/// </remarks>
public class VendingMachineEventArgs : System.EventArgs
{
    public string Message { get; }

    public VendingMachineEventArgs(string message)
    {
        Message = message;
    }
}
