using System.ComponentModel.DataAnnotations;

namespace RoboostTask.DTOs.Stocks
{
    public class AddStockRequest
    {
        [Required(ErrorMessage = "Product ID is required")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Warehouse ID is required")]
        public Guid WarehouseId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

    }
}
