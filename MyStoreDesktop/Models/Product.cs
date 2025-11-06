using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyStoreDesktop.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [MaxLength(50)]
        public string BarCodeId { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        public string Company { get; set; }
        public string Model { get; set; }

        public int Quantity { get; set; }
        public decimal SalePrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal Discount { get; set; }

        [MaxLength(100)]
        public string Category { get; set; }

        public string UrlImage { get; set; }
        public string Description { get; set; }

        public virtual ICollection<BillProduct> BillProducts { get; set; } = new HashSet<BillProduct>();
    }
}
