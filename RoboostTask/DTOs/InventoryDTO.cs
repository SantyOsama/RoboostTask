namespace RoboostTask.DTOs
{
    public class InventoryDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }

        public int QuantityInStock { get; set; }
    }
}
