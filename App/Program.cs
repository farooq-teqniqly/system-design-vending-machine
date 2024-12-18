using Domain;
using Domain.Inventory;
using Domain.Money;

namespace App;

public class Program
{
    static void Main()
    {
        var items = new List<Item>()
        {
            new("A1", "Chips", 1.99m, 3),
            new("A2", "Soda", 1.50m, 3),
            new ("A3", "Cookies", 4.39m, 3),
            new ("A4", "Gum", 0.89m, 3)
        };

        var twoParamCommands = new[] { "select", "insert" };

        var inventoryManager = new InventoryManager(new InventoryManagerConfiguration(2));
        inventoryManager.AddItems(items);

        var moneyManager = new MoneyManager();

        var vendingMachine = new VendingMachine(inventoryManager, moneyManager);
        vendingMachine.OnMessageRaised += (_, args) => Console.WriteLine(args.Message);

        DisplayCommands();
        Console.WriteLine(Environment.NewLine);

        while (true)
        {
            DisplayAvailableItems(inventoryManager.GetAvailableItems());
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("enter command: ");
            var input = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                continue;
            }

            var command = input.Split(" ", StringSplitOptions.TrimEntries);

            if (twoParamCommands.Contains(command[0]) && command.Length < 2)
            {
                Console.WriteLine("invalid command");
                continue;
            }

            switch (command[0].ToLower())
            {
                case "select":
                    vendingMachine.SelectItem(command[1].ToUpper());
                    break;
                case "insert":
                    if (!int.TryParse(command[1], out var amount))
                    {
                        Console.WriteLine("invalid amount");
                    }
                    else
                    {
                        vendingMachine.InsertCash(amount);
                    }

                    break;
                case "cancel":
                    vendingMachine.CancelTransaction();
                    break;
                case "exit":
                    Console.WriteLine("thanks for using the vending machine");
                    return;
                default:
                    Console.WriteLine("invalid command");
                    break;
            }
        }
    }

    private static void DisplayCommands()
    {
        Console.WriteLine("Vending Machine 1.0!");
        Console.WriteLine("Commands:\n");
        Console.WriteLine("SELECT <item id>");
        Console.WriteLine("INSERT <cash denomination>");
        Console.WriteLine("VIEW");
        Console.WriteLine("CANCEL");
        Console.WriteLine("EXIT");
    }

    private static void DisplayAvailableItems(IEnumerable<Item> items)
    {
        Console.WriteLine("Available items:\n");

        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }
}
