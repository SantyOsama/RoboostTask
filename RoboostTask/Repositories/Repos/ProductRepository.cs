using Microsoft.EntityFrameworkCore;
using RoboostTask.Data;
using RoboostTask.Data.Repositories;
using RoboostTask.Models;
using RoboostTask.Repositories.Interfaces;

namespace RoboostTask.Repositories.Repos
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public async Task<bool> ProductExistsAsync(string name)
        {
            return await _dbSet.AnyAsync(p => p.Name == name);
        }

        public async Task SoftDeleteAsync(Guid id)
        {
            var product = await GetByIdAsync(id);
            if (product == null) return;
            product.IsDeleted = true;
            await UpdateAsync(product);
        }

        public async Task<IEnumerable<Product>> GetLowStockProductsAsync()
        {
            return await _dbSet.AsNoTracking()
                    .Where(p => !p.IsDeleted && p.Quantity <= p.LowStockThreshold)
                    .Include(p => p.Stocks).ThenInclude(s => s.Warehouse)
                    .ToListAsync();
        }
        public async Task<List<Product>> GetAllWithStocksAsync()
        {
            return await _dbSet
                .Include(p => p.Stocks)
                .ThenInclude(s => s.Warehouse)
                .ToListAsync();
        }
        public async Task<Product?> GetByIdWithStocksAsync(Guid id)
        {
            return await _dbSet
                .Include(p => p.Stocks)
                .ThenInclude(s => s.Warehouse)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

    }
}
