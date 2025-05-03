using MediatR;
using RoboostTask.DTOs.Reports;

namespace RoboostTask.Features.Reports.Queries
{
    public class GetLowStockReportQuery : IRequest<List<LowStockReportDTO>>
    {
        public Guid? CategoryId { get; set; }
    }
}
