using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyStoreDesktop.Models
{
    public class BillProduct
    {
        [Key]
        public int BillProductId { get; set; }

        [ForeignKey("Bill")]
        public int BillId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual Bill Bill { get; set; }
        public virtual Product Product { get; set; }
    }
}
