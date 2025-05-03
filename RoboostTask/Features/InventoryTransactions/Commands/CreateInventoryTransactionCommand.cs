using MediatR;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.InventoryTransactions.Commands
{
    public record CreateInventoryTransactionCommand(Guid ProductId, int Quantity, Guid? DestinationWarehouseId, string UserId) : IRequest<Response<string>>;

}
