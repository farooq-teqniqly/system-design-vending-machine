using System.Text;

namespace Domain.Money;

public class Change
{
    public int Quarters { get; set; }
    public int Dimes { get; set; }
    public int Nickels { get; set; }

    public int Pennies { get; set; }

    public Change(int quarters, int dimes, int nickels, int pennies)
    {
        Quarters = quarters;
        Dimes = dimes;
        Nickels = nickels;
        Pennies = pennies;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Quarters: {Quarters}");
        sb.AppendLine($"Dimes: {Dimes}");
        sb.AppendLine($"Nickels: {Nickels}");
        sb.AppendLine($"Pennies: {Pennies}");

        return sb.ToString();
    }
}
