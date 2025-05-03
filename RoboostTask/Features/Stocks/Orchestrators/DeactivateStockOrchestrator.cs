using MediatR;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Stocks.Orchestrators
{
    public record DeactivateStockOrchestrator(Guid ProductId) : IRequest<Response<bool>>;

}
