using MediatR;
using RoboostTask.Features.Products.Orchestrators;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Products.Commands
{
    public class DeleteProductOrchestratorCommandHandler : IRequestHandler<DeleteProductOrchestratorCommand, Response<bool>>
    {
        private readonly IMediator _mediator;

        public DeleteProductOrchestratorCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Response<bool>> Handle(DeleteProductOrchestratorCommand request, CancellationToken cancellationToken)
        {
            var orchestrator = new DeleteProductOrchestrator(_mediator, request.ProductId);
            return await orchestrator.Handle(cancellationToken);
        }

    }
}
