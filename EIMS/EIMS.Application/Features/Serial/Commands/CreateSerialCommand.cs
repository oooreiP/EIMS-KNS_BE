using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.DTOs.Serials;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Serial.Commands
{
    public class CreateSerialCommand : IRequest<Result<SerialResponse>>
    {
        public int PrefixID { get; set; } 
        public int SerialStatusID { get; set; }
        public string Year { get; set; } 
        public int InvoiceTypeID { get; set; }
        public string Tail { get; set; } = "YY";
    }
}