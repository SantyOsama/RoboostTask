using MediatR;
using RoboostTask.DTOs.Reports;

namespace RoboostTask.Features.Reports.Queries
{
    public record GetTransactionHistoryReportQuery(TransactionHistoryFilterDTO Filter): IRequest<List<TransactionHistoryDTO>>;

}
