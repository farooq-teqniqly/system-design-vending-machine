namespace Domain;
public sealed class InvalidStateTransitionException : Exception
{
    public InvalidStateTransitionException() : base("invalid state transition.")
    {
        
    }
}
