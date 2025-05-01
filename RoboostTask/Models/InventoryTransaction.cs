using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RoboostTask.Enums.TransactionEnum;

namespace RoboostTask.Models
{
    public class InventoryTransaction:Base
    {

        [Required]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public TransactionType TransactionType { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey(nameof(User))]
        public string PerformedByUserId { get; set; }
        [Required]
        public ApplicationUser User { get; set; }

        public Guid? SourceWarehouseId { get; set; }

        [ForeignKey("SourceWarehouseId")] 
        public Warehouse SourceWarehouse { get; set; }

        public Guid? DestinationWarehouseId { get; set; }

        [ForeignKey("DestinationWarehouseId")]  
        public Warehouse DestinationWarehouse { get; set; }

        //قابلتني مشكلة وان عندي اتنين اوبجيكت من وير هاوس هيميز ازاي بينهم
        //هروح احطها في الدي بي ست اعمل اوفررايد لفانكشن اون موديل كرييتنج


        public Guid? WarehouseId { get; set; }
        [ForeignKey("WarehouseId")]
        public Warehouse Warehouse { get; set; }

    }
}
