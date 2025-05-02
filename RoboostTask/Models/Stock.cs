using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RoboostTask.Models
{
    public class Stock :Base
    {
        [Required]
        public Guid ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required]
        public Guid WarehouseId { get; set; }

        [ForeignKey("WarehouseId")]
        public Warehouse Warehouse { get; set; }

        [Range(0, int.MaxValue)]
        public int QuantityInStock { get; set; }
        public bool IsActive {  get; set; }=false;
    }
}
