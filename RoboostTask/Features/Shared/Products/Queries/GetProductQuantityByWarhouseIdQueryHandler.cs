using MediatR;
using RoboostTask.Repositories.Interfaces;

namespace RoboostTask.Features.Shared.Products.Queries
{
    public class GetProductQuantityByWarhouseIdQueryHandler : IRequestHandler<GetProductQuantityByWarhouseIdQuery, int>
    {
        private readonly IStockRepository _stockRepository;

        public GetProductQuantityByWarhouseIdQueryHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }
        public async Task<int> Handle(GetProductQuantityByWarhouseIdQuery request, CancellationToken cancellationToken)
        {
            return await _stockRepository.GetStockQuantityAsync(request.productId,request.warehouseId);
        }
    }
}
