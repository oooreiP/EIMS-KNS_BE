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
        public DateTime StatementDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; }
        // [ForeignKey("CreatedBy")]
        public int CreatedBy { get; set; }
        // 2. Số dư đầu kỳ (Nợ cũ chuyển sang)
        public int PeriodMonth { get; set; }
        public int PeriodYear { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OpeningBalance { get; set; } = 0;

        // 3. Phát sinh trong kỳ (Tổng các hóa đơn mới)

        [Column(TypeName = "decimal(18, 2)")]

        public decimal NewCharges { get; set; } = 0;
        // [ForeignKey("CustomerID")]
        public int CustomerID { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PaidAmount { get; set; } = 0;
        public int? TotalInvoices { get; set; }
        [Column(TypeName = "decimal(18, 2)")] // Assuming precision
        public decimal TotalAmount { get; set; } = 0;

        public int StatusID { get; set; }

        public string? Notes { get; set; }
        [NotMapped]
        public bool IsOverdue =>
                    (StatusID == 3 || StatusID == 4) && // If Sent or Partially Paid
                    DateTime.Now > DueDate;

        // Helper to quickly see what is left to pay
        [NotMapped]
        public decimal BalanceDue => TotalAmount - PaidAmount;
        //navigations
        [ForeignKey("CustomerID")]
        [InverseProperty("Statements")]
        public virtual Customer? Customer { get; set; }
        [ForeignKey("CreatedBy")]
        [InverseProperty("CreatedStatements")]
        public virtual User Creator { get; set; }
        [ForeignKey("StatusID")]
        [InverseProperty("InvoiceStatements")]
        public virtual StatementStatus StatementStatus { get; set; }
        [InverseProperty("Statement")]
        public virtual ICollection<InvoiceStatementDetail> StatementDetails { get; set; } = new List<InvoiceStatementDetail>();

    }
}