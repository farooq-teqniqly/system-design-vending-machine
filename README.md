# system-design-vending-machine

## PlantUML Diagrams

### State Diagram

```plantuml
@startuml

[*] --> Idle : Start

Idle --> AwaitingPayment : Select a single item
AwaitingPayment --> Dispensing : Enough cash received
AwaitingPayment --> Idle : Transaction canceled (refund)
Dispensing --> Idle : Dispense item and give change
Dispensing --> NotifySysadmin : Item(s) inventory low
NotifySysadmin --> Idle : Sysadmin notified

Idle : Waiting for user input
AwaitingPayment : Waiting for enough cash or issue refund
Dispensing : Dispensing item/change and checking inventory
NotifySysadmin : Low stock notification

@enduml
```

### Class Diagram
```plantuml
@startuml

class VendingMachine {
    - IInventoryManager _inventoryManager
    - IMoneyManager _moneyManager
    + Item SelectedItem
    + IState CurrentState
    + event OnMessageRaised : EventHandler<VendingMachineEventArgs>?
    --
    + VendingMachine(IInventoryManager inventoryManager, IMoneyManager moneyManager)
    + void SelectItem(string itemId)
    + void InsertCash(int amount)
    + void DispenseItem()
    + void DispenseChange(int amount)
    + void Refund(int amount)
    + void CancelTransaction()
    + void RaiseEvent(VendingMachineEventArgs args)
}

class Item <<record>> {
    + string ItemId
    + string Name
    + decimal Price
    + int Quantity
}

class InvalidItem extends Item
class NullItem extends Item
class OutOfStockItem extends Item

interface IInventoryManager {
    + void AddItem(Item item)
    + IEnumerable<Item> GetAvailableItems()
    + Item GetItem(string itemId)
    + void AddItems(IEnumerable<Item> items)
    + void ItemSold(string itemId)
    + IEnumerable<Item> GetLowInventoryItems()
}

class InventoryManager implements IInventoryManager

class InventoryManagerConfiguration {
    + int LowInventoryThreshold { get; }
    --
    + InventoryManagerConfiguration(int lowInventoryThreshold=2)
}

InventoryManager --> InventoryManagerConfiguration

interface IMoneyManager {
    + bool IsValidBillDenomination(int amount)
    + Change MakeChange(int amount)
}

class MoneyManager implements IMoneyManager

interface IState {
    + void SelectItem(Item item)
    + void InsertCash(int amount)
    + void CancelTransaction()
}

class IdleState implements IState

class AwaitingPaymentState implements IState


class Change {
    + int Quarters { get; }
    + int Dimes { get; }
    + int Nickels { get; }
    + int Pennies { get; }
    --
    + Change(int quarters, int dimes, int nickels, int pennies)
    + string ToString()
}

class NoChange extends Change

@enduml


```


### Events
```plantuml
@startuml

class VendingMachineEventArgs {
    + string Message { get; }
    --
    + VendingMachineEventArgs(string message)
}

class VendingMachineEventArgs extends System.EventArgs
class BillAcceptedEventArgs extends VendingMachineEventArgs
class BillRejectedEventArgs extends VendingMachineEventArgs
class ChangeDispensedEventArgs extends VendingMachineEventArgs
class InsertMoreMoneyEventArgs extends VendingMachineEventArgs

class InvalidItemSelectedEventArgs extends VendingMachineEventArgs
class ItemAlreadySelectedEventArgs extends VendingMachineEventArgs
class ItemDispensedEventArgs extends VendingMachineEventArgs
class ItemSelectedEventArgs extends VendingMachineEventArgs
class LowInventoryItemEventArgs extends VendingMachineEventArgs
class PaymentCompleteEventArgs extends VendingMachineEventArgs
class TransactionCancelledEventArgs extends VendingMachineEventArgs

@enduml

```
