using MediatR;
using Microsoft.EntityFrameworkCore;
using RoboostTask.Data;
using RoboostTask.DTOs.Products;
using RoboostTask.GeneralResponse;
using RoboostTask.Models;

namespace RoboostTask.Features.Products.Queries
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Response<List<GetProductResponse>>>
    {
        private readonly AppDbContext _context;
        public GetAllProductsQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Response<List<GetProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Products.AsNoTracking()
                .Select(p => new GetProductResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    IsLowStock = p.Quantity <= p.LowStockThreshold
                })
                .ToListAsync(cancellationToken);

            return Response<List<GetProductResponse>>.Success(products, "Products retrieved successfully.");
        }
    }
}
