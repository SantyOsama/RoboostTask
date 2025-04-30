using MediatR;
using RoboostTask.DTOs.Stocks;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Inventory.Commands
{
    public record AddStockCommand(AddStockRequest StockRequest, string UserId) : IRequest<Response<string>>;

}
