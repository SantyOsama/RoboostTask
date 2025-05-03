using MediatR;
using RoboostTask.Features.InventoryTransactions.Commands;
using RoboostTask.Features.Shared.Products.Commands;
using RoboostTask.Features.Stocks.Orchestrators;
using RoboostTask.Features.Warehouses.Commands;
using RoboostTask.GeneralResponse;

public class AddStockOrchestratorHandler : IRequestHandler<AddStockOrchestrator, Response<string>>
{
    private readonly IMediator _mediator;

    public AddStockOrchestratorHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Response<string>> Handle(AddStockOrchestrator request, CancellationToken cancellationToken)
    {
        var stock = request.StockRequest;

        var updateProductResult = await _mediator.Send(new UpdateProductQuantityCommand(stock.ProductId, -stock.Quantity));
        if (!updateProductResult.IsSucceeded) {
               return updateProductResult;
        }

        var warehouseResult = await _mediator.Send(new UpdateWarehouseStockCommand(stock.ProductId, stock.WarehouseId, stock.Quantity));

        var transactionResult = await _mediator.Send(new CreateInventoryTransactionCommand(
            stock.ProductId,
            stock.Quantity,
            stock.WarehouseId,
            request.UserId
        ));

        return transactionResult;
    }
}
