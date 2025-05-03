using MediatR;
using RoboostTask.DTOs.Products;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Products.Orchestrators
{
    public record AddProductOrchestrator(AddProductRequest ProductRequest) : IRequest<Response<Guid>>;
}
