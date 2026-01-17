using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Domain.Entities
{
    public class InvoiceRequestStatus
    {
        [Key]
        public int StatusID { get; set; } 

        [Required]
        [StringLength(50)]
        public string StatusName { get; set; }

        [StringLength(100)]
        public string? Description { get; set; }
        [InverseProperty("RequestStatus")]
        public virtual ICollection<InvoiceRequest> InvoiceRequests { get; set; } = new List<InvoiceRequest>();
    }
}
