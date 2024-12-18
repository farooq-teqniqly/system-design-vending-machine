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

    public IEnumerable<Item> GetAvailableItems() => _items.Values.Where(i => i.Quantity > 0);

    public Item GetItem(string itemId)
    {
        if (!_items.TryGetValue(itemId, out var item))
        {
            return new InvalidItem();
        }

        return item.Quantity == 0 ? new OutOfStockItem(item) : item;
    }

    public void AddItems(IEnumerable<Item> items)
    {
        foreach (var item in items)
        {
            _items.Add(item.ItemId, item);
        }
    }

    public void ItemSold(string itemId)
    {
        var item = _items[itemId];

        _items.Remove(itemId);
        _items.Add(itemId, new Item(itemId, item.Name, item.Price, item.Quantity - 1));
    }
}
