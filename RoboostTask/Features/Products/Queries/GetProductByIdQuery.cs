using MediatR;
using RoboostTask.DTOs.Products;
using RoboostTask.GeneralResponse;
namespace RoboostTask.Features.Products.Queries
{
    public record GetProductByIdQuery(Guid Id) : IRequest<Response<GetProductResponse>>;

}
