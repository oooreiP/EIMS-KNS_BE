using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Minutes.Commands
{
    public class SignMinuteInvoiceCommand : IRequest<Result<string>>
    {
        public int MinuteInvoiceId { get; set; }
        public string SearchText { get; set; } = "ĐẠI DIỆN BÊN B"; // Mặc định vị trí ký của bên bán
        public string RootPath { get; set; } 

        public SignMinuteInvoiceCommand(int id, string searchText, string rootPath)
        {
            MinuteInvoiceId = id;
            SearchText = searchText;
            RootPath = rootPath;
        }
    }
}
