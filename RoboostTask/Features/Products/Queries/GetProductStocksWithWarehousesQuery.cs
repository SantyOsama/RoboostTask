using MediatR;
using RoboostTask.Models;

namespace RoboostTask.Features.Products.Queries
{
    public record GetProductStocksWithWarehousesQuery(Guid ProductId) : IRequest<List<Stock>>;

}
