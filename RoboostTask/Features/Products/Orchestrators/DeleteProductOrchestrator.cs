using MediatR;
using RoboostTask.Features.Products.Commands;
using RoboostTask.Features.Stocks.Commands;
using RoboostTask.GeneralResponse;
using RoboostTask.Models;

namespace RoboostTask.Features.Products.Orchestrators
{
    public class DeleteProductOrchestrator : IRequest<Response<bool>>
    {
        private readonly IMediator _mediator;
        public Guid ProductId { get; set; }

        public DeleteProductOrchestrator(IMediator mediator, Guid productId)
        {
            _mediator = mediator;
            ProductId = productId;
        }

        public async Task<Response<bool>> Handle(CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeactivateStockCommand { ProductId = ProductId });
            var deleteProductResponse = await _mediator.Send(new DeleteProductCommand( ProductId));
            return deleteProductResponse;
        }
    }
}