using Domain.Exceptions;

namespace Domain.Inventory;
public sealed class InventoryManager : IInventoryManager
{
    private readonly Dictionary<string, Item> _items = new();

    public void AddItem(Item item)
    {
        if (!_items.TryAdd(item.ItemId, item))
        {
            throw new InventoryException($"item with id {item.ItemId} has already been added");
        }
    }

    public IEnumerable<Item> GetItems() => _items.Values;

    public bool ItemExists(string itemId) => _items.ContainsKey(itemId);
}
