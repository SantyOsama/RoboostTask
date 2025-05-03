using RoboostTask.Models;
using static RoboostTask.Enums.TransactionEnum;

namespace RoboostTask.Repositories.Interfaces
{
    public interface IInventoryTransactionRepository: IRepository<InventoryTransaction>
    {
        Task<IEnumerable<InventoryTransaction>> GetTransactionsWithDetailsAsync();
        Task<IEnumerable<InventoryTransaction>> GetFilteredTransactionsAsync(
                Guid? productId = null,
                //Guid? categoryId = null,
                DateTime? startDate = null,
                DateTime? endDate = null,
                TransactionType? transactionType = null,
                Guid? warehouseId = null,
                Guid? userId = null);
    }
}
