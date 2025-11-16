using System;
using System.ComponentModel.DataAnnotations;

namespace MyStoreDesktop.Models
{
    public class Configuration
    {
        [Key]
        public int ConfigId { get; set; }

        public string Key { get; set; }
        public string Value { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
