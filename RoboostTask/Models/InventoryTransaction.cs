using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RoboostTask.Enums.TransactionEnum;

namespace RoboostTask.Models
{
    public class InventoryTransaction
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public TransactionType TransactionType { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public ApplicationUser User { get; set; }

        public int? SourceWarehouseId { get; set; }

        [ForeignKey("SourceWarehouseId")] 
        public Warehouse SourceWarehouse { get; set; }

        public int? DestinationWarehouseId { get; set; }

        [ForeignKey("DestinationWarehouseId")]  
        public Warehouse DestinationWarehouse { get; set; }

        //قابلتني مشكلة وان عندي اتنين اوبجيكت من وير هاوس هيميز ازاي بينهم
        //هروح احطها في الدي بي ست اعمل اوفررايد لفانكشن اون موديل كرييتنج
    }
}
