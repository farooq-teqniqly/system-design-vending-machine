namespace Domain.Inventory;

public interface IInventoryManager
{
    void AddItem(Item item);
    IEnumerable<Item> GetItems();
    bool ItemExists(string itemId);
}
