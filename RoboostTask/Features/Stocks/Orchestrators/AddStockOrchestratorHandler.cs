using MediatR;
using RoboostTask.Enums;
using RoboostTask.Features.InventoryTransactions.Commands;
using RoboostTask.Features.Reports.Orchestrators;
using RoboostTask.Features.Reports.Queries;
using RoboostTask.Features.Shared.Products.Commands;
using RoboostTask.Features.Shared.Warehouses.Commands;
using RoboostTask.Features.Stocks.Orchestrators;
using RoboostTask.GeneralResponse;
using RoboostTask.Services;

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
            Guid.Empty,
            request.UserId,
            TransactionEnum.TransactionType.AddStock

        ));
        //Email
        var products = await _mediator.Send(new GetLowStockProductsQuery());
        var lowstockmail=FormatEmail.CreateLowStockEmail(products);
        SendEmail email = new SendEmail();
        await email.SendEmailAsync("santyosama2@gmail.com", lowstockmail);
        return transactionResult;
    }
}
