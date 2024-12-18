namespace Domain.Inventory;

public interface IInventoryManager
{
    void AddItem(Item item);
    IEnumerable<Item> GetItems();
    Item GetItem(string itemId);

    void AddItems(IEnumerable<Item> items);
}
