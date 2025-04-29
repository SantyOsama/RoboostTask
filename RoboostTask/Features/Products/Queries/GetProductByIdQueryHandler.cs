using MediatR;
using Microsoft.EntityFrameworkCore;
using RoboostTask.Data;
using RoboostTask.DTOs;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Products.Queries
{
    public class GetProductByIdQueryHandler: IRequestHandler<GetProductByIdQuery, Response<ProductDTO>>
    {
        private readonly AppDbContext _context;

        public GetProductByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Response<ProductDTO>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (product == null)
                return new Response<ProductDTO>("Product not found.");

            var productDto = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                LowStockThreshold = product.LowStockThreshold
            };

            return new Response<ProductDTO>(productDto);
        }
    }
}
