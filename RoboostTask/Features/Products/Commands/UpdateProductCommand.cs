using MediatR;
using RoboostTask.GeneralResponse;
using RoboostTask.Models;
using System.ComponentModel.DataAnnotations;

namespace RoboostTask.Features.Products.Commands
{
    public class UpdateProductCommand:IRequest<Response<Product>>
    {
        public int Id { get; set ; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int LowStockThreshold { get; set; }
        public int Quantity { get; set; }
        public int? CategoryId { get; set; }

    }
}
