using MediatR;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Stocks.Commands
{
    public record DeactivateStockCommand(Guid ProductId) : IRequest<Response<bool>>;

}
