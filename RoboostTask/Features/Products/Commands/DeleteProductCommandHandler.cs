using MediatR;
using RoboostTask.Data;
using RoboostTask.GeneralResponse;
using RoboostTask.Models;

namespace RoboostTask.Features.Products.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Response<bool>>
    {
        private readonly AppDbContext _context;
        public DeleteProductCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Response<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(request.Id);

            if (product == null)
                return  Response<bool>.Fail("Product not found.", false);

            product.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);

            return  Response<bool>.Success(true, "Product deleted successfully");
        }
    }
}
