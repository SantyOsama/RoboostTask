using MediatR;
using Microsoft.EntityFrameworkCore;
using RoboostTask.Data;
using RoboostTask.GeneralResponse;
using RoboostTask.Models;
using RoboostTask.Repositories.Interfaces;

namespace RoboostTask.Features.Products.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Response<bool>>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository, IStockRepository stockRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Response<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product == null)
                return  Response<bool>.Fail("Product not found.", false);

            if (product.IsDeleted)
                return Response<bool>.Fail("Product is already deleted.", false, statusCode: 410);


            await _productRepository.SoftDeleteAsync(request.Id);

            await _productRepository.SaveChangesAsyc();

            var deletedProduct = await _productRepository.GetByIdAsync(request.Id);
            if (deletedProduct?.IsDeleted != true)
                return Response<bool>.Fail("Failed to delete product.", false, statusCode: 500);

            return Response<bool>.Success(true,"Product deleted successfully",statusCode: 200);
        }
    }
}
