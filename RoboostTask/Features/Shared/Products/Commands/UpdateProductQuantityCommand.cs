using MediatR;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Shared.Products.Commands
{
    public record UpdateProductQuantityCommand(Guid ProductId, int QuantityToAdd) : IRequest<Response<string>>;

}
