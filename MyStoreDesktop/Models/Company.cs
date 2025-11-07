using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyStoreDesktop.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}

