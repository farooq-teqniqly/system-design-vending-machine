namespace Domain.Inventory;

public interface IInventoryManager
{
    void AddItem(Item item);
    IEnumerable<Item> GetAvailableItems();
    Item GetItem(string itemId);

    void AddItems(IEnumerable<Item> items);
    void ItemSold(string itemId);
    IEnumerable<Item> GetLowInventoryItems();
}
