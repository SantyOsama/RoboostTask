using MediatR;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Products.Commands
{
    public record DeleteProductCommand(int Id) : IRequest<Response<string>>;

}
