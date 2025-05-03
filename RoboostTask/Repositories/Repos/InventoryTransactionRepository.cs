using Microsoft.EntityFrameworkCore;
using RoboostTask.Data;
using RoboostTask.Data.Repositories;
using RoboostTask.Models;
using RoboostTask.Repositories.Interfaces;
using static RoboostTask.Enums.TransactionEnum;

namespace RoboostTask.Repositories.Repos
{
    public class InventoryTransactionRepository : Repository<InventoryTransaction>, IInventoryTransactionRepository
    {
        public InventoryTransactionRepository(AppDbContext context) : base(context) { }
        public async Task<IEnumerable<InventoryTransaction>> GetFilteredTransactionsAsync(Guid? productId = null,/*Guid? categoryId = null,*/
            DateTime? startDate = null,DateTime? endDate = null,TransactionType? transactionType = null,Guid? warehouseId = null,
            Guid? userId = null)
        {
            var query = _dbSet
                .Include(t => t.Product)
                //.ThenInclude(p => p.Category)
                .Include(t => t.User)
                .Include(t => t.SourceWarehouse)
                .Include(t => t.DestinationWarehouse)
                .AsQueryable();

            if (productId.HasValue)
                query = query.Where(t => t.ProductId == productId.Value);

            //if (categoryId.HasValue)
            //    query = query.Where(t => t.Product.Category.Id == categoryId.Value);

            if (startDate.HasValue)
                query = query.Where(t => t.Date >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(t => t.Date <= endDate.Value);

            if (transactionType.HasValue)
                query = query.Where(t => t.TransactionType == transactionType.Value);

            if (warehouseId.HasValue)
                query = query.Where(t => t.SourceWarehouseId == warehouseId.Value ||
                                       t.DestinationWarehouseId == warehouseId.Value);

            if (userId.HasValue)
                query = query.Where(t => t.PerformedByUserId == userId.Value.ToString());


            return await query.OrderByDescending(t => t.Date).ToListAsync();
        }

        public async Task<IEnumerable<InventoryTransaction>> GetTransactionsWithDetailsAsync()
        {
            return await _dbSet
                .Include(t => t.Product)
                //.ThenInclude(p => p.Category)
                .Include(t => t.User)
                .Include(t => t.SourceWarehouse)
                .Include(t => t.DestinationWarehouse)
                .ToListAsync();
        }
    }
}