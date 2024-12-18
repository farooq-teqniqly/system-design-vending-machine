using Domain;
using Domain.Inventory;
using Domain.Money;

namespace App;

/// <summary>
/// Represents the entry point of the application.
/// </summary>
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

        var inventoryManager = new InventoryManager(new InventoryManagerConfiguration());
        inventoryManager.AddItems(items);

        var moneyManager = new MoneyManager();

        var vendingMachine = new VendingMachine(inventoryManager, moneyManager);
        vendingMachine.OnMessageRaised += (_, args) =>
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(args.Message);
            Console.ResetColor();
        };

        DisplayCommands();
        
        while (true)
        {
            DisplayAvailableItems(inventoryManager.GetAvailableItems());
            
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
        Console.WriteLine("Vending Machine Simulator!");
        Console.WriteLine("Commands:");
        Console.WriteLine(Environment.NewLine);
        Console.WriteLine("SELECT <item id> (select a single item for purchase)");
        Console.WriteLine("INSERT <cash denomination> (insert a single bill)");
        Console.WriteLine("CANCEL (cancel an in-process transaction)");
        Console.WriteLine("EXIT (exit the simulator)");
        Console.WriteLine(Environment.NewLine);
    }

    private static void DisplayAvailableItems(IEnumerable<Item> items)
    {
        Console.WriteLine("Available items:");
        Console.WriteLine(Environment.NewLine);

        foreach (var item in items)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine(Environment.NewLine);
    }
}
