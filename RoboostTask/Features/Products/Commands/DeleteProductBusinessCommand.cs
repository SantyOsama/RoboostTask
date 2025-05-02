using MediatR;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Products.Commands
{
    public record DeleteProductBusinessCommand(Guid ProductId) : IRequest<Response<bool>>;

}
