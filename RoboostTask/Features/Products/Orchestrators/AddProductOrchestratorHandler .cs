using MediatR;
using RoboostTask.DTOs.Products;
using RoboostTask.Features.Products.Commands;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Products.Orchestrators
{
    public class AddProductOrchestratorHandler : IRequestHandler<AddProductOrchestrator, Response<Guid>>
    {
        private readonly IMediator _mediator;

        public AddProductOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Response<Guid>> Handle(AddProductOrchestrator request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new AddProductCommand(request.ProductRequest));

            return result;
        }
    }
}
