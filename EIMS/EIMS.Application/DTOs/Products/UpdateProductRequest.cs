using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Products
{
    public class UpdateProductRequest
    {
        public string? Name { get; set; }
        public int? CategoryID { get; set; }
        public string? Unit { get; set; }
        public decimal? BasePrice { get; set; }
        public decimal? VATRate { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
    }
}
