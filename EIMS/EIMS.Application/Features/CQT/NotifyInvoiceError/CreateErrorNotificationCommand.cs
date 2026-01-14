using EIMS.Application.DTOs.TaxAPIDTO;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.CQT.NotifyInvoiceError
{
    public class CreateErrorNotificationCommand : IRequest<Result<CreateErrorNotificationResponse>>
    {

        public string NotificationType { get; set; } = "Thông báo hủy/giải trình của Người nộp thuế";

        [Required(ErrorMessage = "Số thông báo (NotificationNumber) là bắt buộc.")]
        public string NotificationNumber { get; set; } 

        public string TaxAuthority { get; set; } 
        public string? TaxAuthorityCode { get; set; } 

        public DateTime? CreatedDate { get; set; }

        [Required(ErrorMessage = "Địa danh (Place) không được để trống.")]
        public string Place { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "Phải có ít nhất một dòng chi tiết sai sót.")]
        public List<ErrorDetailDto> ErrorItems { get; set; }
    }
}
