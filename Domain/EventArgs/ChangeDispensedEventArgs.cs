using Domain.Money;

namespace Domain.EventArgs;

public class ChangeDispensedEventArgs : VendingMachineEventArgs
{
    public Change Change { get; }

    public ChangeDispensedEventArgs(Change change) : base($"change dispensed: {change}")
    {
        Change = change;
    }
}
