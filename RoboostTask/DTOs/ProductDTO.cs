using System.ComponentModel.DataAnnotations;

namespace RoboostTask.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int LowStockThreshold { get; set; }
    }
}
