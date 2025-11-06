using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyStoreDesktop.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        [MaxLength(150)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        public virtual ICollection<Bill> Bills { get; set; } = new HashSet<Bill>();
    }
}
