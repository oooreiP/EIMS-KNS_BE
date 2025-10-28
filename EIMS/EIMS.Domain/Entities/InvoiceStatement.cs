using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Domain.Entities
{
    public class InvoiceStatement
    {
        [Key]
        public int StatementID { get; set; }
        public string? StatementCode { get; set; }
        public DateTime? StatementDate { get; set; }
        [ForeignKey("CreatedBy")]
        public int CreatedBy { get; set; }
        [ForeignKey("CustomerID")]
        public int? CustomerID { get; set; }
        public int? TotalInvoices { get; set; }
        [Column(TypeName = "decimal(18, 2)")] // Assuming precision
        public decimal? TotalAmount { get; set; }
        public int StatusID { get; set; }
        [ForeignKey("StatusID")]

        public string? Notes { get; set; }
        //navigations
        [InverseProperty("Statements")]
        public virtual Customer? Customer { get; set; }
        [InverseProperty("CreatedStatements")]
        public virtual User Creator { get; set; }
        [InverseProperty("InvoiceStatements")]
        public virtual StatementStatus StatementStatus { get; set; }


    }
}