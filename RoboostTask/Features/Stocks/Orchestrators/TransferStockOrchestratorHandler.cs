using MediatR;
using RoboostTask.DTOs.Stocks;
using RoboostTask.Enums;
using RoboostTask.Features.InventoryTransactions.Commands;
using RoboostTask.Features.Shared.Products.Queries;
using RoboostTask.Features.Shared.Warehouses.Commands;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Stocks.Orchestrators
{
    public class TransferStockOrchestratorHandler : IRequestHandler<TransferStockOrchestrator, Response<string>>
    {
        private readonly IMediator _mediator;

        public TransferStockOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Response<string>> Handle(TransferStockOrchestrator request, CancellationToken cancellationToken)
        {
            var transfer = request.StockRequest;
            var QuantitysourceResult = await _mediator.Send(new GetProductQuantityByWarhouseIdQuery(transfer.ProductId, transfer.FromWarehouseId));
            if (QuantitysourceResult < transfer.Quantity)
            {
                return Response<string>.Fail("Quantity is not available.");
            }

            var sourceResult = await _mediator.Send(new UpdateWarehouseStockCommand(transfer.ProductId, transfer.FromWarehouseId, -transfer.Quantity));
            if (!sourceResult.IsSucceeded)
            {
                return Response<string>.Fail("Failed to update source warehouse.");
            }

            var destinationResult = await _mediator.Send(new UpdateWarehouseStockCommand(transfer.ProductId, transfer.ToWarehouseId, transfer.Quantity));
            if (!destinationResult.IsSucceeded)
            {
                return Response<string>.Fail("Failed to update destination warehouse.");
            }

            var transactionResult = await _mediator.Send(new CreateInventoryTransactionCommand(
                transfer.ProductId,
                transfer.Quantity,
                transfer.FromWarehouseId,
                transfer.ToWarehouseId,
                request.UserId,
                TransactionEnum.TransactionType.TransferStock
            ));
            return transactionResult;
        }
    }
}
