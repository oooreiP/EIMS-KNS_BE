using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Domain.Entities
{
    public class TaxApiStatus
    {
        [Key]
        public int TaxApiStatusID { get; set; }
        [StringLength(255)]
        public string? StatusName { get; set; }

        // --- Navigation Properties ---
        [InverseProperty("TaxApiStatus")]
        public virtual ICollection<TaxApiLog> TaxApiLogs { get; set; } = new List<TaxApiLog>();
    }
}