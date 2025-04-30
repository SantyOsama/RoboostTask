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
            var product = await _context.Products.FindAsync(request.ProductRequest.Id,cancellationToken);

            if (product == null)
            {
                return  Response<string>.Fail(null, "Product not found.");
            }

            product.Name = request.ProductRequest.Name;
            product.Description = request.ProductRequest.Description;
            product.Price = request.ProductRequest.Price;
            product.Quantity = request.ProductRequest.Quantity;
            product.LowStockThreshold = request.ProductRequest.LowStockThreshold;

            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);
          
            return Response<string>.Success(string.Empty, "Product updated successfully.");
        }
    }
}
