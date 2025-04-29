using MediatR;
using Microsoft.EntityFrameworkCore;
using RoboostTask.Data;
using RoboostTask.DTOs.Products;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Products.Queries
{
    public class GetProductByIdQueryHandler: IRequestHandler<GetProductByIdQuery, Response<GetProductResponse>>
    {
        private readonly AppDbContext _context;
        public GetProductByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Response<GetProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (product == null)
                return new Response<GetProductResponse>().Fail(null,"Product not found.");

            var productDto = new GetProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                LowStockThreshold = product.LowStockThreshold
            };

            return new Response<GetProductResponse>().Success(productDto);
        }
    }
}
