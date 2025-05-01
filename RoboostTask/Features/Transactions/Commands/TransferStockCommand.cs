using MediatR;
using RoboostTask.DTOs.Stocks;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Transactions.Commands
{
    public record TransferStockCommand(TransferStockRequest TransferRequest, string UserId) : IRequest<Response<bool>>;
}
