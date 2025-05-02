using MediatR;
using RoboostTask.GeneralResponse;
using RoboostTask.Repositories.Interfaces;

namespace RoboostTask.Features.Stocks.Commands
{
    public class DeactivateStockCommandHandler:IRequestHandler<DeactivateStockCommand, Response<bool>>
    {
        private readonly IStockRepository _stockRepository;

        public DeactivateStockCommandHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<Response<bool>> Handle(DeactivateStockCommand request, CancellationToken cancellationToken)
        {
            await _stockRepository.DeactivateStocksForProductAsync(request.ProductId);
            return Response<bool>.Success(true, "Stock deactivated successfully.", statusCode: 200);
        }
    }
}

