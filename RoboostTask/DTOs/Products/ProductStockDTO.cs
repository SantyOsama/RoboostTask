namespace RoboostTask.DTOs.Products
{
    public class ProductStockDTO
    {
        public Guid WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public string WarehouseLocation { get; set; }
        public int QuantityInStock { get; set; }
        public bool IsActive { get; set; }
    }
}
