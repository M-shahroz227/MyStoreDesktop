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

        // Store VALUE (QR GUID, Barcode Text, Manual Code)
        [Required]
        public string CodeValue { get; set; }

        // Store TYPE: "QR", "BARCODE", "MANUAL"
        [Required]
        public string CodeType { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public object QrCode { get; internal set; }
    }
}
