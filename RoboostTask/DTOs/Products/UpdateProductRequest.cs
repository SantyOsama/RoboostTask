using System.ComponentModel.DataAnnotations;

namespace RoboostTask.DTOs.Products
{
    public class UpdateProductRequest
    {
        [Required(ErrorMessage = "Product ID is required")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be positive")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative")]
        public int Quantity { get; set; } = 0;

        [Range(1, int.MaxValue, ErrorMessage = "Threshold must be at least 1")]
        public int LowStockThreshold { get; set; } = 3;
    }
}
