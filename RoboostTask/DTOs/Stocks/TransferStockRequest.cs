using System.ComponentModel.DataAnnotations;

namespace RoboostTask.DTOs.Stocks
{
    public class TransferStockRequest
    {
        [Required(ErrorMessage = "Product ID is required")]
        public Guid ProductId { get; set; }
        [Required(ErrorMessage = "Source warehouse ID is required")]

        public Guid FromWarehouseId { get; set; }

      //  [NotEqual(nameof(FromWarehouseId), ErrorMessage = "Cannot transfer to the same warehouse")]
        public Guid ToWarehouseId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]

        public int Quantity { get; set; }

    }
}
