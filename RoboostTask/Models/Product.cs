using System.ComponentModel.DataAnnotations;

namespace RoboostTask.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int LowStockThreshold { get; set; } = 3;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<InventoryTransaction> Transactions { get; set; }

    }
}
