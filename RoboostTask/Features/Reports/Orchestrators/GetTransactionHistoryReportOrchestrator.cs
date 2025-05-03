using MediatR;
using RoboostTask.DTOs.Reports;

namespace RoboostTask.Features.Reports.Orchestrators
{
    public record GetTransactionHistoryReportOrchestrator(TransactionHistoryFilterDTO Filter)
      : IRequest<List<TransactionHistoryDTO>>;
}
