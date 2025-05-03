using MediatR;
using RoboostTask.DTOs.Products;
using RoboostTask.DTOs.Reports;
using RoboostTask.DTOs.Stocks;
using RoboostTask.Models;
using RoboostTask.Repositories.Interfaces;

namespace RoboostTask.Features.Reports.Queries
{
    public class GetLowStockProductsQueryHandler : IRequestHandler<GetLowStockProductsQuery, List<Product>>
    {
        private readonly IProductRepository _productRepository;

        public GetLowStockProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> Handle(GetLowStockProductsQuery request, CancellationToken cancellationToken)
        {
            return (await _productRepository.GetLowStockProductsAsync()).ToList();
        }
    }
}

