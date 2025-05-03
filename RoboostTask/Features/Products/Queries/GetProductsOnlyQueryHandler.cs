using MediatR;
using RoboostTask.Models;
using RoboostTask.Repositories.Interfaces;

namespace RoboostTask.Features.Products.Queries
{
    public class GetProductsOnlyQueryHandler : IRequestHandler<GetProductsOnlyQuery, List<Product>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsOnlyQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> Handle(GetProductsOnlyQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetAllWithStocksAsync();
        }
    }

}
