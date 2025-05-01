using MediatR;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Products.Commands
{
    public record DeleteProductCommand(Guid Id) : IRequest<Response<bool>>;

}
