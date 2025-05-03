using MediatR;
using RoboostTask.DTOs.Products;
using RoboostTask.Features.Products.Queries;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Products.Orchestrators
{
    public class GetProductByIdOrchestratorHandler : IRequestHandler<GetProductByIdOrchestrator, Response<GetProductResponse>>
    {
        private readonly IMediator _mediator;

        public GetProductByIdOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Response<GetProductResponse>> Handle(GetProductByIdOrchestrator request, CancellationToken cancellationToken)
        {
            var product = await _mediator.Send(new GetProductEntityByIdQuery(request.ProductId), cancellationToken);
            if (product == null || product.IsDeleted)
                return Response<GetProductResponse>.Fail("Product not found or is deleted", statusCode: 404);

            var stocks = await _mediator.Send(new GetProductStocksWithWarehousesQuery(product.Id), cancellationToken);

            var dto = new GetProductResponse
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

            return Response<GetProductResponse>.Success(dto, "Product details retrieved successfully");
        }
    }

}
