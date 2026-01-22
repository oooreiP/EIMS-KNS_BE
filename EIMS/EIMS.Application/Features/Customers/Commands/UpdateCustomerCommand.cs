using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Customers.Commands
{
    public class UpdateCustomerCommand : IRequest<Result>
    {
        [Required]
        public int? CustomerID { get; set; } 

        [Required]
        public string CustomerName { get; set; } = string.Empty;

        public string? TaxCode { get; set; } 

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string ContactEmail { get; set; } = string.Empty;

        public string? ContactPerson { get; set; }

        public string? ContactPhone { get; set; }

        public bool? IsActive { get; set; } 
    }
}

