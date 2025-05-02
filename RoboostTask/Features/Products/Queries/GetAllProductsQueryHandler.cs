using MediatR;
using RoboostTask.DTOs.Products;
using RoboostTask.GeneralResponse;
using RoboostTask.Models;
using RoboostTask.Repositories.Interfaces;

namespace RoboostTask.Features.Products.Queries
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Response<List<GetProductResponse>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;

        public GetAllProductsQueryHandler(
            IProductRepository productRepository,
            IStockRepository stockRepository)
        {
            _productRepository = productRepository;
            _stockRepository = stockRepository;
        }

        public async Task<Response<List<GetProductResponse>>> Handle(GetAllProductsQuery request,CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllWithStocksAsync();

            var productDtos = new List<GetProductResponse>();

            foreach (var product in products.Where(p => !p.IsDeleted))
            {
                var stocks = await _stockRepository.GetProductStocksWithWarehousesAsync(product.Id);

                var productDto = new GetProductResponse
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    LowStockThreshold = product.LowStockThreshold,
                    IsLowStock = product.Quantity <= product.LowStockThreshold,
                    Stocks = stocks.Select(s => new ProductStockDTO
                    {
                        WarehouseId = s.WarehouseId,
                        WarehouseName = s.Warehouse?.Name,
                        WarehouseLocation = s.Warehouse?.Location,
                        QuantityInStock = s.QuantityInStock,
                        IsActive = s.IsActive
                    }).ToList(),
                    TotalWarehouses = stocks.Count,
                    TotalActiveStock = stocks.Sum(s => s.QuantityInStock)
                };

                productDtos.Add(productDto);
            }
            return Response<List<GetProductResponse>>.Success(productDtos,"Products retrieved successfully with stock details");
        }
    }
}