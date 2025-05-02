using MediatR;
using RoboostTask.DTOs;
using RoboostTask.Repositories.Interfaces;
using RoboostTask.Features.Transaction.Queries;
using RoboostTask.Models;

namespace RoboostTask.Features.Transaction.Queries
{
    public class GetInventoryQueryHandler : IRequestHandler<GetInventoryQuery, List<InventoryDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IInventoryTransactionRepository _transactionRepository;

        public GetInventoryQueryHandler(
            IProductRepository productRepository,
            IWarehouseRepository warehouseRepository,
            IInventoryTransactionRepository transactionRepository)
        {
            _productRepository = productRepository;
            _warehouseRepository = warehouseRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<List<InventoryDTO>> Handle(
            GetInventoryQuery request,
            CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();
            var warehouses = await _warehouseRepository.GetAllAsync();
            var transactions = await _transactionRepository.GetAllAsync();

            var inventory = (from product in products
                             from warehouse in warehouses
                             let quantity = CalculateWarehouseProductQuantity(
                                 product.Id,
                                 warehouse.Id,
                                 transactions)
                             select new InventoryDTO
                             {
                                 ProductId = product.Id,
                                 ProductName = product.Name,
                                 WarehouseId = warehouse.Id,
                                 WarehouseName = warehouse.Name,
                                 QuantityInStock = quantity
                             }).ToList();

            return inventory;
        }
        private int CalculateWarehouseProductQuantity(
            Guid productId,
            Guid warehouseId,
            IEnumerable<InventoryTransaction> transactions)
        {
            return transactions
                .Where(t => t.ProductId == productId &&
                           (t.SourceWarehouseId == warehouseId ||
                            t.DestinationWarehouseId == warehouseId))
                .Sum(t =>
                    (t.DestinationWarehouseId == warehouseId ? t.Quantity : 0) -
                    (t.SourceWarehouseId == warehouseId ? t.Quantity : 0));
        }
    }
}