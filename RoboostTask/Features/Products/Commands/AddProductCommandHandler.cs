using MediatR;
using Microsoft.EntityFrameworkCore;
using RoboostTask.Data;
using RoboostTask.Models;
using RoboostTask.GeneralResponse;
using RoboostTask.DTOs.Products;
using RoboostTask.Repositories.Interfaces;

namespace RoboostTask.Features.Products.Commands
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Response<Guid>>
    {
        private readonly IProductRepository _productRepository;
        public AddProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
 
       public async Task<Response<Guid>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            if (request?.ProductRequest == null)
                return Response<Guid>.Fail("Product data is required", statusCode: 400);
            var exists = await _productRepository.ProductExistsAsync(request.ProductRequest.Name);
            if (exists)
                return Response<Guid>.Fail("Product name already exists", statusCode: 409);

            var product = new Product
            {
                Name = request.ProductRequest.Name,
                Description = request.ProductRequest.Description,
                Price = request.ProductRequest.Price,
                Quantity = request.ProductRequest.Quantity,
                LowStockThreshold = request.ProductRequest.LowStockThreshold,
                IsDeleted = false 
            };
            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsyc();

            return Response<Guid>.Success(product.Id,"Product created successfully", statusCode: 201);
        }

    }
}
