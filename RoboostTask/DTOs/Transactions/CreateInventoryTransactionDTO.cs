using System.ComponentModel.DataAnnotations;
using static RoboostTask.Enums.TransactionEnum;

namespace RoboostTask.DTOs.Transactions
{
    public class CreateInventoryTransactionDTO
    {
        [Required(ErrorMessage = "Product ID is required")]
        public Guid ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Transaction type is required")]
        public TransactionType TransactionType { get; set; }

        public Guid? SourceWarehouseId { get; set; }

        public Guid? DestinationWarehouseId { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public Guid UserId { get; set; }
    }
}
