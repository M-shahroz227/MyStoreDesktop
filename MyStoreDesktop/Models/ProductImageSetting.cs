using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyStoreDesktop.Models
{
    public class ProductImageSetting
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [MaxLength(500)]
        public string ImagePath { get; set; }

        [Required]
        public Drive DriveLabel { get; set; }  // 👈 Enum property

        // 👇 Ye property sirf EF ke liye string storage me use hogi
        [NotMapped]
        public string DriveLabelString
        {
            get => DriveLabel.ToString();
            set => DriveLabel = (Drive)Enum.Parse(typeof(Drive), value);
        }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
