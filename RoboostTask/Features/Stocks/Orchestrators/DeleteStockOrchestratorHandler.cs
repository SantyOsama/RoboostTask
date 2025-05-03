using MediatR;
using RoboostTask.Features.InventoryTransactions.Commands;
using RoboostTask.Features.Shared.Products.Commands;
using RoboostTask.Features.Warehouses.Commands;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Stocks.Orchestrators
{
    public class DeleteStockOrchestratorHandler : IRequestHandler<DeleteStockOrchestrator, Response<string>>
    {
        private readonly IMediator _mediator;

        public DeleteStockOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Response<string>> Handle(DeleteStockOrchestrator request, CancellationToken cancellationToken)
        {
            var stock = request.StockRequest;

            var updateProductResult = await _mediator.Send(new UpdateProductQuantityCommand(stock.ProductId, stock.Quantity));

            var warehouseResult = await _mediator.Send(new UpdateWarehouseStockCommand(stock.ProductId, stock.WarehouseId, -stock.Quantity));

            var transactionResult = await _mediator.Send(new CreateInventoryTransactionCommand(
                stock.ProductId,
                stock.Quantity,
                stock.WarehouseId,
                request.UserId
            ));

            return transactionResult;
        }
    }
}
