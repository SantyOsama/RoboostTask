using MediatR;
using RoboostTask.DTOs.Stocks;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Transaction.Commands
{
    public record AddStockCommand(AddStockRequest StockRequest, string UserId) : IRequest<Response<string>>;

}
