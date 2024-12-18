using Domain.Exceptions;

namespace Domain.Inventory;
public sealed class InventoryManager : IInventoryManager
{
    private readonly Dictionary<string, Item> _items = new();

    public void AddItem(Item item)
    {
        if (!_items.TryAdd(item.ItemId, item))
        {
            throw new InventoryException($"item already added: {item}");
        }
    }

    public IEnumerable<Item> GetItems() => _items.Values;

    public Item GetItem(string itemId)
    {
        return _items.TryGetValue(itemId, out var item) ? item : new NullItem();
    }

    public void AddItems(IEnumerable<Item> items)
    {
        foreach (var item in items)
        {
            _items.Add(item.ItemId, item);
        }
    }
}
