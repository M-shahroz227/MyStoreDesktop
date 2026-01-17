using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyStoreDesktop.Models
{
    public class BillHistory
    {
        [Key]
        public int BillHistoryId { get; set; }

        [ForeignKey(nameof(Bill))]
        public int BillId { get; set; }

        // BEFORE (nvarchar(1000))
        [StringLength(1000)]
        public string BeforeJson { get; set; }

        // AFTER (nvarchar(1000))
        [StringLength(1000)]
        public string AfterJson { get; set; }

        // Snapshot full (nvarchar(max))
        public string SnapshotJson { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual Bill Bill { get; set; }
    }
}
