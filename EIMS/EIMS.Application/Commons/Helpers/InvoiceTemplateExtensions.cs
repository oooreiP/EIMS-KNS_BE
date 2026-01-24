using EIMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Commons.Helpers
{
    public static class InvoiceTemplateExtensions
    {
        public static (string KHMSHDon, string KHHDon) BuildInvoiceSymbol(this InvoiceTemplate template)
        {
            var serial = template.Serial
                ?? throw new InvalidOperationException("Serial not loaded");

            var prefix = serial.Prefix
                ?? throw new InvalidOperationException("Prefix not loaded");

            string khmsHDon = prefix.PrefixID.ToString();

            string khHDon =
                serial.SerialStatus!.Symbol +
                serial.Year +
                serial.InvoiceType!.Symbol +
                serial.Tail;

            return (khmsHDon, khHDon);
        }
    }
}
