using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Domain.Entities
{
    public class PaymentStatus
    {
        [Key]
        public int PaymentStatusID { get; set; }

        [Required]
        [StringLength(50)]
        public string StatusName { get; set; } = string.Empty;

        // --- Navigation Properties ---
        [InverseProperty("PaymentStatus")]
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}