using MediatR;
using Microsoft.EntityFrameworkCore;
using RoboostTask.Data;
using RoboostTask.GeneralResponse;
using RoboostTask.Models;
using static RoboostTask.Enums.TransactionEnum;

namespace RoboostTask.Features.Transaction.Commands
{
    public class DeleteStockCommandHandler : IRequestHandler<DeleteStockCommand, Response<string>>
    {
        private readonly AppDbContext _context;

        public DeleteStockCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Response<string>> Handle(DeleteStockCommand request, CancellationToken cancellationToken)
        {
            var stock = request.StockRequest;

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == stock.ProductId, cancellationToken);

            if (product == null)
                return Response<string>.Fail("Product not found", statusCode: 404);

            Warehouse warehouse = null;
            if (stock.WarehouseId != Guid.Empty)
            {
                warehouse = await _context.Warehouses
                    .FirstOrDefaultAsync(w => w.Id == stock.WarehouseId, cancellationToken);

                if (warehouse == null)
                    return Response<string>.Fail("Warehouse not found", statusCode: 404);
            }

            if (stock.Quantity <= 0)
                return Response<string>.Fail("Quantity must be greater than zero", statusCode: 400);

            int availableQuantity;

            if (stock.WarehouseId != Guid.Empty)
            {
                var stockItem = await _context.Stocks
                    .FirstOrDefaultAsync(s => s.ProductId == stock.ProductId &&
                                           s.WarehouseId == stock.WarehouseId,
                                     cancellationToken);

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
                return Response<string>.Fail(
                    $"Not enough stock available. Available: {availableQuantity}, Requested: {stock.Quantity}",
                    statusCode: 400);
            }

            if (stock.WarehouseId != Guid.Empty)
            {
                var stockItem = await _context.Stocks
                    .FirstOrDefaultAsync(s => s.ProductId == stock.ProductId &&
                                           s.WarehouseId == stock.WarehouseId,
                                     cancellationToken);

                stockItem.QuantityInStock -= stock.Quantity;
            }

            product.Quantity -= stock.Quantity;

            var transaction = new InventoryTransaction
            {
                Id = Guid.NewGuid(),
                ProductId = stock.ProductId,
                Quantity = stock.Quantity,
                TransactionType = TransactionType.RemoveStock,
                PerformedByUserId = request.UserId,
                Date = DateTime.UtcNow,
                SourceWarehouseId = stock.WarehouseId != Guid.Empty ? stock.WarehouseId : null,
                WarehouseId = stock.WarehouseId != Guid.Empty ? stock.WarehouseId : null,
                CreatedAt = DateTime.UtcNow
            };

            _context.InventoryTransactions.Add(transaction);
            await _context.SaveChangesAsync(cancellationToken);

            return Response<string>.Success(
                $"Successfully removed {stock.Quantity} items of product {product.Name}" +
                (warehouse != null ? $" from warehouse {warehouse.Name}" : ""));
        }
    }
}
