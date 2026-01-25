using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Invoices
{
    public class InvoiceSymbolDto
    {
        public string MauSo { get; set; }  // khmsHDon
        public string KyHieu { get; set; } // khHDon
        public string FullSymbol => $"{MauSo}{KyHieu}";
    }
}
