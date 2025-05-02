using RoboostTask.DTOs.Products;
using RoboostTask.DTOs.Stocks;

namespace RoboostTask.DTOs.Reports
{
    public class LowStockReportDTO
    {
        public bool IsLowStock { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; } = 0;
        public int LowStockThreshold { get; set; }

        public List<StockForLowDTO> Stocks { get; set; } = new List<StockForLowDTO>();
        public int TotalWarehouses { get; set; }
        public int TotalActiveStock { get; set; }
        public int DeficitAmount { get; set; }
    }
}
