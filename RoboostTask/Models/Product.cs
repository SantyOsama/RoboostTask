using System.ComponentModel.DataAnnotations;

namespace RoboostTask.Models
{
    public class Product:Base
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int LowStockThreshold { get; set; } = 3;
        public bool IsDeleted { get; set; } = false;
        public ICollection<Stock> Stocks { get; set; } = new List<Stock>(); 
        public ICollection<InventoryTransaction> Transactions { get; set; }

    }
}
