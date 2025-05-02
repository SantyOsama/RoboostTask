using RoboostTask.Data;
using RoboostTask.Data.Repositories;
using RoboostTask.Models;
using RoboostTask.Repositories.Interfaces;

namespace RoboostTask.Repositories.Repos
{
    public class InventoryTransactionRepository : Repository<InventoryTransaction>, IInventoryTransactionRepository
    {
        public InventoryTransactionRepository(AppDbContext context) : base(context) { }
    }
}
