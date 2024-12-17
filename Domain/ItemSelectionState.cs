namespace Domain;

public sealed class ItemSelectionState : BaseState
{
    private Item _selectedItem = new NullItem();

    public ItemSelectionState(
        VendingMachine vendingMachine,
        ButtonToProductMapping mapping): base(vendingMachine, mapping)
    {
        Console.WriteLine("Select an item from the list below:");
        Console.WriteLine("==================================");
        Console.WriteLine(Mapping.ToString());
        Console.WriteLine("==================================");
    }

    public override void PressStartButton()
    {
        throw new InvalidStateTransitionException();
    }

    public override void PressCancelButton()
    {
        Console.WriteLine("Transaction cancelled.");
        SetState(new IdleState(VendingMachine, Mapping));
    }

    public override void PressBackButton()
    {
        if (_selectedItem is NullItem)
        {
            throw new InvalidStateTransitionException();
        }

        _selectedItem = new NullItem();
        SetState(new ItemSelectionState(VendingMachine, Mapping));
    }

    public override void PressItemSelectionButton(string buttonId)
    {
        if (_selectedItem is not NullItem)
        {
            throw new InvalidStateTransitionException();
        }

        var item = Mapping.GetItem(buttonId);

        if (item is NullItem)
        {
            Console.WriteLine("Invalid item selection.");
            SetState(new ItemSelectionState(VendingMachine, Mapping));
            return;
        }

        _selectedItem = item;
        Console.WriteLine($"You selected\t{_selectedItem.Description}\t{_selectedItem.UnitPrice}");
        //SetState(new CollectPaymentState());
    }
}
