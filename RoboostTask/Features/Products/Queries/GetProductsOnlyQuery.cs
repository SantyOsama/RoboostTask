using MediatR;
using RoboostTask.Models;

namespace RoboostTask.Features.Products.Queries
{
    public record GetProductsOnlyQuery() : IRequest<List<Product>>;

}
