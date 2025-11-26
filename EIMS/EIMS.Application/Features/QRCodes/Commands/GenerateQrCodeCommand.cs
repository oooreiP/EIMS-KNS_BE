using EIMS.Application.DTOs;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.QRCodes.Commands
{
    public record GenerateQrCodeCommand(
        string SellerTaxCode,
        string InvoiceSymbol,     // Ký hiệu mẫu số hoá đơn (VD: 1C21TYY)
        string InvoiceNo,         // Số hoá đơn (VD: 00001234)
        decimal TotalAmount,      // Tổng tiền thanh toán
        DateTime IssueDate,       // Ngày lập hoá đơn
        string SellerName,        // Tên người bán
        string? City = "HANOI"    // Thành phố hiển thị QR
    ) : IRequest<Result<GenerateQrCodeResult>>;
}
