using MediatR;
using RoboostTask.Models;
using RoboostTask.Repositories.Interfaces;

namespace RoboostTask.Features.Products.Queries
{
    public class GetProductStocksWithWarehousesQueryHandler : IRequestHandler<GetProductStocksWithWarehousesQuery, List<Stock>>
    {
        private readonly IStockRepository _stockRepository;

        public GetProductStocksWithWarehousesQueryHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<List<Stock>> Handle(GetProductStocksWithWarehousesQuery request, CancellationToken cancellationToken)
        {
            return await _stockRepository.GetProductStocksWithWarehousesAsync(request.ProductId);
        }
    }

}
