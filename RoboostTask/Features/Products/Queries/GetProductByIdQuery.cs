using MediatR;
using RoboostTask.DTOs.Products;
using RoboostTask.GeneralResponse;
namespace RoboostTask.Features.Products.Queries
{
    public record GetProductByIdQuery(int Id) : IRequest<Response<GetProductResponse>>;

}
