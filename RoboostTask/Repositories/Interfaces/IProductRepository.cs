using RoboostTask.Models;

namespace RoboostTask.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<bool> ProductExistsAsync(string name);
        Task SoftDeleteAsync(Guid id);
        Task<IEnumerable<Product>> GetLowStockProductsAsync();
        Task<List<Product>> GetAllWithStocksAsync();
        Task<Product?> GetByIdWithStocksAsync(Guid id);

    }
}
