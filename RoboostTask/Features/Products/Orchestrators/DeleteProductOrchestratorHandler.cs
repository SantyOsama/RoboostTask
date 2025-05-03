using MediatR;
using RoboostTask.Features.Products.Orchestrators;
using RoboostTask.Features.Stocks.Orchestrators;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Products.Commands
{
    public class DeleteProductOrchestratorHandler : IRequestHandler<DeleteProductOrchestrator, Response<bool>>
    {
        private readonly IMediator _mediator;

        public DeleteProductOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Response<bool>> Handle(DeleteProductOrchestrator request, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeactivateStockOrchestrator (request.ProductId));

            var deleteProductResponse = await _mediator.Send(new DeleteProductCommand(request.ProductId), cancellationToken);

            return deleteProductResponse;
        }

    }
}
