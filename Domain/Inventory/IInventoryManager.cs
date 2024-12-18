namespace Domain.Inventory;

/// <summary>
/// Defines the contract for managing inventory operations within the system.
/// </summary>
/// <remarks>
/// This interface provides methods to add, retrieve, and manage items in the inventory,
/// as well as to handle inventory-related operations such as tracking low stock and marking items as sold.
/// </remarks>
public interface IInventoryManager
{
    /// <summary>
    /// Adds a new item to the inventory.
    /// </summary>
    /// <param name="item">The <see cref="Item"/> to be added to the inventory. Must not be <c>null</c>.</param>
    void AddItem(Item item);

    /// <summary>
    /// Retrieves all items in the inventory that are currently available for purchase.
    /// </summary>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of <see cref="Item"/> objects representing the available items.
    /// Each item will have a <see cref="Item.Quantity"/> greater than zero.
    /// </returns>
    IEnumerable<Item> GetAvailableItems();

    /// <summary>
    /// Retrieves an item from the inventory based on its unique identifier.
    /// </summary>
    /// <param name="itemId">The unique identifier of the item to retrieve. Must not be <c>null</c>, empty, or whitespace.</param>
    /// <returns>
    /// The <see cref="Item"/> corresponding to the specified <paramref name="itemId"/>.
    /// If the item does not exist, an instance of <see cref="InvalidItem"/> is returned.
    /// If the item exists but is out of stock, an instance of <see cref="OutOfStockItem"/> is returned.
    /// </returns>
    Item GetItem(string itemId);

    /// <summary>
    /// Adds multiple items to the inventory.
    /// </summary>
    /// <param name="items">
    /// A collection of <see cref="Item"/> objects to be added to the inventory. 
    /// Each item must have a unique <see cref="Item.ItemId"/> and must not be <c>null</c>.
    /// </param>
    void AddItems(IEnumerable<Item> items);

    /// <summary>
    /// Marks an item in the inventory as sold by decrementing its quantity.
    /// </summary>
    /// <param name="itemId">
    /// The unique identifier of the item to be marked as sold. Must not be <c>null</c>, empty, or consist only of whitespace.
    /// </param>
    /// <remarks>
    /// If the item's quantity reaches zero, it is still retained in the inventory but marked as out of stock.
    /// This method ensures the inventory remains consistent after an item is sold.
    /// </remarks>
    void ItemSold(string itemId);

    /// <summary>
    /// Retrieves all items in the inventory that are considered to have low stock levels.
    /// </summary>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of <see cref="Item"/> objects representing items with low inventory.
    /// Each item will have a <see cref="Item.Quantity"/> less than or equal to the configured low inventory threshold.
    /// </returns>
    /// <remarks>
    /// This method is useful for identifying items that need restocking.
    /// The threshold for low inventory is determined by the system configuration.
    /// </remarks>
    IEnumerable<Item> GetLowInventoryItems();
}
