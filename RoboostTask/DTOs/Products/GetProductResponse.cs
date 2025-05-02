using System.ComponentModel.DataAnnotations;

namespace RoboostTask.DTOs.Products
{
    public class GetProductResponse
    {
        public Guid Id { get; set; }
        public bool IsLowStock { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; } = 0;
        public int LowStockThreshold { get; set; }

        public List<ProductStockDTO> Stocks { get; set; } = new List<ProductStockDTO>();
        public int TotalWarehouses { get; set; }
        public int TotalActiveStock { get; set; }


    }
}
