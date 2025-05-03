using MediatR;
using RoboostTask.DTOs.Reports;
using RoboostTask.DTOs.Stocks;
using RoboostTask.Features.Reports.Queries;

namespace RoboostTask.Features.Reports.Orchestrators
{
    public class GetLowStockReportOrchestratorHandler : IRequestHandler<GetLowStockReportOrchestrator, List<LowStockReportDTO>>
    {
        private readonly IMediator _mediator;

        public GetLowStockReportOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<List<LowStockReportDTO>> Handle(GetLowStockReportOrchestrator request, CancellationToken cancellationToken)
        {
            var products = await _mediator.Send(new GetLowStockProductsQuery(), cancellationToken);

            //if (request.CategoryId.HasValue)
            //    products = products.Where(p => p.CategoryId == request.CategoryId.Value).ToList();

            var report = products.Select(p => new LowStockReportDTO
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

            return report;
        }
    }

}
