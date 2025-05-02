using MediatR;
using RoboostTask.GeneralResponse;
using RoboostTask.Models;
using RoboostTask.Repositories.Interfaces;
using static RoboostTask.Enums.TransactionEnum;

namespace RoboostTask.Features.Transaction.Commands
{
    public class AddStockCommandHandler : IRequestHandler<AddStockCommand, Response<string>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IInventoryTransactionRepository _transactionRepository;

        public AddStockCommandHandler(
            IProductRepository productRepository,
            IWarehouseRepository warehouseRepository,
            IStockRepository stockRepository,
            IInventoryTransactionRepository transactionRepository)
        {
            _productRepository = productRepository;
            _warehouseRepository = warehouseRepository;
            _stockRepository = stockRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<Response<string>> Handle(AddStockCommand request, CancellationToken cancellationToken)
        {
            var stock = request.StockRequest;

            var product = await _productRepository.GetByIdAsync(stock.ProductId);
            if (product == null || product.IsDeleted)
                return Response<string>.Fail("Product not found or is not active");

            if (stock.WarehouseId != Guid.Empty)
            {
                var warehouseExists = await _warehouseRepository.WarehouseExistsAsync(stock.WarehouseId);
                if (!warehouseExists)
                    return Response<string>.Fail("Warehouse not found");
            }

            product.Quantity += stock.Quantity;
            await _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsyc();

            if (stock.WarehouseId != Guid.Empty)
            {
                var stockItem = await _stockRepository.GetStockAsync(stock.ProductId, stock.WarehouseId);

                if (stockItem == null)
                {
                    stockItem = new Stock
                    {
                        ProductId = stock.ProductId,
                        WarehouseId = stock.WarehouseId,
                        QuantityInStock = stock.Quantity,
                        IsActive = true
                    };
                    await _stockRepository.AddAsync(stockItem);
                }
                else
                {
                    stockItem.QuantityInStock += stock.Quantity;
                    await _stockRepository.UpdateAsync(stockItem);
                }
                await _stockRepository.SaveChangesAsyc();
            }
            var transaction = new InventoryTransaction
            {
                ProductId = stock.ProductId,
                Quantity = stock.Quantity,
                TransactionType = TransactionType.AddStock,
                PerformedByUserId = request.UserId,
                Date = DateTime.UtcNow,
                DestinationWarehouseId = stock.WarehouseId != Guid.Empty ? stock.WarehouseId : null
            };

            await _transactionRepository.AddAsync(transaction);
            await _transactionRepository.SaveChangesAsyc();

            return Response<string>.Success($"Stock added successfully for product {product.Name}");
        }
    }
}