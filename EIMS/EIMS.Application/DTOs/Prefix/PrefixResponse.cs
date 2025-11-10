using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Prefix
{
    public class PrefixResponse
    {
        public int PrefixID { get; set; }
        public string PrefixName { get; set; } = string.Empty;
    }
}