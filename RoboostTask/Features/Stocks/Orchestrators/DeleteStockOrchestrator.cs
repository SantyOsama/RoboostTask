using MediatR;
using RoboostTask.DTOs.Stocks;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Stocks.Orchestrators
{
      public record DeleteStockOrchestrator(DeleteStockRequest StockRequest, string UserId) : IRequest<Response<string>>;

}
