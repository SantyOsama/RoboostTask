using MediatR;
using Microsoft.EntityFrameworkCore;
using RoboostTask.Data;
using RoboostTask.Models;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Products.Commands
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Response<int>>
    {
        private readonly AppDbContext _context;

        public AddProductCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Response<int>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Quantity = request.Quantity,
                LowStockThreshold = request.LowStockThreshold,
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);
            return new Response<int>(product.Id, "Product added successfully.");
        }
    }
}
