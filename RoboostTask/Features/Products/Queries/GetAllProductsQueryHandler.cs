using MediatR;
using Microsoft.EntityFrameworkCore;
using RoboostTask.Data;
using RoboostTask.DTOs.Products;
using RoboostTask.GeneralResponse;
using RoboostTask.Models;
using RoboostTask.Repositories.Interfaces;

namespace RoboostTask.Features.Products.Queries
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Response<List<GetProductResponse>>>
    {
        private readonly IProductRepository _productRepository;
        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Response<List<GetProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();

            var productDtos = products
                .Where(p => !p.IsDeleted) 
                .Select(p => new GetProductResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    LowStockThreshold = p.LowStockThreshold,
                    IsLowStock = p.Quantity <= p.LowStockThreshold
                })
                .ToList();

            return Response<List<GetProductResponse>>.Success(productDtos,"Products retrieved successfully.");
        }
    }
}
