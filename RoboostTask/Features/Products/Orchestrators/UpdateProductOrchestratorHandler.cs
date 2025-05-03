using MediatR;
using RoboostTask.Features.Products.Commands;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Products.Orchestrators
{
    public class UpdateProductOrchestratorHandler : IRequestHandler<UpdateProductOrchestrator, Response<string>>
    {
        private readonly IMediator _mediator;

        public UpdateProductOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<Response<string>> Handle(UpdateProductOrchestrator request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateProductCommand(request.ProductRequest), cancellationToken);

            return result;
        }
    }
}

