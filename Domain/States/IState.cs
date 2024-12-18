using Domain.Money;

namespace Domain.States;

public interface IState
{
    void SelectItem(string itemId);
    void InsertCash(Bill bill);
    void CancelTransaction();
    void NotifyLowStockItems();
}
