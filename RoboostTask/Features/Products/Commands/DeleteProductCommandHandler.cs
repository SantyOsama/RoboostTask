using MediatR;
using RoboostTask.Data;
using RoboostTask.GeneralResponse;
using RoboostTask.Models;

namespace RoboostTask.Features.Products.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Response<string>>
    {
        private readonly AppDbContext _context;
        public DeleteProductCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Response<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(request.Id);

            if (product == null)
                return new Response<string>().Fail(null, "Product not found.");

            product.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);

            return new Response<string>().Success(null, "Product deleted successfully");
        }
    }
}
