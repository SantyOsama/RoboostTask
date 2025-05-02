using MediatR;
using RoboostTask.GeneralResponse;
using RoboostTask.Models;
using RoboostTask.Repositories.Interfaces;
using static RoboostTask.Enums.TransactionEnum;

namespace RoboostTask.Features.Transaction.Commands
{
    public class DeleteStockCommandHandler : IRequestHandler<DeleteStockCommand, Response<string>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IInventoryTransactionRepository _transactionRepository;

        public DeleteStockCommandHandler(
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

        public async Task<Response<string>> Handle(DeleteStockCommand request,CancellationToken cancellationToken)
        {
            var stock = request.StockRequest;

            var product = await _productRepository.GetByIdAsync(stock.ProductId);
            if (product == null)
                return Response<string>.Fail("Product not found", statusCode: 404);

            Warehouse warehouse = null;
            if (stock.WarehouseId != Guid.Empty)
            {
                warehouse = await _warehouseRepository.GetByIdAsync(stock.WarehouseId);
                if (warehouse == null)
                    return Response<string>.Fail("Warehouse not found", statusCode: 404);
            }

            if (stock.Quantity <= 0)
                return Response<string>.Fail("Quantity must be greater than zero", statusCode: 400);

            int availableQuantity;
            if (stock.WarehouseId != Guid.Empty)
            {
                var stockItem = await _stockRepository.GetStockAsync(stock.ProductId, stock.WarehouseId);
                if (stockItem == null)
                    return Response<string>.Fail("Product not found in specified warehouse", statusCode: 404);

                availableQuantity = Math.Min(stockItem.QuantityInStock, product.Quantity);
            }
            else
            {
                availableQuantity = product.Quantity;
            }

            if (availableQuantity < stock.Quantity)
            {
                return Response<string>.Fail($"Not enough stock available. Available: {availableQuantity}, Requested: {stock.Quantity}",statusCode: 400);
            }

            if (stock.WarehouseId != Guid.Empty)
            {
                var stockItem = await _stockRepository.GetStockAsync(stock.ProductId, stock.WarehouseId);
                stockItem.QuantityInStock -= stock.Quantity;
                await _stockRepository.UpdateAsync(stockItem);
                await _stockRepository.SaveChangesAsyc();
            }

            product.Quantity -= stock.Quantity;
            await _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsyc();

            await _transactionRepository.AddAsync(new InventoryTransaction
            {
                ProductId = stock.ProductId,
                Quantity = stock.Quantity,
                TransactionType = TransactionType.RemoveStock,
                PerformedByUserId = request.UserId,
                Date = DateTime.UtcNow,
                SourceWarehouseId = stock.WarehouseId != Guid.Empty ? stock.WarehouseId : null,
                CreatedAt = DateTime.UtcNow
            });
            await _transactionRepository.SaveChangesAsyc();

            return Response<string>.Success(
                $"Successfully removed {stock.Quantity} items of product {product.Name}" +
                (warehouse != null ? $" from warehouse {warehouse.Name}" : ""));
        }
    }
}