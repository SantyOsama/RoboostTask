using MediatR;
using RoboostTask.Data;
using RoboostTask.GeneralResponse;
using RoboostTask.Models;
using RoboostTask.Repositories.Interfaces;
using static RoboostTask.Enums.TransactionEnum;

namespace RoboostTask.Features.Transactions.Commands
{
    public class TransferStockCommandHandler : IRequestHandler<TransferStockCommand, Response<bool>>
    {
        private readonly AppDbContext _context;
        private readonly IProductRepository _productRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IStockRepository _stockRepository;

        public TransferStockCommandHandler(
            AppDbContext context,
            IProductRepository productRepository,
            IWarehouseRepository warehouseRepository,
            IStockRepository stockRepository)
        {
            _context = context;
            _productRepository = productRepository;
            _warehouseRepository = warehouseRepository;
            _stockRepository = stockRepository;
        }

        public async Task<Response<bool>> Handle(TransferStockCommand command,CancellationToken cancellationToken)
        {
            var request = command.TransferRequest;

            var product = await _productRepository.GetByIdAsync(request.ProductId);
            if (product == null || product.IsDeleted)
                return Response<bool>.Fail("Product not found or is deleted", statusCode: 404);

            var fromWarehouse = await _warehouseRepository.GetByIdAsync(request.FromWarehouseId);
            var toWarehouse = await _warehouseRepository.GetByIdAsync(request.ToWarehouseId);

            if (fromWarehouse == null || toWarehouse == null)
                return Response<bool>.Fail("One or both warehouses not found", statusCode: 404);

            var sourceStock = await _stockRepository.GetStockAsync(request.ProductId, request.FromWarehouseId);
            if (sourceStock == null || sourceStock.QuantityInStock < request.Quantity)
                return Response<bool>.Fail("Insufficient stock in source warehouse", statusCode: 400);

            if (request.FromWarehouseId == request.ToWarehouseId)
                return Response<bool>.Fail("Cannot transfer to the same warehouse", statusCode: 400);

            sourceStock.QuantityInStock -= request.Quantity;
            await _stockRepository.UpdateAsync(sourceStock);

            var destinationStock = await _stockRepository.GetStockAsync(request.ProductId, request.ToWarehouseId);
            if (destinationStock == null)
            {
                destinationStock = new Stock
                {
                    ProductId = request.ProductId,
                    WarehouseId = request.ToWarehouseId,
                    QuantityInStock = request.Quantity
                };
                await _stockRepository.AddAsync(destinationStock);
            }
            else
            {
                destinationStock.QuantityInStock += request.Quantity;
                await _stockRepository.UpdateAsync(destinationStock);
            }

            await _context.InventoryTransactions.AddAsync(new InventoryTransaction
            {
                ProductId = request.ProductId,
                TransactionType = TransactionType.TransferStock,
                Quantity = request.Quantity,
                Date = DateTime.UtcNow,
                PerformedByUserId = command.UserId, 
                SourceWarehouseId = request.FromWarehouseId,
                DestinationWarehouseId = request.ToWarehouseId
            });

            await _context.SaveChangesAsync();

            return Response<bool>.Success(true, "Stock transferred successfully");
        }
    }
}