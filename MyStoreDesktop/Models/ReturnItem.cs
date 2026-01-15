using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyStoreDesktop.Models
{
    public class ReturnItem
    {
        [Key]
        public int ReturnItemId { get; set; }

        [ForeignKey("Return")]
        public int ReturnId { get; set; }

        [ForeignKey("BillProduct")]
        public int BillProductId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public int ReturnQuantity { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual Return Return { get; set; }
        public virtual BillProduct BillProduct { get; set; }
        public virtual Product Product { get; set; }
    }
}
