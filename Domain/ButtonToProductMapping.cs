using System.Text;

namespace Domain;

public sealed class ButtonToProductMapping
{
    private readonly Dictionary<string, Item> _mapping = new();

    public void Add(string buttonId, Item item)
    {
        _mapping.TryAdd(buttonId.ToUpper(), item);
    }

    public Item GetItem(string buttonId)
    {
        if (!_mapping.TryGetValue(buttonId.ToUpper(), out var item))
        {
            return new NullItem();
        }

        return item;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        foreach (var keyValuePair in _mapping)
        {
            sb.AppendLine($"{keyValuePair.Key}\t{keyValuePair.Value.Description}\t{keyValuePair.Value.UnitPrice}");
        }

        return sb.ToString();
    }
}
