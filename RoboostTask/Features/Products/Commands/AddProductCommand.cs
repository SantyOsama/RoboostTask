using MediatR;
using RoboostTask.DTOs.Products;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Products.Commands
{
    public record AddProductCommand (AddProductRequest ProductRequest) : IRequest<Response<Guid>>;
}
