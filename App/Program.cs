using Domain;

namespace App;

public class Program
{
    static void Main()
    {
        var machine = new VendingMachine(ConfigureButtonToProductMapping());

        var line = Console.ReadLine();

        while (string.Compare(line, "START", StringComparison.OrdinalIgnoreCase) != 0)
        {
            Console.WriteLine("Type START to begin.");
            line = Console.ReadLine();
        }

        machine.PressStartButton();

        var buttonId = Console.ReadLine();
        
        while (string.IsNullOrEmpty(buttonId))
        {
            buttonId = Console.ReadLine();
        }

        machine.PressItemSelectionButton(buttonId);


        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }

    private static ButtonToProductMapping ConfigureButtonToProductMapping()
    {
        var mapping = new ButtonToProductMapping();

        mapping.Add("A1", new Item("1", "Coke Zero", 1.99m, 10));

        mapping.Add("A2", new Item("2", "Coke Classic", 1.99m, 10));

        mapping.Add("A3", new Item("3", "Flaming Hot Cheetos", 2.59m, 5));

        mapping.Add("A4", new Item("4", "Moon Pie", 4.89m, 2));

        return mapping;
    }

}
