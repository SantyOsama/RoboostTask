using MediatR;
using Microsoft.EntityFrameworkCore;
using RoboostTask.Data;
using RoboostTask.Models;
using RoboostTask.GeneralResponse;
using RoboostTask.DTOs.Products;

namespace RoboostTask.Features.Products.Commands
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Response<string>>
    {
        private readonly AppDbContext _context;

        public AddProductCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Response<string>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.ProductRequest.Name,
                Description = request.ProductRequest.Description,
                Price = request.ProductRequest.Price,
                Quantity = request.ProductRequest.Quantity,
                LowStockThreshold = request.ProductRequest.LowStockThreshold,
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);
            return new Response<string>().Success(string.Empty,"Product added successfully.");
        }

    }
}
