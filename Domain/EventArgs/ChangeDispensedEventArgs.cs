using Domain.Money;

namespace Domain.EventArgs;

/// <summary>
/// Represents the event data for when change is dispensed by the vending machine.
/// </summary>
/// <remarks>
/// This event is raised to notify that change has been dispensed, either as a result of a refund
/// or after completing a purchase. The dispensed change is encapsulated in the <see cref="Change"/> property.
/// </remarks>
public class ChangeDispensedEventArgs : VendingMachineEventArgs
{
    public Change Change { get; }

    public ChangeDispensedEventArgs(Change change) : base($"change dispensed: {change}")
    {
        Change = change;
    }
}
