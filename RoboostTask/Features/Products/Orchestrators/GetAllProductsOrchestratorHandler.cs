using MediatR;
using RoboostTask.DTOs.Products;
using RoboostTask.Features.Products.Queries;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Products.Orchestrators
{
    public class GetAllProductsOrchestratorHandler : IRequestHandler<GetAllProductsOrchestrator, Response<List<GetProductResponse>>>
    {
        private readonly IMediator _mediator;

        public GetAllProductsOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Response<List<GetProductResponse>>> Handle(GetAllProductsOrchestrator request, CancellationToken cancellationToken)
        {
            var products = await _mediator.Send(new GetProductsOnlyQuery(), cancellationToken);
            var productDtos = new List<GetProductResponse>();

            foreach (var product in products.Where(p => !p.IsDeleted))
            {
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

                productDtos.Add(dto);
            }

            return Response<List<GetProductResponse>>.Success(productDtos, "Products retrieved successfully with stock details");
        }
    }

}
