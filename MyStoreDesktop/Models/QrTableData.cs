using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyStoreDesktop.Models
{
    public class QrTableData
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        [MaxLength(255)]
        public string QrCode { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
