# system-design-vending-machine

## PlantUML Diagrams

[Online Editor](https://www.planttext.com/)
[State Machine Diagrams](https://www.baeldung.com/cs/uml-state-diagrams)

## State Diagram

```plantuml
@startuml
skinparam DefaultFontSize 8

[*] --> IdleState

IdleState : Waiting for user interaction
IdleState --> AwaitingPaymentState : SelectItem(item)
IdleState --> IdleState : CancelTransaction()

AwaitingPaymentState : Waiting for payment
AwaitingPaymentState --> IdleState : CancelTransaction()\nRefund(amount)
AwaitingPaymentState --> IdleState : Payment complete\nDispenseItem()\nDispenseChange()
AwaitingPaymentState --> AwaitingPaymentState : InsertCash(amount < itemPrice)
@enduml

```

## Class Diagram
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


## Events
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


## Activity Diagrams for Vending Machine

### SelectItem
```plantuml
@startuml
actor User
participant VendingMachine
participant InventoryManager
participant CurrentState

User -> VendingMachine: SelectItem(itemId)
VendingMachine -> VendingMachine: ArgumentException.ThrowIfNullOrWhiteSpace(itemId)

alt SelectedItem is not NullItem
    VendingMachine -> VendingMachine: RaiseEvent(ItemAlreadySelectedEventArgs)
    return
end

VendingMachine -> InventoryManager: GetItem(itemId)
InventoryManager --> VendingMachine: item

alt item is InvalidItem
    VendingMachine -> VendingMachine: RaiseEvent(InvalidItemSelectedEventArgs)
    return
else item is OutOfStockItem
    VendingMachine -> VendingMachine: RaiseEvent(OutOfStockItemEventArgs)
end

VendingMachine -> VendingMachine: SelectedItem = item
VendingMachine -> CurrentState: SelectItem(SelectedItem)
@enduml

```

### Insert Cash
```plantuml
@startuml
actor User
participant VendingMachine
participant MoneyManager
participant CurrentState

User -> VendingMachine: InsertCash(amount)
VendingMachine -> VendingMachine: ArgumentOutOfRangeException.ThrowIfNegative(amount)

alt Invalid Bill Denomination
    VendingMachine -> MoneyManager: IsValidBillDenomination(amount)
    MoneyManager --> VendingMachine: false
    VendingMachine -> VendingMachine: RaiseEvent(BillRejectedEventArgs)
    return
else Valid Bill Denomination
    VendingMachine -> MoneyManager: IsValidBillDenomination(amount)
    MoneyManager --> VendingMachine: true
    VendingMachine -> CurrentState: InsertCash(amount)
end
@enduml

```

## Refund
```plantuml
@startuml
actor User
participant VendingMachine
participant MoneyManager

User -> VendingMachine: Refund(amount)
VendingMachine -> VendingMachine: ArgumentOutOfRangeException.ThrowIfNegative(amount)

alt amount == 0
    VendingMachine -> VendingMachine: OnMessageRaised(ChangeDispensedEventArgs(new NoChange()))
    return
else amount > 0
    VendingMachine -> MoneyManager: MakeChange(amount)
    MoneyManager --> VendingMachine: change
    VendingMachine -> VendingMachine: OnMessageRaised(ChangeDispensedEventArgs(change))
end
@enduml

```

## Sequence Diagrams for IState Implementations

### IdleState

#### SelectItem
```plantuml
@startuml
actor User
participant IdleState
participant VendingMachine
participant AwaitingPaymentState

User -> IdleState: SelectItem(item)
IdleState -> IdleState: ArgumentNullException.ThrowIfNull(item)
IdleState -> VendingMachine: RaiseEvent(ItemSelectedEventArgs)
VendingMachine -> VendingMachine: CurrentState = new AwaitingPaymentState()
@enduml

```

#### CancelTransaction
```plantuml
@startuml
actor User
participant IdleState
participant VendingMachine

User -> IdleState: CancelTransaction()
IdleState -> VendingMachine: CurrentState = new IdleState()
@enduml

```

### AwaitingPaymentState

#### InsertCash
```plantuml
@startuml
actor User
participant AwaitingPaymentState
participant VendingMachine

User -> AwaitingPaymentState: InsertCash(amount)
AwaitingPaymentState -> AwaitingPaymentState: ArgumentOutOfRangeException.ThrowIfNegative(amount)
AwaitingPaymentState -> AwaitingPaymentState: _totalInsertedAmount += amount
AwaitingPaymentState -> VendingMachine: RaiseEvent(BillAcceptedEventArgs(amount))

alt _totalInsertedAmount < itemPrice
    AwaitingPaymentState -> VendingMachine: RaiseEvent(InsertMoreMoneyEventArgs(totalInserted, needed))
else _totalInsertedAmount >= itemPrice
    AwaitingPaymentState -> AwaitingPaymentState: Calculate change
    AwaitingPaymentState -> VendingMachine: RaiseEvent(PaymentCompleteEventArgs(totalInserted, change))
    AwaitingPaymentState -> VendingMachine: DispenseItem()
    AwaitingPaymentState -> VendingMachine: DispenseChange(change)
    AwaitingPaymentState -> VendingMachine: CurrentState = new IdleState()
end
@enduml

```

#### CancelTransaction
```plantuml
@startuml
actor User
participant AwaitingPaymentState
participant VendingMachine

User -> AwaitingPaymentState: CancelTransaction()
AwaitingPaymentState -> VendingMachine: Refund(_totalInsertedAmount * 100)
AwaitingPaymentState -> VendingMachine: CurrentState = new IdleState()
@enduml
```
