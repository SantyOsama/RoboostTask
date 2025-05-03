using MediatR;
using RoboostTask.DTOs.Products;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Products.Orchestrators
{
    public record GetProductByIdOrchestrator(Guid ProductId) : IRequest<Response<GetProductResponse>>;

}
