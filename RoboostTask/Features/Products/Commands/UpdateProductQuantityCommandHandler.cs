using MediatR;
using RoboostTask.GeneralResponse;
using RoboostTask.Repositories.Interfaces;

namespace RoboostTask.Features.Products.Commands
{
    public class UpdateProductQuantityCommandHandler : IRequestHandler<UpdateProductQuantityCommand, Response<string>>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductQuantityCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Response<string>> Handle(UpdateProductQuantityCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);

            if (product == null || product.IsDeleted)
                return Response<string>.Fail("Product not found or is deleted.");

            product.Quantity += request.QuantityToAdd;

            await _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsyc();

            return Response<string>.Success("Product quantity updated.");
        }
    }

}
