using MediatR;
using RoboostTask.Data;
using RoboostTask.DTOs.Reports;
using RoboostTask.Repositories.Interfaces;

namespace RoboostTask.Features.Reports.Queries
{
    public class GetTransactionHistoryReportQueryHandler : IRequestHandler<GetTransactionHistoryReportQuery, List<TransactionHistoryDTO>>
    {
        private readonly IInventoryTransactionRepository _transactionRepository;

        public GetTransactionHistoryReportQueryHandler(
            IInventoryTransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<List<TransactionHistoryDTO>> Handle(GetTransactionHistoryReportQuery request,CancellationToken cancellationToken)
        {
            var transactions = await _transactionRepository.GetFilteredTransactionsAsync(
                request.Filter.ProductId,
                 //request.Filter.CategoryId,
                request.Filter.StartDate,
                request.Filter.EndDate,
                request.Filter.TransactionType,
                request.Filter.WarehouseId,
                request.Filter.UserId);

            return transactions.Select(t => new TransactionHistoryDTO
            {
                Date = t.Date,
                TransactionType = t.TransactionType.ToString(),
                ProductName = t.Product.Name,
                //CategoryName = t.Product.Category?.Name ?? "N/A",
                //CategoryId = t.Product.Category?.Id,
                Quantity = t.Quantity,
                UserName = $"{t.User.FirstName} {t.User.LastName}",
                SourceWarehouse = t.SourceWarehouse?.Name ?? "N/A",
                DestinationWarehouse = t.DestinationWarehouse?.Name ?? "N/A",
            }).ToList();
        }
    }
}