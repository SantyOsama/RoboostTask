using MediatR;
using RoboostTask.Models;
using RoboostTask.Repositories.Interfaces;

namespace RoboostTask.Features.Products.Queries
{
    public class GetProductEntityByIdQueryHandler : IRequestHandler<GetProductEntityByIdQuery, Product?>
    {
        private readonly IProductRepository _productRepository;

        public GetProductEntityByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product?> Handle(GetProductEntityByIdQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetByIdWithStocksAsync(request.ProductId);
        }
    }

}
