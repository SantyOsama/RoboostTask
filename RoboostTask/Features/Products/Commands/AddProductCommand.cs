using MediatR;
using RoboostTask.GeneralResponse;

namespace RoboostTask.Features.Products.Commands
{
    public class AddProductCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int LowStockThreshold { get; set; } = 3;
        public int? CategoryId { get; set; }
    }
}
