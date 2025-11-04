using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Serials
{
    public class CreateSerialRequest
    {
        [Required]
        public int PrefixID { get; set; } // Ký tự 1 (Số 1-9)
        [Required]
        public int SerialStatusID { get; set; } // Ký tự 2 (C hoặc K)
        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string Year { get; set; } // Ký tự 3, 4 (e.g., "25")
        [Required]
        public int InvoiceTypeID { get; set; } // Ký tự 5 (T, D, L, etc.)
        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string Tail { get; set; } = "YY"; // Ký tự 6, 7 (e.g., "YY")
    }
}