using System.ComponentModel.DataAnnotations;

namespace RoboostTask.Models
{
    public class Warehouse:Base
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Location { get; set; }
        public ICollection<InventoryTransaction> SourceTransactions { get; set; }
        public ICollection<InventoryTransaction> DestinationTransactions { get; set; }
        public  ICollection<Stock> Stocks { get; set; }

    }
}
