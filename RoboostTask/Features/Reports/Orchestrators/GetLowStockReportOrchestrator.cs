using MediatR;
using RoboostTask.DTOs.Reports;

namespace RoboostTask.Features.Reports.Orchestrators
{
    public record GetLowStockReportOrchestrator(Guid? CategoryId) : IRequest<List<LowStockReportDTO>>;

}
