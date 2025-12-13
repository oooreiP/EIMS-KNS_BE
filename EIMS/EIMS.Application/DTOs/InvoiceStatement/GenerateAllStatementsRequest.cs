using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.InvoiceStatement
{
    public class GenerateAllStatementsRequest
    {
        public int Month { get; set; }
        public int Year { get; set; }
    }
}