using MediatR;
using RoboostTask.DTOs.Products;
using RoboostTask.DTOs.Reports;
using RoboostTask.DTOs.Stocks;
using RoboostTask.Repositories.Interfaces;

namespace RoboostTask.Features.Reports.Queries
{
    public class GetLowStockReportQueryHandler : IRequestHandler<GetLowStockReportQuery, List<LowStockReportDTO>>
    {
        private readonly IProductRepository _productRepository;

        public GetLowStockReportQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<LowStockReportDTO>> Handle(GetLowStockReportQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetLowStockProductsAsync();

            //if (request.CategoryId.HasValue)
            //{
            //    products = products.Where(p => p.CategoryId == request.CategoryId.Value);
            //}

            return products.Select(p => new LowStockReportDTO
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Quantity = p.Quantity,
                LowStockThreshold = p.LowStockThreshold,
                IsLowStock = true,
                Stocks = p.Stocks?.Select(s => new StockForLowDTO
                {
                    WarehouseId = s.WarehouseId,
                    QuantityInStock = s.QuantityInStock,
                }).ToList(),
                TotalWarehouses = p.Stocks?.Count ?? 0,
                TotalActiveStock = p.Stocks?.Sum(s => s.QuantityInStock) ?? 0,
                DeficitAmount = p.LowStockThreshold - (p.Stocks?.Sum(s => s.QuantityInStock) ?? 0)
            }).ToList();
        }
    }
}

