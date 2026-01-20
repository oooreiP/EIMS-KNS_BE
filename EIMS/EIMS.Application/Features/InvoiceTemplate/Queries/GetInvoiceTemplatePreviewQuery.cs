using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceTemplate.Queries
{
    public class GetInvoiceTemplatePreviewQuery : IRequest<string> 
    {
        public int TemplateID { get; set; }
        public int? CompanyID { get; set; }
        public string? RootPath { get; set; }
                                          
    }
}
