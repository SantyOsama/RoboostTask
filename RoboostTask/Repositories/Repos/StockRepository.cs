using Microsoft.EntityFrameworkCore;
using RoboostTask.Data.Repositories;
using RoboostTask.Data;
using RoboostTask.Models;
using RoboostTask.Repositories.Interfaces;

namespace RoboostTask.Repositories.Repos
{

    public class StockRepository : Repository<Stock>, IStockRepository
    {
        public StockRepository(AppDbContext context) : base(context) { }

        public async Task<Stock?> GetStockAsync(Guid productId, Guid warehouseId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(s => s.ProductId == productId && s.WarehouseId == warehouseId);
        }

        public async Task<int> GetStockQuantityAsync(Guid productId, Guid warehouseId)
        {
            return await _dbSet
                .Where(s => s.ProductId == productId && s.WarehouseId == warehouseId)
                .Select(s => s.QuantityInStock)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> StockExistsAsync(Guid productId, Guid warehouseId)
        {
            return await _dbSet
                .AnyAsync(s => s.ProductId == productId && s.WarehouseId == warehouseId);
        }

        public async Task<IEnumerable<Stock>> GetStocksByProductIdAsync(Guid productId)
        {
            return await _dbSet
                .Include(s => s.Warehouse)
                .Where(s => s.ProductId == productId)
                .ToListAsync();
        }
        public async Task UpdateRangeAsync(IEnumerable<Stock> stocks)
        {
            _dbSet.UpdateRange(stocks);
        }
        public async Task DeactivateStocksForProductAsync(Guid productId)
        {
            var stocks = await GetStocksByProductIdAsync(productId);
            foreach (var stock in stocks)
            {
                stock.IsActive = false;
            }
            await UpdateRangeAsync(stocks);
        }
        public async Task<List<Stock>> GetProductStocksWithWarehousesAsync(Guid productId)
        {
            return await _dbSet
                .Where(s => s.ProductId == productId)
                .Include(s => s.Warehouse)
                .ToListAsync();
        }
    }
}

