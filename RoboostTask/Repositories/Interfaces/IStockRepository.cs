using RoboostTask.Models;

namespace RoboostTask.Repositories.Interfaces
{
    public interface IStockRepository : IRepository<Stock>
    {
        Task<Stock?> GetStockAsync(Guid productId, Guid warehouseId);
        Task<int> GetStockQuantityAsync(Guid productId, Guid warehouseId);
        Task<bool> StockExistsAsync(Guid productId, Guid warehouseId);
        Task UpdateRangeAsync(IEnumerable<Stock> stocks);
        Task DeactivateStocksForProductAsync(Guid productId);

    }
}
