using MediatR;
using RoboostTask.Enums;
using RoboostTask.GeneralResponse;
using RoboostTask.Models;
using RoboostTask.Repositories.Interfaces;

namespace RoboostTask.Features.InventoryTransactions.Commands
{
    public class CreateInventoryTransactionCommandHandler : IRequestHandler<CreateInventoryTransactionCommand, Response<string>>
    {
        private readonly IInventoryTransactionRepository _transactionRepository;

        public CreateInventoryTransactionCommandHandler(IInventoryTransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<Response<string>> Handle(CreateInventoryTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = new InventoryTransaction
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                TransactionType = request.TransactionType,
                PerformedByUserId = request.UserId,
                Date = DateTime.UtcNow,
                DestinationWarehouseId = request.DestinationWarehouseId ?? Guid.Empty,
                SourceWarehouseId = request.DestinationWarehouseId ?? Guid.Empty,

            };

            await _transactionRepository.AddAsync(transaction);
            await _transactionRepository.SaveChangesAsyc();

            return Response<string>.Success("Transaction logged.");
        }
    }

}
