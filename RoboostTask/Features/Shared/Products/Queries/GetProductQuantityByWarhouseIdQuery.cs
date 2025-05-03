using MediatR;

namespace RoboostTask.Features.Shared.Products.Queries
{
    public record GetProductQuantityByWarhouseIdQuery(Guid productId,Guid warehouseId):IRequest<int>;

}
