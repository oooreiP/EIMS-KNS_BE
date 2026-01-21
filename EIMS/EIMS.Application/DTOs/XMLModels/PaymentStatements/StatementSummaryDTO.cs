using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.XMLModels.PaymentStatements
{
    public class StatementSummaryDTO
    {
        public decimal TotalAmount { get; set; }
        public decimal TotalVAT { get; set; }
        public decimal TotalCurrentPeriod { get; set; }
        public decimal TotalPreviousDebt { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
