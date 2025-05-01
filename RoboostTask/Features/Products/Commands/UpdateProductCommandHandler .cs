using MediatR;
using Microsoft.EntityFrameworkCore;
using RoboostTask.Data;
using RoboostTask.DTOs.Products;
using RoboostTask.GeneralResponse;
using RoboostTask.Models;
using RoboostTask.Repositories.Interfaces;

namespace RoboostTask.Features.Products.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<string>>
    {
        private readonly IProductRepository _productRepository;
        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Response<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductRequest.Id);

            if (product == null || product.IsDeleted)
            {
                return  Response<string>.Fail("Product not found.");
            }

            product.Name = request.ProductRequest.Name;
            product.Description = request.ProductRequest.Description;
            product.Price = request.ProductRequest.Price;
            product.Quantity = request.ProductRequest.Quantity;
            product.LowStockThreshold = request.ProductRequest.LowStockThreshold;

            await _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsyc();

            return Response<string>.Success(string.Empty, "Product updated successfully.");
        }
    }
}
