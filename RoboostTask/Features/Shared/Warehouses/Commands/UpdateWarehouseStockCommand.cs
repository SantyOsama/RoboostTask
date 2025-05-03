using MediatR;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Shared.Warehouses.Commands
{
    public record UpdateWarehouseStockCommand(Guid ProductId, Guid WarehouseId, int QuantityToAdd) : IRequest<Response<string>>;

}
