using System.ComponentModel.DataAnnotations;

namespace RoboostTask.Models
{
    public class Warehouse
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Location { get; set; }

        //** بفكر افصلهم يبقي الحاجات ال انا كنت فيها سورس لوحدها والحاجات ال انا كنت فيها مستقبل لوحدها **//
        public ICollection<InventoryTransaction> SourceTransactions { get; set; }
        public ICollection<InventoryTransaction> DestinationTransactions { get; set; }

    }
}
