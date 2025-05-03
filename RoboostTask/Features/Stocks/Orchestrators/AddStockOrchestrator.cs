using MediatR;
using RoboostTask.DTOs.Stocks;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Stocks.Orchestrators
{
    public record AddStockOrchestrator(AddStockRequest StockRequest, string UserId) : IRequest<Response<string>>;

}
