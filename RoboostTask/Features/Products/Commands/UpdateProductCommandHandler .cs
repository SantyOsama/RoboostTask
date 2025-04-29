using MediatR;
using Microsoft.EntityFrameworkCore;
using RoboostTask.Data;
using RoboostTask.GeneralResponse;
using RoboostTask.Models;

namespace RoboostTask.Features.Products.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<string>>
    {
        private readonly AppDbContext _context;

        public UpdateProductCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Response<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (product == null)
            {
                return new Response<string>().Fail(null, "Product not found.");
            }

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Quantity = request.Quantity;
            product.LowStockThreshold = request.LowStockThreshold;

            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);
          
            return new Response<string>().Success(string.Empty, "Product updated successfully.");
        }
    }
}
