using MediatR;
using Microsoft.EntityFrameworkCore;
using RoboostTask.Data;
using RoboostTask.DTOs;
using RoboostTask.Features.Inventory.Queries;

public class GetInventoryQueryHandler : IRequestHandler<GetInventoryQuery, List<InventoryDTO>>
{
    private readonly AppDbContext _context;

    public GetInventoryQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<InventoryDTO>> Handle(GetInventoryQuery request, CancellationToken cancellationToken)
    {
        var products = await _context.Products.AsNoTracking().ToListAsync();
        var warehouses = await _context.Warehouses.AsNoTracking().ToListAsync();
        var transactions = await _context.InventoryTransactions.AsNoTracking().ToListAsync();

        var inventory = (from product in products
                         from warehouse in warehouses
                         let quantity = transactions
                            .Where(t => t.ProductId == product.Id &&
                                       (t.SourceWarehouseId == warehouse.Id || t.DestinationWarehouseId == warehouse.Id))
                            .Sum(t =>
                                (t.DestinationWarehouseId == warehouse.Id ? t.Quantity : 0) -
                                (t.SourceWarehouseId == warehouse.Id ? t.Quantity : 0))
                             select new InventoryDTO
                             {
                                 ProductId = product.Id,
                                 ProductName = product.Name,
                                 WarehouseId = warehouse.Id,
                                 WarehouseName = warehouse.Name,
                                 QuantityInStock = quantity
                             }).ToList();
        return inventory;
    }
}


