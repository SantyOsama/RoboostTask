using RoboostTask.Models;

namespace RoboostTask.Repositories.Interfaces
{
    public interface IWarehouseRepository : IRepository<Warehouse>
    {
        Task<bool> WarehouseExistsAsync(Guid warehouseId);
    }
}
