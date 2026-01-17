using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyStoreDesktop.Models
{
    public class Bill
    {
        [Key]
        public int BillId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public int CustomerInvoiceId { get; set; }

        [ForeignKey("CustomerInvoiceId")]
        public CustomerInvoice CustomerInvoice { get; set; }

        public string Role { get; set; }
        public DateTime BillDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime OwnDate { get; set; }

        public decimal Discount { get; set; }
        public decimal itemPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Tax { get; set; }
        public decimal GrandTotal { get; set; }

        [MaxLength(50)]
        public string PaymentMethod { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<BillProduct> BillProducts { get; set; }
            =   new HashSet<BillProduct>();
        public virtual ICollection<BillHistory> BillHistories { get; set; } // <-- FIX: Change type from ICollection<object> to ICollection<BillHistory>
    }
}