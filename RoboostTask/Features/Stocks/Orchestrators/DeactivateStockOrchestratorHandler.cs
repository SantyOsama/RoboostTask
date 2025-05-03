using MediatR;
using RoboostTask.Features.Stocks.Commands;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Stocks.Orchestrators
{
    public class DeactivateStockOrchestratorHandler : IRequestHandler<DeactivateStockOrchestrator, Response<bool>>
    {
        private readonly IMediator _mediator;

        public DeactivateStockOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Response<bool>> Handle(DeactivateStockOrchestrator request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeactivateStockCommand(request.ProductId ));

            return result;
        }
    }
}