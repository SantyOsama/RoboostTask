using MediatR;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Stocks.Commands
{
    public class DeactivateStockCommand : IRequest<Response<bool>>
    {
        public Guid ProductId { get; set; }

    }
}
