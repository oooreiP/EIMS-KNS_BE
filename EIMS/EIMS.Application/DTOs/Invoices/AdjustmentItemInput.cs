using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Invoices
{
    public class InvoiceItemInputDto
    {
        [Required]
        public int ProductID { get; set; }
        public double Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? OverrideVATRate { get; set; }
    }
}
