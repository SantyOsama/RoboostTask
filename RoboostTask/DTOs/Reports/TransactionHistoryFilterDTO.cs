using static RoboostTask.Enums.TransactionEnum;

namespace RoboostTask.DTOs.Reports
{
    public class TransactionHistoryFilterDTO
    {
        public Guid? ProductId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TransactionType? TransactionType { get; set; }
        public Guid? WarehouseId { get; set; }
        public Guid? UserId { get; set; }
        //public Guid? CategoryId { get; set; }
    }
}
