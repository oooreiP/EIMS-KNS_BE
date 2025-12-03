using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Customers.Commands
{
    public class CreateCustomerCommand : IRequest<Result<int>>
    {
        [Required(ErrorMessage = "Customer Name is required.")]
        public string CustomerName { get; set; } = string.Empty;

        public string TaxCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string ContactEmail { get; set; } = string.Empty;

        public string? ContactPerson { get; set; }
        public string? ContactPhone { get; set; }
        public bool? IsActive { get; set; }
    }

}