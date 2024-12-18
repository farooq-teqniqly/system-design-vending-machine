namespace Domain.Inventory;
/// <summary>
/// Provides functionality for managing inventory operations within the system.
/// </summary>
/// <remarks>
/// This class is responsible for adding, retrieving, and managing items in the inventory.
/// It also handles operations such as tracking low stock, marking items as sold, and retrieving available items.
/// </remarks>
public sealed class InventoryManager : IInventoryManager
{
    private readonly InventoryManagerConfiguration _config;
    private readonly Dictionary<string, Item> _items = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="InventoryManager"/> class with the specified configuration.
    /// </summary>
    /// <param name="config">
    /// The configuration settings for the inventory manager, including thresholds for low inventory.
    /// </param>
    /// <remarks>
    /// This constructor sets up the inventory manager with the provided configuration, enabling it to manage
    /// inventory operations such as adding items, retrieving items, and tracking low inventory levels.
    /// </remarks>
    public InventoryManager(InventoryManagerConfiguration config)
    {
        _config = config;
    }

    /// <summary>
    /// Adds an item to the inventory.
    /// </summary>
    /// <param name="item">The <see cref="Item"/> to be added to the inventory.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when an item with the same <see cref="Item.ItemId"/> already exists in the inventory.
    /// </exception>
    public void AddItem(Item item)
    {
        if (!_items.TryAdd(item.ItemId, item))
        {
            throw new ArgumentException($"item already added: {item}");
        }
    }

    /// <summary>
    /// Retrieves all available items in the inventory.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="Item"/> objects that are currently available in the inventory.
    /// An item is considered available if its quantity is greater than zero.
    /// </returns>
    /// <remarks>
    /// This method filters the inventory to include only items with a positive quantity.
    /// It is useful for displaying items that can be purchased or selected.
    /// </remarks>
    public IEnumerable<Item> GetAvailableItems() => _items.Values.Where(i => i.Quantity > 0);

    /// <summary>
    /// Retrieves an item from the inventory by its unique identifier.
    /// </summary>
    /// <param name="itemId">The unique identifier of the item to retrieve.</param>
    /// <returns>
    /// The item corresponding to the specified <paramref name="itemId"/>.
    /// If the item does not exist, an instance of <see cref="InvalidItem"/> is returned.
    /// If the item exists but is out of stock, an instance of <see cref="OutOfStockItem"/> is returned.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="itemId"/> is <c>null</c>, empty, or consists only of white-space characters.
    /// </exception>
    /// <remarks>
    /// This method ensures that the inventory is queried safely and provides meaningful responses
    /// for invalid or unavailable items.
    /// </remarks>
    public Item GetItem(string itemId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(itemId);

        if (!_items.TryGetValue(itemId, out var item))
        {
            return new InvalidItem();
        }

        return item.Quantity == 0 ? new OutOfStockItem(item) : item;
    }

    /// <summary>
    /// Adds multiple items to the inventory.
    /// </summary>
    /// <param name="items">A collection of <see cref="Item"/> objects to be added to the inventory.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="items"/> parameter is <c>null</c>.</exception>
    /// <remarks>
    /// This method iterates through the provided collection of items and adds each item to the inventory.
    /// Duplicate item IDs are not handled and may result in an exception.
    /// </remarks>
    public void AddItems(IEnumerable<Item> items)
    {
        ArgumentNullException.ThrowIfNull(items);

        foreach (var item in items)
        {
            _items.Add(item.ItemId, item);
        }
    }

    /// <summary>
    /// Marks an item in the inventory as sold by decrementing its quantity.
    /// </summary>
    /// <param name="itemId">The unique identifier of the item to be marked as sold.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when the <paramref name="itemId"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    /// <exception cref="KeyNotFoundException">
    /// Thrown when the item with the specified <paramref name="itemId"/> does not exist in the inventory.
    /// </exception>
    /// <remarks>
    /// This method updates the inventory by reducing the quantity of the specified item by one.
    /// If the item's quantity reaches zero, it remains in the inventory with a quantity of zero.
    /// </remarks>
    public void ItemSold(string itemId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(itemId);

        var item = _items[itemId];

        _items.Remove(itemId);
        _items.Add(itemId, new Item(itemId, item.Name, item.Price, item.Quantity - 1));
    }

    /// <summary>
    /// Retrieves items from the inventory that have a quantity less than or equal to the low inventory threshold.
    /// </summary>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of <see cref="Item"/> objects representing items with low inventory levels.
    /// </returns>
    /// <remarks>
    /// This method identifies items in the inventory whose quantity is less than or equal to the threshold 
    /// specified in the <see cref="InventoryManagerConfiguration.LowInventoryThreshold"/> property.
    /// It is useful for tracking and managing items that are running low in stock.
    /// </remarks>
    public IEnumerable<Item> GetLowInventoryItems()
    {
        return _items.Where(keyValuePairs => keyValuePairs.Value.Quantity <= _config.LowInventoryThreshold)
            .Select(keyValuePair => keyValuePair.Value);
    }
}
