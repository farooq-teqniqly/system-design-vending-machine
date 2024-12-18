# Vending Machine Simulator

## Overview

This application simulates a vending machine. Users can select items, insert cash, cancel transactions, and exit the simulator. The application provides feedback on the operations performed.

## How to Run

1. Ensure you have .NET 8 installed on your machine.
2. Navigate to the solution folder.
3. Build and run the application using the following commands:

```bash
dotnet restore
dotnet build
dotnet run --project .\App\App.csproj
```

## Commands

The vending machine simulator accepts the following commands:

-   **SELECT `<item id>`**: Select an item for purchase by specifying its ID.
-   **INSERT `<cash denomination>`**: Insert a bill into the vending machine by specifying the denomination. Enter `1` to insert a one dollar bill.
-   **CANCEL**: Cancel the current transaction.
-   **EXIT**: Exit the vending machine simulator.

## Example Usage

1. **Start the Simulator**: Run the application to start the vending machine simulator.
2. **Display Available Items**: The simulator will display a list of available items.
3. **Select an Item**: Type `SELECT A1` to select the item with ID `A1`.
4. **Insert Cash**: Type `INSERT 2` to insert a $2 bill.
5. **Cancel Transaction**: Type `CANCEL` to cancel the current transaction.
6. **Exit the Simulator**: Type `EXIT` to exit the simulator.

## Sample Session

```bash
Vending Machine Simulator!
Commands:


SELECT <item id> (select a single item for purchase)
INSERT <cash denomination> (insert a single bill)
CANCEL (cancel an in-process transaction)
EXIT (exit the simulator)


Available items:


Item { ItemId = A1, Name = Chips, Price = 1.99, Quantity = 3 }
Item { ItemId = A2, Name = Soda, Price = 1.50, Quantity = 3 }
Item { ItemId = A3, Name = Cookies, Price = 4.39, Quantity = 3 }
Item { ItemId = A4, Name = Gum, Price = 0.89, Quantity = 3 }


enter command:

select a4
item selected: Item { ItemId = A4, Name = Gum, Price = 0.89, Quantity = 3 }
Available items:


Item { ItemId = A1, Name = Chips, Price = 1.99, Quantity = 3 }
Item { ItemId = A2, Name = Soda, Price = 1.50, Quantity = 3 }
Item { ItemId = A3, Name = Cookies, Price = 4.39, Quantity = 3 }
Item { ItemId = A4, Name = Gum, Price = 0.89, Quantity = 3 }


enter command:

insert 1
bill accepted; amount: 1
payment complete; amount: 1; change due: 0.11
item dispensed: Item { ItemId = A4, Name = Gum, Price = 0.89, Quantity = 3 }
item inventory low: Item { ItemId = A4, Name = Gum, Price = 0.89, Quantity = 2 }
change dispensed: Quarters: 0
Dimes: 1
Nickels: 0
Pennies: 1

Available items:


Item { ItemId = A1, Name = Chips, Price = 1.99, Quantity = 3 }
Item { ItemId = A2, Name = Soda, Price = 1.50, Quantity = 3 }
Item { ItemId = A3, Name = Cookies, Price = 4.39, Quantity = 3 }
Item { ItemId = A4, Name = Gum, Price = 0.89, Quantity = 2 }

select a1
item selected: Item { ItemId = A1, Name = Chips, Price = 1.99, Quantity = 3 }
Available items:


Item { ItemId = A1, Name = Chips, Price = 1.99, Quantity = 3 }
Item { ItemId = A2, Name = Soda, Price = 1.50, Quantity = 3 }
Item { ItemId = A3, Name = Cookies, Price = 4.39, Quantity = 3 }
Item { ItemId = A4, Name = Gum, Price = 0.89, Quantity = 2 }


enter command:
insert 1
bill accepted; amount: 1
an additional 0.99 is needed; total amount inserted: 1
Available items:


Item { ItemId = A1, Name = Chips, Price = 1.99, Quantity = 3 }
Item { ItemId = A2, Name = Soda, Price = 1.50, Quantity = 3 }
Item { ItemId = A3, Name = Cookies, Price = 4.39, Quantity = 3 }
Item { ItemId = A4, Name = Gum, Price = 0.89, Quantity = 2 }


enter command:

cancel
transaction cancelled
change dispensed: Quarters: 4
Dimes: 0
Nickels: 0
Pennies: 0

Available items:


Item { ItemId = A1, Name = Chips, Price = 1.99, Quantity = 3 }
Item { ItemId = A2, Name = Soda, Price = 1.50, Quantity = 3 }
Item { ItemId = A3, Name = Cookies, Price = 4.39, Quantity = 3 }
Item { ItemId = A4, Name = Gum, Price = 0.89, Quantity = 2 }

exit
thanks for using the vending machine
```

## Notes

-   Ensure to enter commands exactly as specified.
-   The simulator will provide feedback for each operation, such as invalid commands or successful transactions.
-   The application will continue to run until the `EXIT` command is issued.

## Tests

There are integration tests covering the following:

-   State transitions and event handling.
-   Edge case tests for invalid inputs and out of sequence operations.

The tests use the FluentAssertions library for assertions.

## How to Run

1. Navigate to the solution folder.
2. Run the tests using the following commands:

```bash
dotnet restore
dotnet test
```

