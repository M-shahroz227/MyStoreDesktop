using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyStoreDesktop.Models
{
    public class Return
    {
        [Key]
        public int ReturnId { get; set; }

        [ForeignKey("Bill")]
        public int BillId { get; set; }

        public DateTime ReturnDate { get; set; }
        public decimal TotalAmount { get; set; }

        public virtual Bill Bill { get; set; }
        public virtual ICollection<ReturnItem> ReturnItems { get; set; }
            = new HashSet<ReturnItem>();
    }
}
