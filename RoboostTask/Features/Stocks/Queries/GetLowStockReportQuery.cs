using MediatR;
using RoboostTask.DTOs.Reports;

namespace RoboostTask.Features.Stocks.Queries
{
    public class GetLowStockReportQuery : IRequest<List<LowStockReportDTO>>
    {
        public Guid? CategoryId { get; set; }
    }
}
