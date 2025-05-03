using MediatR;
using RoboostTask.Enums;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.InventoryTransactions.Commands
{
    public record CreateInventoryTransactionCommand(Guid ProductId, int Quantity, Guid? DestinationWarehouseId, Guid? SourceWarehouseId, string UserId, TransactionEnum.TransactionType TransactionType) : IRequest<Response<string>>;

}
