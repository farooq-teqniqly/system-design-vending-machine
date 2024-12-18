using Domain.Inventory;
using Domain.Money;

namespace Domain.States;

public interface IState
{
    void SelectItem(Item item);
    void InsertCash(int amount);
    void CancelTransaction();
    void NotifyLowStockItems();
}
