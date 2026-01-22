using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.XMLModels.PaymentStatements
{
    public class StatementItemDTO
    {
        public string Description { get; set; }
        public string PeriodFrom { get; set; }
        public string PeriodTo { get; set; }
        public string IndicatorOld { get; set; }
        public string IndicatorNew { get; set; }

        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; } 

        public double VATRate { get; set; } 
        public decimal VATAmount { get; set; }

        public decimal TotalCurrent { get; set; } 
        public decimal PreviousDebt { get; set; }
        public decimal TotalPayable { get; set; } 
    }
}
