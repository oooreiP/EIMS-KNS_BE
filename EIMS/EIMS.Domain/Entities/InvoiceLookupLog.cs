using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Domain.Entities
{
    public class InvoiceLookupLog
    {
        [Key]
        public int LogID { get; set; }
        public string LookupCode { get; set; } = string.Empty;
        public string? IPAddress { get; set; }
        public string? UserAgent { get; set; }
        public DateTime Time { get; set; } = DateTime.UtcNow;
        public bool IsSuccess { get; set; }
        
        // Optional: Link to Invoice ID if found (nullable)
        public int? FoundInvoiceID { get; set;}
    }
}