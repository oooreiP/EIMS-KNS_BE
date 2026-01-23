using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedEmailData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OriginalBodyContent",
                table: "EmailTemplate",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 10, 23, 15, 975, DateTimeKind.Utc).AddTicks(721));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 10, 23, 15, 975, DateTimeKind.Utc).AddTicks(728));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 10, 23, 15, 975, DateTimeKind.Utc).AddTicks(730));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "OriginalBodyContent" },
                values: new object[] { new DateTime(2026, 1, 23, 10, 23, 16, 552, DateTimeKind.Utc).AddTicks(9914), "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <style>\r\n        .email-container { font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; background-color: #f4f4f7; padding: 40px 20px; }\r\n        .email-content { max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); overflow: hidden; }\r\n        .email-header { background-color: #007BFF; padding: 30px; text-align: center; color: #ffffff; }\r\n        .email-header h1 { margin: 0; font-size: 24px; font-weight: bold; }\r\n        .email-body { padding: 30px; color: #333333; line-height: 1.6; }\r\n        .info-table { width: 100%; border-collapse: collapse; margin: 20px 0; }\r\n        .info-table td { padding: 12px 0; border-bottom: 1px solid #eeeeee; }\r\n        .info-label { font-weight: bold; color: #555555; width: 40%; }\r\n        .info-value { text-align: right; color: #333333; }\r\n        .highlight-amount { color: #D63384; font-weight: bold; font-size: 18px; }\r\n        .lookup-box { background-color: #f8f9fa; border: 2px dashed #007BFF; border-radius: 6px; padding: 15px; text-align: center; margin: 25px 0; }\r\n        .lookup-code { display: block; font-size: 24px; letter-spacing: 2px; font-weight: bold; color: #007BFF; margin-top: 5px; }\r\n        .email-footer { background-color: #f4f4f7; padding: 20px; text-align: center; font-size: 12px; color: #888888; }\r\n        .attachment-list { list-style: none; padding: 0; margin: 0; }\r\n        .attachment-list li { margin-bottom: 8px; }\r\n        .attachment-list a { color: #007BFF; text-decoration: none; font-weight: bold; }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class='email-container'>\r\n        <div class='email-content'>\r\n            <div class='email-header'>\r\n                <h1>Hóa Đơn Điện Tử</h1>\r\n            </div>\r\n\r\n            <div class='email-body'>\r\n                <p><strong>Xin chào {{CustomerName}},</strong></p>\r\n                <p>{{Message}}</p>\r\n                \r\n                <p>Hệ thống xin thông báo hóa đơn của quý khách đã được phát hành với thông tin chi tiết như sau:</p>\r\n\r\n                <table class='info-table'>\r\n                    <tr>\r\n                        <td class='info-label'>Số hóa đơn:</td>\r\n                        <td class='info-value'>#{{InvoiceNumber}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Ký hiệu (Serial):</td>\r\n                        <td class='info-value'>{{Serial}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Ngày phát hành:</td>\r\n                        <td class='info-value'>{{IssuedDate}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Tổng thanh toán:</td>\r\n                        <td class='info-value highlight-amount'>{{TotalAmount}} VND</td>\r\n                    </tr>\r\n                </table>\r\n\r\n                <div class='lookup-box'>\r\n                    <span>Mã tra cứu hóa đơn</span>\r\n                    <span class='lookup-code'>{{LookupCode}}</span>\r\n                </div>\r\n\r\n                <p>📂 <strong>Tài liệu đính kèm:</strong></p>\r\n                <ul class='attachment-list'>\r\n                    {{AttachmentList}}\r\n                </ul>\r\n\r\n                <p style='margin-top: 30px;'>Nếu quý khách có thắc mắc, vui lòng liên hệ bộ phận hỗ trợ.</p>\r\n                <p>Trân trọng,<br><strong>Đội ngũ EIMS</strong></p>\r\n            </div>\r\n\r\n            <div class='email-footer'>\r\n                <p>&copy; 2026 EIMS KNS Solutions. All rights reserved.</p>\r\n                <p>Email này được gửi tự động, vui lòng không trả lời.</p>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>" });

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "OriginalBodyContent" },
                values: new object[] { new DateTime(2026, 1, 23, 10, 23, 16, 552, DateTimeKind.Utc).AddTicks(9919), "<div style='font-family:Arial, sans-serif; padding: 20px; border: 1px solid #ddd; max-width: 600px; margin: 0 auto;'>\r\n                <h2 style='color:#007BFF;'>Hello {{CustomerName}},</h2>\r\n                <p style='background:#f0f8ff; padding:10px; border-left:4px solid #007BFF; font-style:italic;'>{{Message}}</p>\r\n                <p>We are pleased to inform you that your e-invoice has been issued:</p>\r\n                <table style='width:100%; margin:15px 0;'>\r\n                    <tr><td><strong>Invoice No:</strong></td><td>#{{InvoiceNumber}}</td></tr>\r\n                    <tr><td><strong>Total Amount:</strong></td><td style='color:#D63384; font-weight:bold;'>{{TotalAmount}} VND</td></tr>\r\n                </table>\r\n                <p>📂 <strong>Attachments:</strong></p>\r\n                <ul>{{AttachmentList}}</ul>\r\n                <p style='color:#777; font-size:12px;'>Best Regards,<br>EIMS Team</p>\r\n            </div>" });

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "OriginalBodyContent" },
                values: new object[] { new DateTime(2026, 1, 23, 10, 23, 16, 552, DateTimeKind.Utc).AddTicks(9921), "<div style='font-family:Arial, sans-serif; border: 2px solid #dc3545; padding: 20px; max-width: 600px; margin: 0 auto;'>\r\n                <h2 style='color:#dc3545;'>⚠️ Thông báo Nhắc thanh toán</h2>\r\n                <p>Kính gửi {{CustomerName}},</p>\r\n                <div style='background:#fff3cd; color:#856404; padding:10px; margin:10px 0;'>\r\n                    <strong>Lời nhắn:</strong> {{Message}}\r\n                </div>\r\n                <p>Hóa đơn <strong>#{{InvoiceNumber}}</strong> ({{TotalAmount}} VND) hiện chưa được thanh toán.</p>\r\n                <ul>{{AttachmentList}}</ul>\r\n            </div>" });

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "OriginalBodyContent" },
                values: new object[] { new DateTime(2026, 1, 23, 10, 23, 16, 552, DateTimeKind.Utc).AddTicks(9923), "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <style>\r\n        .email-container { font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; background-color: #f4f4f7; padding: 40px 20px; }\r\n        .email-content { max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); overflow: hidden; }\r\n        .email-header { background-color: #007BFF; padding: 30px; text-align: center; color: #ffffff; }\r\n        .email-header h1 { margin: 0; font-size: 24px; font-weight: bold; }\r\n        .email-body { padding: 30px; color: #333333; line-height: 1.6; }\r\n        .info-table { width: 100%; border-collapse: collapse; margin: 20px 0; }\r\n        .info-table td { padding: 12px 0; border-bottom: 1px solid #eeeeee; }\r\n        .info-label { font-weight: bold; color: #555555; width: 40%; }\r\n        .info-value { text-align: right; color: #333333; }\r\n        .highlight-amount { color: #D63384; font-weight: bold; font-size: 18px; }\r\n        .lookup-box { background-color: #f8f9fa; border: 2px dashed #007BFF; border-radius: 6px; padding: 15px; text-align: center; margin: 25px 0; }\r\n        .lookup-code { display: block; font-size: 24px; letter-spacing: 2px; font-weight: bold; color: #007BFF; margin-top: 5px; }\r\n        .email-footer { background-color: #f4f4f7; padding: 20px; text-align: center; font-size: 12px; color: #888888; }\r\n        .attachment-list { list-style: none; padding: 0; margin: 0; }\r\n        .attachment-list li { margin-bottom: 8px; }\r\n        .attachment-list a { color: #007BFF; text-decoration: none; font-weight: bold; }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class='email-container'>\r\n        <div class='email-content'>\r\n            <div class='email-header'>\r\n                <h1>Hóa Đơn Điện Tử</h1>\r\n            </div>\r\n\r\n            <div class='email-body'>\r\n                <p><strong>Xin chào {{CustomerName}},</strong></p>\r\n                <p>{{Message}}</p>\r\n                \r\n                <p>Hệ thống xin thông báo hóa đơn của quý khách đã được phát hành với thông tin chi tiết như sau:</p>\r\n\r\n                <table class='info-table'>\r\n                    <tr>\r\n                        <td class='info-label'>Số hóa đơn:</td>\r\n                        <td class='info-value'>#{{InvoiceNumber}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Ký hiệu (Serial):</td>\r\n                        <td class='info-value'>{{Serial}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Ngày phát hành:</td>\r\n                        <td class='info-value'>{{IssuedDate}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Tổng thanh toán:</td>\r\n                        <td class='info-value highlight-amount'>{{TotalAmount}} VND</td>\r\n                    </tr>\r\n                </table>\r\n\r\n                <div class='lookup-box'>\r\n                    <span>Mã tra cứu hóa đơn</span>\r\n                    <span class='lookup-code'>{{LookupCode}}</span>\r\n                </div>\r\n\r\n                <p>📂 <strong>Tài liệu đính kèm:</strong></p>\r\n                <ul class='attachment-list'>\r\n                    {{AttachmentList}}\r\n                </ul>\r\n\r\n                <p style='margin-top: 30px;'>Nếu quý khách có thắc mắc, vui lòng liên hệ bộ phận hỗ trợ.</p>\r\n                <p>Trân trọng,<br><strong>Đội ngũ EIMS</strong></p>\r\n            </div>\r\n\r\n            <div class='email-footer'>\r\n                <p>&copy; 2026 EIMS KNS Solutions. All rights reserved.</p>\r\n                <p>Email này được gửi tự động, vui lòng không trả lời.</p>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>" });

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                columns: new[] { "CreatedAt", "OriginalBodyContent" },
                values: new object[] { new DateTime(2026, 1, 23, 10, 23, 16, 552, DateTimeKind.Utc).AddTicks(9924), "<div style='font-family:Arial, sans-serif; padding: 20px; border: 1px solid #ddd; max-width: 600px; margin: 0 auto;'>\r\n            <h2 style='color:#007BFF;'>Kính gửi {{CustomerName}},</h2>\r\n            <p>Do có sự sai sót về thông tin trên hóa đơn số <strong>#{{InvoiceNumber}}</strong> (Ngày lập: {{IssuedDate}}), chúng tôi đã lập biên bản thu hồi/thay thế hóa đơn này.</p>\r\n            \r\n            <div style='background:#fff3cd; color:#856404; padding:10px; margin:15px 0;'>\r\n                <strong>Lý do sai sót:</strong> {{Reason}}\r\n            </div>\r\n\r\n            <p>Kính đề nghị Quý khách xem xét, <strong>ký xác nhận</strong> vào biên bản đính kèm và phản hồi lại email này để chúng tôi tiến hành xuất hóa đơn thay thế mới.</p>\r\n            \r\n            <p>📂 <strong>Tài liệu đính kèm:</strong></p>\r\n            <ul>{{AttachmentList}}</ul>\r\n            \r\n            <p style='color:#777; font-size:12px;'>Trân trọng,<br><strong>Đội ngũ Kế toán EIMS</strong></p>\r\n        </div>" });

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                columns: new[] { "CreatedAt", "OriginalBodyContent" },
                values: new object[] { new DateTime(2026, 1, 23, 10, 23, 16, 552, DateTimeKind.Utc).AddTicks(9925), "<div style='font-family:Arial, sans-serif; padding: 20px; border: 1px solid #ddd; max-width: 600px; margin: 0 auto;'>\r\n            <h2 style='color:#007BFF;'>Kính gửi {{CustomerName}},</h2>\r\n            <p>Chúng tôi gửi đến Quý khách biên bản thỏa thuận điều chỉnh cho hóa đơn số <strong>#{{InvoiceNumber}}</strong>.</p>\r\n            \r\n            <div style='background:#e2e3e5; color:#383d41; padding:10px; margin:15px 0;'>\r\n                <strong>Nội dung điều chỉnh:</strong> {{Reason}}\r\n            </div>\r\n\r\n            <p>Quý khách vui lòng kiểm tra, <strong>ký số (hoặc ký tươi)</strong> vào biên bản đính kèm và gửi lại cho chúng tôi.</p>\r\n            \r\n            <ul>{{AttachmentList}}</ul>\r\n            <p style='color:#777; font-size:12px;'>Trân trọng,<br><strong>Đội ngũ Kế toán EIMS</strong></p>\r\n        </div>" });

            migrationBuilder.InsertData(
                table: "EmailTemplate",
                columns: new[] { "EmailTemplateID", "BodyContent", "Category", "CreatedAt", "IsActive", "IsSystemTemplate", "LanguageCode", "Name", "OriginalBodyContent", "Subject", "TemplateCode", "UpdatedAt" },
                values: new object[] { 9, "<!DOCTYPE html>\r\n\r\n<html>\r\n\r\n<head>\r\n\r\n    <style>\r\n\r\n        .email-container { font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; background-color: #f4f4f7; padding: 40px 20px; }\r\n\r\n        .email-content { max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); overflow: hidden; }\r\n\r\n        .email-header { background-color: #28a745; padding: 30px; text-align: center; color: #ffffff; } /* Màu xanh lá cho Statement khác với Invoice */\r\n\r\n        .email-header h1 { margin: 0; font-size: 24px; font-weight: bold; }\r\n\r\n        .email-body { padding: 30px; color: #333333; line-height: 1.6; }\r\n\r\n        .info-table { width: 100%; border-collapse: collapse; margin: 20px 0; }\r\n\r\n        .info-table td { padding: 12px 0; border-bottom: 1px solid #eeeeee; }\r\n\r\n        .info-label { font-weight: bold; color: #555555; width: 50%; }\r\n\r\n        .info-value { text-align: right; color: #333333; }\r\n\r\n        .highlight-amount { color: #D63384; font-weight: bold; font-size: 20px; }\r\n\r\n        .email-footer { background-color: #f4f4f7; padding: 20px; text-align: center; font-size: 12px; color: #888888; }\r\n\r\n        .attachment-list { list-style: none; padding: 0; margin: 0; }\r\n\r\n        .attachment-list li { margin-bottom: 8px; }\r\n\r\n        .attachment-list a { color: #007BFF; text-decoration: none; font-weight: bold; }\r\n\r\n        .note-text { font-size: 13px; font-style: italic; color: #666; margin-top: 10px; }\r\n\r\n    </style>\r\n\r\n</head>\r\n\r\n<body>\r\n\r\n    <div class='email-container'>\r\n\r\n        <div class='email-content'>\r\n\r\n            <div class='email-header'>\r\n\r\n                <h1>Thông Báo Cước Dịch Vụ</h1>\r\n\r\n            </div>\r\n\r\n\r\n\r\n            <div class='email-body'>\r\n\r\n                <p><strong>Kính gửi Quý khách hàng {{CustomerName}},</strong></p>\r\n\r\n                \r\n\r\n                <p><strong>{{CompanyName}}</strong> trân trọng thông báo tổng cước dịch vụ sử dụng trong tháng <strong>{{Month}}/{{Year}}</strong> và nợ tồn đọng (nếu có) như sau:</p>\r\n\r\n\r\n\r\n                <table class='info-table'>\r\n\r\n                    <tr>\r\n\r\n                        <td class='info-label'>Kỳ cước:</td>\r\n\r\n                        <td class='info-value'>Tháng {{Month}}/{{Year}}</td>\r\n\r\n                    </tr>\r\n\r\n                    <tr>\r\n\r\n                        <td class='info-label'>Hạn thanh toán:</td>\r\n\r\n                        <td class='info-value'><strong>{{DueDate}}</strong></td>\r\n\r\n                    </tr>\r\n\r\n                    <tr>\r\n\r\n                        <td class='info-label'>TỔNG TIỀN PHẢI THANH TOÁN:</td>\r\n\r\n                        <td class='info-value highlight-amount'>{{TotalAmount}} VNĐ</td>\r\n\r\n                    </tr>\r\n\r\n                </table>\r\n\r\n\r\n\r\n                <p>Quý khách vui lòng kiểm tra file <strong>Bảng kê chi tiết</strong> (đính kèm) và sắp xếp thanh toán đúng hạn để dịch vụ không bị gián đoạn.</p>\r\n\r\n                \r\n\r\n                <p class='note-text'>(Thông tin tài khoản ngân hàng xem chi tiết trong file đính kèm).</p>\r\n\r\n\r\n\r\n                <p>📂 <strong>Tài liệu đính kèm:</strong></p>\r\n\r\n                <ul class='attachment-list'>\r\n\r\n                     {{AttachmentList}}\r\n\r\n                </ul>\r\n\r\n\r\n\r\n                <p style='margin-top: 30px;'>Cảm ơn Quý khách đã tin tưởng và sử dụng dịch vụ.</p>\r\n\r\n                <p>Trân trọng,<br><strong>Phòng Kế toán {{CompanyName}}</strong></p>\r\n\r\n            </div>\r\n\r\n\r\n\r\n            <div class='email-footer'>\r\n\r\n                <p>&copy; {{Year}} {{CompanyName}}. All rights reserved.</p>\r\n\r\n                <p>Email này được gửi tự động, vui lòng không trả lời.</p>\r\n\r\n            </div>\r\n\r\n        </div>\r\n\r\n    </div>\r\n\r\n</body>\r\n\r\n</html>", "payment", new DateTime(2026, 1, 23, 10, 23, 16, 552, DateTimeKind.Utc).AddTicks(9928), true, false, "vi", "Mẫu gửi bảng kê cước (Tiếng Việt)", "<!DOCTYPE html>\r\n\r\n<html>\r\n\r\n<head>\r\n\r\n    <style>\r\n\r\n        .email-container { font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; background-color: #f4f4f7; padding: 40px 20px; }\r\n\r\n        .email-content { max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); overflow: hidden; }\r\n\r\n        .email-header { background-color: #28a745; padding: 30px; text-align: center; color: #ffffff; } /* Màu xanh lá cho Statement khác với Invoice */\r\n\r\n        .email-header h1 { margin: 0; font-size: 24px; font-weight: bold; }\r\n\r\n        .email-body { padding: 30px; color: #333333; line-height: 1.6; }\r\n\r\n        .info-table { width: 100%; border-collapse: collapse; margin: 20px 0; }\r\n\r\n        .info-table td { padding: 12px 0; border-bottom: 1px solid #eeeeee; }\r\n\r\n        .info-label { font-weight: bold; color: #555555; width: 50%; }\r\n\r\n        .info-value { text-align: right; color: #333333; }\r\n\r\n        .highlight-amount { color: #D63384; font-weight: bold; font-size: 20px; }\r\n\r\n        .email-footer { background-color: #f4f4f7; padding: 20px; text-align: center; font-size: 12px; color: #888888; }\r\n\r\n        .attachment-list { list-style: none; padding: 0; margin: 0; }\r\n\r\n        .attachment-list li { margin-bottom: 8px; }\r\n\r\n        .attachment-list a { color: #007BFF; text-decoration: none; font-weight: bold; }\r\n\r\n        .note-text { font-size: 13px; font-style: italic; color: #666; margin-top: 10px; }\r\n\r\n    </style>\r\n\r\n</head>\r\n\r\n<body>\r\n\r\n    <div class='email-container'>\r\n\r\n        <div class='email-content'>\r\n\r\n            <div class='email-header'>\r\n\r\n                <h1>Thông Báo Cước Dịch Vụ</h1>\r\n\r\n            </div>\r\n\r\n\r\n\r\n            <div class='email-body'>\r\n\r\n                <p><strong>Kính gửi Quý khách hàng {{CustomerName}},</strong></p>\r\n\r\n                \r\n\r\n                <p><strong>{{CompanyName}}</strong> trân trọng thông báo tổng cước dịch vụ sử dụng trong tháng <strong>{{Month}}/{{Year}}</strong> và nợ tồn đọng (nếu có) như sau:</p>\r\n\r\n\r\n\r\n                <table class='info-table'>\r\n\r\n                    <tr>\r\n\r\n                        <td class='info-label'>Kỳ cước:</td>\r\n\r\n                        <td class='info-value'>Tháng {{Month}}/{{Year}}</td>\r\n\r\n                    </tr>\r\n\r\n                    <tr>\r\n\r\n                        <td class='info-label'>Hạn thanh toán:</td>\r\n\r\n                        <td class='info-value'><strong>{{DueDate}}</strong></td>\r\n\r\n                    </tr>\r\n\r\n                    <tr>\r\n\r\n                        <td class='info-label'>TỔNG TIỀN PHẢI THANH TOÁN:</td>\r\n\r\n                        <td class='info-value highlight-amount'>{{TotalAmount}} VNĐ</td>\r\n\r\n                    </tr>\r\n\r\n                </table>\r\n\r\n\r\n\r\n                <p>Quý khách vui lòng kiểm tra file <strong>Bảng kê chi tiết</strong> (đính kèm) và sắp xếp thanh toán đúng hạn để dịch vụ không bị gián đoạn.</p>\r\n\r\n                \r\n\r\n                <p class='note-text'>(Thông tin tài khoản ngân hàng xem chi tiết trong file đính kèm).</p>\r\n\r\n\r\n\r\n                <p>📂 <strong>Tài liệu đính kèm:</strong></p>\r\n\r\n                <ul class='attachment-list'>\r\n\r\n                     {{AttachmentList}}\r\n\r\n                </ul>\r\n\r\n\r\n\r\n                <p style='margin-top: 30px;'>Cảm ơn Quý khách đã tin tưởng và sử dụng dịch vụ.</p>\r\n\r\n                <p>Trân trọng,<br><strong>Phòng Kế toán {{CompanyName}}</strong></p>\r\n\r\n            </div>\r\n\r\n\r\n\r\n            <div class='email-footer'>\r\n\r\n                <p>&copy; {{Year}} {{CompanyName}}. All rights reserved.</p>\r\n\r\n                <p>Email này được gửi tự động, vui lòng không trả lời.</p>\r\n\r\n            </div>\r\n\r\n        </div>\r\n\r\n    </div>\r\n\r\n</body>\r\n\r\n</html>", "TB Cước Dịch vụ Tháng {{Month}}/{{Year}} - {{CustomerName}}", "STATEMENT_SEND", null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 10, 23, 15, 975, DateTimeKind.Utc).AddTicks(758));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 10, 23, 15, 975, DateTimeKind.Utc).AddTicks(761));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 10, 23, 15, 975, DateTimeKind.Utc).AddTicks(763));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 10, 23, 16, 95, DateTimeKind.Utc).AddTicks(2536), "$2a$11$EGJiADTRQ54RDoJUTxZlmOJ0/ezEjvgHFYGCDL93NEyElexeDliwS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 10, 23, 16, 209, DateTimeKind.Utc).AddTicks(7330), "$2a$11$BeqqsNSAZ2WqV/SSdQ9uOeM4L715hS8PHyqqyIYeq7AmqfBh8psIe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 10, 23, 16, 325, DateTimeKind.Utc).AddTicks(5388), "$2a$11$U/P.13w7AkSbgbHWE.LoD.zrQMlzCWar88galFeogTXnppN2kDZIS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 10, 23, 16, 441, DateTimeKind.Utc).AddTicks(8793), "$2a$11$2/63lsAncRgVY/eH/zyS1u7z9jYqgiuSlbhY8dE3y8mg9cgvEIwnC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 10, 23, 16, 552, DateTimeKind.Utc).AddTicks(8334), "$2a$11$4XQ8O6rOd4nwo2ldpgGRmuU/RPBwr5TE.W8ZrCSGJkSDbq6/NvXOO" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 9);

            migrationBuilder.DropColumn(
                name: "OriginalBodyContent",
                table: "EmailTemplate");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 5, 41, 15, 100, DateTimeKind.Utc).AddTicks(4420));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 5, 41, 15, 100, DateTimeKind.Utc).AddTicks(4429));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 5, 41, 15, 100, DateTimeKind.Utc).AddTicks(4432));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 5, 41, 15, 898, DateTimeKind.Utc).AddTicks(6820));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 5, 41, 15, 898, DateTimeKind.Utc).AddTicks(6825));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 5, 41, 15, 898, DateTimeKind.Utc).AddTicks(6828));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 5, 41, 15, 898, DateTimeKind.Utc).AddTicks(6830));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 5, 41, 15, 898, DateTimeKind.Utc).AddTicks(6832));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 5, 41, 15, 898, DateTimeKind.Utc).AddTicks(6834));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 5, 41, 15, 100, DateTimeKind.Utc).AddTicks(4478));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 5, 41, 15, 100, DateTimeKind.Utc).AddTicks(4482));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 5, 41, 15, 100, DateTimeKind.Utc).AddTicks(4485));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 5, 41, 15, 257, DateTimeKind.Utc).AddTicks(8447), "$2a$11$/FNRWrBzlPxaA6y/3ay4WemPdQEuYbNDIAjmGk25vz00iHd9HQ/JK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 5, 41, 15, 410, DateTimeKind.Utc).AddTicks(5231), "$2a$11$B9TQeRHiYDoXxMxUx30xeOygF18mc7ngA.xGkivWXrJSxzt.Hf5wq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 5, 41, 15, 563, DateTimeKind.Utc).AddTicks(5523), "$2a$11$nKr2BIQBeKrv3/N2HMroeuMkArcQMtvga0HC3g1GyxXd5WMixKwGO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 5, 41, 15, 736, DateTimeKind.Utc).AddTicks(1251), "$2a$11$xiJlyIqGJ0OUC7SzcnSnn.MZtWsnDN4A91Qkq/n6CYFHQOx2KLdXW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 5, 41, 15, 895, DateTimeKind.Utc).AddTicks(4642), "$2a$11$MGOza/dnkcw6k7lVMbkBGOhXND/.14KHgCj1L0pj9N.scdsgxCuMS" });
        }
    }
}
