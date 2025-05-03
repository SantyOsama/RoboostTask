using MediatR;
using RoboostTask.Models;

namespace RoboostTask.Features.Products.Queries
{
    public record GetProductEntityByIdQuery(Guid ProductId) : IRequest<Product?>;

}
