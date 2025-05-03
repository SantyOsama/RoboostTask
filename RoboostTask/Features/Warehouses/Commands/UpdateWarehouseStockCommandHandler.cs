using MediatR;
using RoboostTask.GeneralResponse;
using RoboostTask.Models;
using RoboostTask.Repositories.Interfaces;

namespace RoboostTask.Features.Warehouses.Commands
{
    public class UpdateWarehouseStockCommandHandler : IRequestHandler<UpdateWarehouseStockCommand, Response<string>>
    {
        private readonly IStockRepository _stockRepository;
        private readonly IWarehouseRepository _warehouseRepository;

        public UpdateWarehouseStockCommandHandler(IStockRepository stockRepository, IWarehouseRepository warehouseRepository)
        {
            _stockRepository = stockRepository;
            _warehouseRepository = warehouseRepository;
        }

        public async Task<Response<string>> Handle(UpdateWarehouseStockCommand request, CancellationToken cancellationToken)
        {
            if (!await _warehouseRepository.WarehouseExistsAsync(request.WarehouseId))
                return Response<string>.Fail("Warehouse not found.");

            var stock = await _stockRepository.GetStockAsync(request.ProductId, request.WarehouseId);

            if (stock == null)
            {
                stock = new Stock
                {
                    ProductId = request.ProductId,
                    WarehouseId = request.WarehouseId,
                    QuantityInStock = request.QuantityToAdd,
                    IsActive = true
                };
                await _stockRepository.AddAsync(stock);
            }
            else
            {
                stock.QuantityInStock += request.QuantityToAdd;
                await _stockRepository.UpdateAsync(stock);
            }

            await _stockRepository.SaveChangesAsyc();

            return Response<string>.Success("Warehouse stock updated.");
        }
    }

}
