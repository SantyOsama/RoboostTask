using MediatR;
using RoboostTask.DTOs.Reports;
using RoboostTask.Features.Reports.Queries;

namespace RoboostTask.Features.Reports.Orchestrators
{
    public class GetTransactionHistoryReportOrchestratorHandler
        : IRequestHandler<GetTransactionHistoryReportOrchestrator, List<TransactionHistoryDTO>>
    {
        private readonly IMediator _mediator;

        public GetTransactionHistoryReportOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<List<TransactionHistoryDTO>> Handle(GetTransactionHistoryReportOrchestrator request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetTransactionHistoryReportQuery(request.Filter), cancellationToken);

            return result;
        }
    }
}
