using MediatR;
using RoboostTask.DTOs.Products;
using RoboostTask.GeneralResponse;
using RoboostTask.Models;
using RoboostTask.Repositories.Interfaces;

namespace RoboostTask.Features.Products.Queries
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Response<GetProductResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;

        public GetProductByIdQueryHandler(
            IProductRepository productRepository,
            IStockRepository stockRepository)
        {
            _productRepository = productRepository;
            _stockRepository = stockRepository;
        }

        public async Task<Response<GetProductResponse>> Handle(
            GetProductByIdQuery request,
            CancellationToken cancellationToken)
        {
            // Get product with stock information
            var product = await _productRepository.GetByIdWithStocksAsync(request.Id);

            if (product == null || product.IsDeleted)
                return Response<GetProductResponse>.Fail("Product not found or is deleted", statusCode: 404);

            // Get detailed stock information
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

            return Response<GetProductResponse>.Success(productDto, "Product details retrieved successfully");
        }
    }
}