using MediatR;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Products.Commands
{
    public record UpdateProductQuantityCommand(Guid ProductId, int QuantityToAdd) : IRequest<Response<string>>;

}
