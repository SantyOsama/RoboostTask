using MediatR;
using RoboostTask.Data;
using RoboostTask.GeneralResponse;
using RoboostTask.Models;
using static RoboostTask.Enums.TransactionEnum;

namespace RoboostTask.Features.Inventory.Commands
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

            var product = await _context.Products.FindAsync(stock.ProductId);
            if (product == null)
                return  Response<string>.Fail(null, "Product not found");

            product.Quantity += stock.Quantity;

            var transaction = new InventoryTransaction
            {
                ProductId = stock.ProductId,
                Quantity = stock.Quantity,
                TransactionType = TransactionType.AddStock,
                PerformedByUserId =request.UserId,
                Date = DateTime.UtcNow
            };

            _context.InventoryTransactions.Add(transaction);
            await _context.SaveChangesAsync(cancellationToken);

            return Response<string>.Success($"Stock added successfully for product {product.Name}");
        }
    }
}
