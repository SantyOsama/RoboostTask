using MediatR;
using Microsoft.EntityFrameworkCore;
using RoboostTask.Data;
using RoboostTask.GeneralResponse;
using RoboostTask.Models;
using static RoboostTask.Enums.TransactionEnum;

namespace RoboostTask.Features.Transaction.Commands
{
    public class AddStockCommandHandler : IRequestHandler<AddStockCommand, Response<string>>
    {
        private readonly AppDbContext _context;

        public AddStockCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Response<string>> Handle(AddStockCommand request, CancellationToken cancellationToken)
        {
            var stock = request.StockRequest;

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == stock.ProductId && !p.IsDeleted);
            if (product == null)
                return  Response<string>.Fail(null, "Product not found or is not Active");

            var  warehouse = await _context.Warehouses
                    .FirstOrDefaultAsync(w => w.Id == stock.WarehouseId, cancellationToken);

            if (warehouse == null)
                    return Response<string>.Fail(null, "Warehouse not found");

            product.Quantity += stock.Quantity;

            if (stock.WarehouseId != Guid.Empty)
            {
                var stockItem = await _context.Stocks
                    .FirstOrDefaultAsync(s => s.ProductId == stock.ProductId &&
                                           s.WarehouseId == stock.WarehouseId);

                if (stockItem == null)
                {
                    stockItem = new Stock
                    {
                        Id = Guid.NewGuid(),
                        ProductId = stock.ProductId,
                        WarehouseId = stock.WarehouseId,
                        QuantityInStock = stock.Quantity,
                        IsActive = true
                    };
                    _context.Stocks.Add(stockItem);
                }
                else
                {
                    stockItem.QuantityInStock += stock.Quantity;
                }
            }
            var transaction = new InventoryTransaction
            {
                ProductId = stock.ProductId,
                Quantity = stock.Quantity,
                TransactionType = TransactionType.AddStock,
                PerformedByUserId = request.UserId,
                Date = DateTime.UtcNow,
                DestinationWarehouseId = stock.WarehouseId != Guid.Empty ? stock.WarehouseId : null,
            };

            _context.InventoryTransactions.Add(transaction);
            await _context.SaveChangesAsync(cancellationToken);

            return Response<string>.Success($"Stock added successfully for product {product.Name}");
        }
    }
}
