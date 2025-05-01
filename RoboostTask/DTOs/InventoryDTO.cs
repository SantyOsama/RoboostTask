namespace RoboostTask.DTOs
{
    public class InventoryDTO
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }

        public Guid WarehouseId { get; set; }
        public string WarehouseName { get; set; }

        public int QuantityInStock { get; set; }
    }
}
