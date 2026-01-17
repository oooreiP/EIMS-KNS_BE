using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EIMS.Domain.Entities
{
    public class InvoiceRequestItem
    {
        [Key]
        public int RequestItemID { get; set; }

        public int RequestID { get; set; }
        [ForeignKey("RequestID")]
        [JsonIgnore]
        public virtual InvoiceRequest InvoiceRequests { get; set; }

        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public double Quantity { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; } 

        [Column(TypeName = "decimal(18, 2)")]
        public decimal VATAmount { get; set; }

        public string? Note { get; set; }
    }
}
