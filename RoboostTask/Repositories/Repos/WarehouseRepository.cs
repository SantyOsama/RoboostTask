using Microsoft.EntityFrameworkCore;
using RoboostTask.Data;
using RoboostTask.Data.Repositories;
using RoboostTask.Models;
using RoboostTask.Repositories.Interfaces;

namespace RoboostTask.Repositories.Repos
{
    public class WarehouseRepository : Repository<Warehouse>, IWarehouseRepository
    {
        public WarehouseRepository(AppDbContext context) : base(context) { }

        public async Task<bool> WarehouseExistsAsync(Guid warehouseId)
        {
            return await _dbSet.AnyAsync(w => w.Id == warehouseId);
        }

        public async Task<IEnumerable<Warehouse>> GetWarehousesByLocationAsync(string location)
        {
            return await _dbSet
                .Where(w => w.Location.Contains(location, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }

        public async Task<int> GetWarehouseStockCountAsync(Guid warehouseId)
        {
            return await _context.Stocks
                .Where(s => s.WarehouseId == warehouseId)
                .SumAsync(s => s.QuantityInStock);
        }

    }
}
