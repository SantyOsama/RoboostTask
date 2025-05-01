using MediatR;
using Microsoft.EntityFrameworkCore;
using RoboostTask.Data;
using RoboostTask.Models;
using RoboostTask.GeneralResponse;
using RoboostTask.DTOs.Products;

namespace RoboostTask.Features.Products.Commands
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Response<Guid>>
    {
        private readonly AppDbContext _context;
        public AddProductCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Response<Guid>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var exists = await _context.Products
                .AnyAsync(p => p.Name == request.ProductRequest.Name);

            if (exists)
                return Response<Guid>.Fail("Product name already exists", statusCode: 409);

            var product = new Product
            {
                Name = request.ProductRequest.Name,
                Description = request.ProductRequest.Description,
                Price = request.ProductRequest.Price,
                Quantity = request.ProductRequest.Quantity,
                LowStockThreshold = request.ProductRequest.LowStockThreshold
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return Response<Guid>.Success(product.Id, "Product created successfully");
        }

    }
}
