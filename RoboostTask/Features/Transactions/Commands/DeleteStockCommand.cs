using MediatR;
using RoboostTask.DTOs.Stocks;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Transaction.Commands
{
    public record DeleteStockCommand(DeleteStockRequest StockRequest, string UserId) : IRequest<Response<string>>;

}
