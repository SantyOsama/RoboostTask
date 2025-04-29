using MediatR;
using RoboostTask.DTOs.Products;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Products.Queries
{
    public record GetAllProductsQuery() : IRequest<Response<List<GetProductResponse>>>;

}
