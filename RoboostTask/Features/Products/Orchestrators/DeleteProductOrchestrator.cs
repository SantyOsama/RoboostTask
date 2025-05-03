using MediatR;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Products.Orchestrators
{
    public record DeleteProductOrchestrator(Guid ProductId) : IRequest<Response<bool>>;

}
