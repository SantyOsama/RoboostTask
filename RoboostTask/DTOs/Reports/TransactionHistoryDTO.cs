using static RoboostTask.Enums.TransactionEnum;

namespace RoboostTask.DTOs.Reports
{
    public class TransactionHistoryDTO
    {
        public DateTime Date { get; set; }
        public string ProductName { get; set; }
        public TransactionType Type { get; set; }
        public int Quantity { get; set; }
        public string SourceWarehouse { get; set; }
        public string DestinationWarehouse { get; set; }
        public string PerformedBy { get; set; }
    }
}
