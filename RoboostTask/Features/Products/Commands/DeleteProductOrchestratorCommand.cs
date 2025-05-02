using MediatR;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Products.Commands
{
    public record DeleteProductOrchestratorCommand(Guid ProductId) : IRequest<Response<bool>>;

}
