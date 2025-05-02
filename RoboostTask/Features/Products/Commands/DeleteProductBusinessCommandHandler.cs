using MediatR;
using RoboostTask.Features.Products.Orchestrators;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Products.Commands
{
    public class DeleteProductBusinessCommandHandler : IRequestHandler<DeleteProductBusinessCommand, Response<bool>>
    {
        private readonly IMediator _mediator;

        public DeleteProductBusinessCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Response<bool>> Handle(DeleteProductBusinessCommand request, CancellationToken cancellationToken)
        {
            var orchestrator = new DeleteProductOrchestrator(_mediator, request.ProductId);
            return await orchestrator.Handle(cancellationToken);
        }

    }
}
