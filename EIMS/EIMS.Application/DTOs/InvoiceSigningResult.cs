using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs
{
    public class InvoiceSigningResult
    {
        public string SignedXml { get; set; } = string.Empty;    
        public string SignatureValue { get; set; } = string.Empty; 
    }
}
