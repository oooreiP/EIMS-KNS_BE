using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLinkTraCuu1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 34, 12, 771, DateTimeKind.Utc).AddTicks(8839));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 34, 12, 771, DateTimeKind.Utc).AddTicks(8858));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 34, 12, 771, DateTimeKind.Utc).AddTicks(8861));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 34, 13, 727, DateTimeKind.Utc).AddTicks(4610));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                columns: new[] { "BodyContent", "CreatedAt", "OriginalBodyContent" },
                values: new object[] { "<div style='font-family:Arial, sans-serif; padding: 20px; border: 1px solid #ddd; max-width: 600px; margin: 0 auto;'>\r\n                <h2 style='color:#007BFF;'>Hello {{CustomerName}},</h2>\r\n                <p style='background:#f0f8ff; padding:10px; border-left:4px solid #007BFF; font-style:italic;'>{{Message}}</p>\r\n                <p>We are pleased to inform you that your e-invoice has been issued:</p>\r\n                <table style='width:100%; margin:15px 0;'>\r\n                    <tr><td><strong>Invoice No:</strong></td><td>#{{InvoiceNumber}}</td></tr>\r\n                    <tr><td><strong>Total Amount:</strong></td><td style='color:#D63384; font-weight:bold;'>{{TotalAmount}} VND</td></tr>\r\n                </table>\r\n                <p><strong>Lookup code:</strong> {{LookupCode}}</p>\r\n                <p><a href='{{LookupUrl}}' target='_blank' style='color:#007BFF; font-weight:bold; text-decoration:none;'>View invoice</a></p>\r\n                <p>📂 <strong>Attachments:</strong></p>\r\n                <ul>{{AttachmentList}}</ul>\r\n                <p style='color:#777; font-size:12px;'>Best Regards,<br>EIMS Team</p>\r\n            </div>", new DateTime(2026, 1, 23, 19, 34, 13, 727, DateTimeKind.Utc).AddTicks(4615), "<div style='font-family:Arial, sans-serif; padding: 20px; border: 1px solid #ddd; max-width: 600px; margin: 0 auto;'>\r\n                <h2 style='color:#007BFF;'>Hello {{CustomerName}},</h2>\r\n                <p style='background:#f0f8ff; padding:10px; border-left:4px solid #007BFF; font-style:italic;'>{{Message}}</p>\r\n                <p>We are pleased to inform you that your e-invoice has been issued:</p>\r\n                <table style='width:100%; margin:15px 0;'>\r\n                    <tr><td><strong>Invoice No:</strong></td><td>#{{InvoiceNumber}}</td></tr>\r\n                    <tr><td><strong>Total Amount:</strong></td><td style='color:#D63384; font-weight:bold;'>{{TotalAmount}} VND</td></tr>\r\n                </table>\r\n                <p><strong>Lookup code:</strong> {{LookupCode}}</p>\r\n                <p><a href='{{LookupUrl}}' target='_blank' style='color:#007BFF; font-weight:bold; text-decoration:none;'>View invoice</a></p>\r\n                <p>📂 <strong>Attachments:</strong></p>\r\n                <ul>{{AttachmentList}}</ul>\r\n                <p style='color:#777; font-size:12px;'>Best Regards,<br>EIMS Team</p>\r\n            </div>" });

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 34, 13, 727, DateTimeKind.Utc).AddTicks(4622));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                columns: new[] { "BodyContent", "CreatedAt", "OriginalBodyContent" },
                values: new object[] { "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <style>\r\n        .email-container { font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; background-color: #f4f4f7; padding: 40px 20px; }\r\n        .email-content { max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); overflow: hidden; }\r\n        .email-header { background-color: #007BFF; padding: 30px; text-align: center; color: #ffffff; }\r\n        .email-header h1 { margin: 0; font-size: 24px; font-weight: bold; }\r\n        .email-body { padding: 30px; color: #333333; line-height: 1.6; }\r\n        .info-table { width: 100%; border-collapse: collapse; margin: 20px 0; }\r\n        .info-table td { padding: 12px 0; border-bottom: 1px solid #eeeeee; }\r\n        .info-label { font-weight: bold; color: #555555; width: 40%; }\r\n        .info-value { text-align: right; color: #333333; }\r\n        .highlight-amount { color: #D63384; font-weight: bold; font-size: 18px; }\r\n        .lookup-box { background-color: #f8f9fa; border: 2px dashed #007BFF; border-radius: 6px; padding: 15px; text-align: center; margin: 25px 0; }\r\n        .lookup-code { display: block; font-size: 24px; letter-spacing: 2px; font-weight: bold; color: #007BFF; margin-top: 5px; }\r\n        .email-footer { background-color: #f4f4f7; padding: 20px; text-align: center; font-size: 12px; color: #888888; }\r\n        .attachment-list { list-style: none; padding: 0; margin: 0; }\r\n        .attachment-list li { margin-bottom: 8px; }\r\n        .attachment-list a { color: #007BFF; text-decoration: none; font-weight: bold; }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class='email-container'>\r\n        <div class='email-content'>\r\n            <div class='email-header'>\r\n                <h1>Hóa Đơn Điện Tử</h1>\r\n            </div>\r\n\r\n            <div class='email-body'>\r\n                <p><strong>Xin chào {{CustomerName}},</strong></p>\r\n                <p>{{Message}}</p>\r\n                \r\n                <p>Hệ thống xin thông báo hóa đơn của quý khách đã được phát hành với thông tin chi tiết như sau:</p>\r\n\r\n                <table class='info-table'>\r\n                    <tr>\r\n                        <td class='info-label'>Số hóa đơn:</td>\r\n                        <td class='info-value'>#{{InvoiceNumber}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Ký hiệu (Serial):</td>\r\n                        <td class='info-value'>{{Serial}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Ngày phát hành:</td>\r\n                        <td class='info-value'>{{IssuedDate}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Tổng thanh toán:</td>\r\n                        <td class='info-value highlight-amount'>{{TotalAmount}} VND</td>\r\n                    </tr>\r\n                </table>\r\n\r\n                <div class='lookup-box'>\r\n                    <span>Mã tra cứu hóa đơn</span>\r\n                    <span class='lookup-code'>{{LookupCode}}</span>\r\n                    <div style='margin-top:10px;'>\r\n                        <a href='{{LookupUrl}}' target='_blank' style='color:#007BFF; font-weight:bold; text-decoration:none;'>Tra cứu hóa đơn</a>\r\n                    </div>\r\n                </div>\r\n\r\n                <p>📂 <strong>Tài liệu đính kèm:</strong></p>\r\n                <ul class='attachment-list'>\r\n                    {{AttachmentList}}\r\n                </ul>\r\n\r\n                <p style='margin-top: 30px;'>Nếu quý khách có thắc mắc, vui lòng liên hệ bộ phận hỗ trợ.</p>\r\n                <p>Trân trọng,<br><strong>Đội ngũ EIMS</strong></p>\r\n            </div>\r\n\r\n            <div class='email-footer'>\r\n                <p>&copy; 2026 EIMS KNS Solutions. All rights reserved.</p>\r\n                <p>Email này được gửi tự động, vui lòng không trả lời.</p>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>", new DateTime(2026, 1, 23, 19, 34, 13, 727, DateTimeKind.Utc).AddTicks(4624), "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <style>\r\n        .email-container { font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; background-color: #f4f4f7; padding: 40px 20px; }\r\n        .email-content { max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); overflow: hidden; }\r\n        .email-header { background-color: #007BFF; padding: 30px; text-align: center; color: #ffffff; }\r\n        .email-header h1 { margin: 0; font-size: 24px; font-weight: bold; }\r\n        .email-body { padding: 30px; color: #333333; line-height: 1.6; }\r\n        .info-table { width: 100%; border-collapse: collapse; margin: 20px 0; }\r\n        .info-table td { padding: 12px 0; border-bottom: 1px solid #eeeeee; }\r\n        .info-label { font-weight: bold; color: #555555; width: 40%; }\r\n        .info-value { text-align: right; color: #333333; }\r\n        .highlight-amount { color: #D63384; font-weight: bold; font-size: 18px; }\r\n        .lookup-box { background-color: #f8f9fa; border: 2px dashed #007BFF; border-radius: 6px; padding: 15px; text-align: center; margin: 25px 0; }\r\n        .lookup-code { display: block; font-size: 24px; letter-spacing: 2px; font-weight: bold; color: #007BFF; margin-top: 5px; }\r\n        .email-footer { background-color: #f4f4f7; padding: 20px; text-align: center; font-size: 12px; color: #888888; }\r\n        .attachment-list { list-style: none; padding: 0; margin: 0; }\r\n        .attachment-list li { margin-bottom: 8px; }\r\n        .attachment-list a { color: #007BFF; text-decoration: none; font-weight: bold; }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class='email-container'>\r\n        <div class='email-content'>\r\n            <div class='email-header'>\r\n                <h1>Hóa Đơn Điện Tử</h1>\r\n            </div>\r\n\r\n            <div class='email-body'>\r\n                <p><strong>Xin chào {{CustomerName}},</strong></p>\r\n                <p>{{Message}}</p>\r\n                \r\n                <p>Hệ thống xin thông báo hóa đơn của quý khách đã được phát hành với thông tin chi tiết như sau:</p>\r\n\r\n                <table class='info-table'>\r\n                    <tr>\r\n                        <td class='info-label'>Số hóa đơn:</td>\r\n                        <td class='info-value'>#{{InvoiceNumber}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Ký hiệu (Serial):</td>\r\n                        <td class='info-value'>{{Serial}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Ngày phát hành:</td>\r\n                        <td class='info-value'>{{IssuedDate}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Tổng thanh toán:</td>\r\n                        <td class='info-value highlight-amount'>{{TotalAmount}} VND</td>\r\n                    </tr>\r\n                </table>\r\n\r\n                <div class='lookup-box'>\r\n                    <span>Mã tra cứu hóa đơn</span>\r\n                    <span class='lookup-code'>{{LookupCode}}</span>\r\n                    <div style='margin-top:10px;'>\r\n                        <a href='{{LookupUrl}}' target='_blank' style='color:#007BFF; font-weight:bold; text-decoration:none;'>Tra cứu hóa đơn</a>\r\n                    </div>\r\n                </div>\r\n\r\n                <p>📂 <strong>Tài liệu đính kèm:</strong></p>\r\n                <ul class='attachment-list'>\r\n                    {{AttachmentList}}\r\n                </ul>\r\n\r\n                <p style='margin-top: 30px;'>Nếu quý khách có thắc mắc, vui lòng liên hệ bộ phận hỗ trợ.</p>\r\n                <p>Trân trọng,<br><strong>Đội ngũ EIMS</strong></p>\r\n            </div>\r\n\r\n            <div class='email-footer'>\r\n                <p>&copy; 2026 EIMS KNS Solutions. All rights reserved.</p>\r\n                <p>Email này được gửi tự động, vui lòng không trả lời.</p>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>" });

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 34, 13, 727, DateTimeKind.Utc).AddTicks(4627));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 34, 13, 727, DateTimeKind.Utc).AddTicks(4629));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 34, 13, 727, DateTimeKind.Utc).AddTicks(4634));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 34, 12, 771, DateTimeKind.Utc).AddTicks(8933));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 34, 12, 771, DateTimeKind.Utc).AddTicks(8938));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 34, 12, 771, DateTimeKind.Utc).AddTicks(8942));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 19, 34, 12, 960, DateTimeKind.Utc).AddTicks(9145), "$2a$11$a8K4f23hJ3PUx3bWzyesZeWFef4.OGpGXTXw.fLBis9c0rbr94mzK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 19, 34, 13, 152, DateTimeKind.Utc).AddTicks(1955), "$2a$11$AyP.l.61ekHIlVbh8v2B9O.lpW5QkA.xvSu5F7/6C6oq77/ro6csS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 19, 34, 13, 334, DateTimeKind.Utc).AddTicks(8916), "$2a$11$7wdeyVciszMICp9kOYO5K.YyVOl/BOOhlyrn6DC877VJtuVjsQ7Xi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 19, 34, 13, 539, DateTimeKind.Utc).AddTicks(558), "$2a$11$0ftWlkPx/xpgslCwDtr6ruckmG.JUwJKzWUcLmk03zQlfaz0K8pWO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 19, 34, 13, 727, DateTimeKind.Utc).AddTicks(2415), "$2a$11$3uvurgZWsz/w63aVUXIhYejqeXtWj7pbzDVPMkz1ZLjErbVSG9LBa" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 18, 48, 40, 709, DateTimeKind.Utc).AddTicks(8320));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 18, 48, 40, 709, DateTimeKind.Utc).AddTicks(8330));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 18, 48, 40, 709, DateTimeKind.Utc).AddTicks(8333));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 18, 48, 41, 477, DateTimeKind.Utc).AddTicks(5101));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                columns: new[] { "BodyContent", "CreatedAt", "OriginalBodyContent" },
                values: new object[] { "<div style='font-family:Arial, sans-serif; padding: 20px; border: 1px solid #ddd; max-width: 600px; margin: 0 auto;'>\r\n                <h2 style='color:#007BFF;'>Hello {{CustomerName}},</h2>\r\n                <p style='background:#f0f8ff; padding:10px; border-left:4px solid #007BFF; font-style:italic;'>{{Message}}</p>\r\n                <p>We are pleased to inform you that your e-invoice has been issued:</p>\r\n                <table style='width:100%; margin:15px 0;'>\r\n                    <tr><td><strong>Invoice No:</strong></td><td>#{{InvoiceNumber}}</td></tr>\r\n                    <tr><td><strong>Total Amount:</strong></td><td style='color:#D63384; font-weight:bold;'>{{TotalAmount}} VND</td></tr>\r\n                </table>\r\n                <p>📂 <strong>Attachments:</strong></p>\r\n                <ul>{{AttachmentList}}</ul>\r\n                <p style='color:#777; font-size:12px;'>Best Regards,<br>EIMS Team</p>\r\n            </div>", new DateTime(2026, 1, 23, 18, 48, 41, 477, DateTimeKind.Utc).AddTicks(5107), "<div style='font-family:Arial, sans-serif; padding: 20px; border: 1px solid #ddd; max-width: 600px; margin: 0 auto;'>\r\n                <h2 style='color:#007BFF;'>Hello {{CustomerName}},</h2>\r\n                <p style='background:#f0f8ff; padding:10px; border-left:4px solid #007BFF; font-style:italic;'>{{Message}}</p>\r\n                <p>We are pleased to inform you that your e-invoice has been issued:</p>\r\n                <table style='width:100%; margin:15px 0;'>\r\n                    <tr><td><strong>Invoice No:</strong></td><td>#{{InvoiceNumber}}</td></tr>\r\n                    <tr><td><strong>Total Amount:</strong></td><td style='color:#D63384; font-weight:bold;'>{{TotalAmount}} VND</td></tr>\r\n                </table>\r\n                <p>📂 <strong>Attachments:</strong></p>\r\n                <ul>{{AttachmentList}}</ul>\r\n                <p style='color:#777; font-size:12px;'>Best Regards,<br>EIMS Team</p>\r\n            </div>" });

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 18, 48, 41, 477, DateTimeKind.Utc).AddTicks(5109));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                columns: new[] { "BodyContent", "CreatedAt", "OriginalBodyContent" },
                values: new object[] { "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <style>\r\n        .email-container { font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; background-color: #f4f4f7; padding: 40px 20px; }\r\n        .email-content { max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); overflow: hidden; }\r\n        .email-header { background-color: #007BFF; padding: 30px; text-align: center; color: #ffffff; }\r\n        .email-header h1 { margin: 0; font-size: 24px; font-weight: bold; }\r\n        .email-body { padding: 30px; color: #333333; line-height: 1.6; }\r\n        .info-table { width: 100%; border-collapse: collapse; margin: 20px 0; }\r\n        .info-table td { padding: 12px 0; border-bottom: 1px solid #eeeeee; }\r\n        .info-label { font-weight: bold; color: #555555; width: 40%; }\r\n        .info-value { text-align: right; color: #333333; }\r\n        .highlight-amount { color: #D63384; font-weight: bold; font-size: 18px; }\r\n        .lookup-box { background-color: #f8f9fa; border: 2px dashed #007BFF; border-radius: 6px; padding: 15px; text-align: center; margin: 25px 0; }\r\n        .lookup-code { display: block; font-size: 24px; letter-spacing: 2px; font-weight: bold; color: #007BFF; margin-top: 5px; }\r\n        .email-footer { background-color: #f4f4f7; padding: 20px; text-align: center; font-size: 12px; color: #888888; }\r\n        .attachment-list { list-style: none; padding: 0; margin: 0; }\r\n        .attachment-list li { margin-bottom: 8px; }\r\n        .attachment-list a { color: #007BFF; text-decoration: none; font-weight: bold; }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class='email-container'>\r\n        <div class='email-content'>\r\n            <div class='email-header'>\r\n                <h1>Hóa Đơn Điện Tử</h1>\r\n            </div>\r\n\r\n            <div class='email-body'>\r\n                <p><strong>Xin chào {{CustomerName}},</strong></p>\r\n                <p>{{Message}}</p>\r\n                \r\n                <p>Hệ thống xin thông báo hóa đơn của quý khách đã được phát hành với thông tin chi tiết như sau:</p>\r\n\r\n                <table class='info-table'>\r\n                    <tr>\r\n                        <td class='info-label'>Số hóa đơn:</td>\r\n                        <td class='info-value'>#{{InvoiceNumber}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Ký hiệu (Serial):</td>\r\n                        <td class='info-value'>{{Serial}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Ngày phát hành:</td>\r\n                        <td class='info-value'>{{IssuedDate}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Tổng thanh toán:</td>\r\n                        <td class='info-value highlight-amount'>{{TotalAmount}} VND</td>\r\n                    </tr>\r\n                </table>\r\n\r\n                <div class='lookup-box'>\r\n                    <span>Mã tra cứu hóa đơn</span>\r\n                    <span class='lookup-code'>{{LookupCode}}</span>\r\n                </div>\r\n\r\n                <p>📂 <strong>Tài liệu đính kèm:</strong></p>\r\n                <ul class='attachment-list'>\r\n                    {{AttachmentList}}\r\n                </ul>\r\n\r\n                <p style='margin-top: 30px;'>Nếu quý khách có thắc mắc, vui lòng liên hệ bộ phận hỗ trợ.</p>\r\n                <p>Trân trọng,<br><strong>Đội ngũ EIMS</strong></p>\r\n            </div>\r\n\r\n            <div class='email-footer'>\r\n                <p>&copy; 2026 EIMS KNS Solutions. All rights reserved.</p>\r\n                <p>Email này được gửi tự động, vui lòng không trả lời.</p>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>", new DateTime(2026, 1, 23, 18, 48, 41, 477, DateTimeKind.Utc).AddTicks(5111), "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <style>\r\n        .email-container { font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; background-color: #f4f4f7; padding: 40px 20px; }\r\n        .email-content { max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); overflow: hidden; }\r\n        .email-header { background-color: #007BFF; padding: 30px; text-align: center; color: #ffffff; }\r\n        .email-header h1 { margin: 0; font-size: 24px; font-weight: bold; }\r\n        .email-body { padding: 30px; color: #333333; line-height: 1.6; }\r\n        .info-table { width: 100%; border-collapse: collapse; margin: 20px 0; }\r\n        .info-table td { padding: 12px 0; border-bottom: 1px solid #eeeeee; }\r\n        .info-label { font-weight: bold; color: #555555; width: 40%; }\r\n        .info-value { text-align: right; color: #333333; }\r\n        .highlight-amount { color: #D63384; font-weight: bold; font-size: 18px; }\r\n        .lookup-box { background-color: #f8f9fa; border: 2px dashed #007BFF; border-radius: 6px; padding: 15px; text-align: center; margin: 25px 0; }\r\n        .lookup-code { display: block; font-size: 24px; letter-spacing: 2px; font-weight: bold; color: #007BFF; margin-top: 5px; }\r\n        .email-footer { background-color: #f4f4f7; padding: 20px; text-align: center; font-size: 12px; color: #888888; }\r\n        .attachment-list { list-style: none; padding: 0; margin: 0; }\r\n        .attachment-list li { margin-bottom: 8px; }\r\n        .attachment-list a { color: #007BFF; text-decoration: none; font-weight: bold; }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class='email-container'>\r\n        <div class='email-content'>\r\n            <div class='email-header'>\r\n                <h1>Hóa Đơn Điện Tử</h1>\r\n            </div>\r\n\r\n            <div class='email-body'>\r\n                <p><strong>Xin chào {{CustomerName}},</strong></p>\r\n                <p>{{Message}}</p>\r\n                \r\n                <p>Hệ thống xin thông báo hóa đơn của quý khách đã được phát hành với thông tin chi tiết như sau:</p>\r\n\r\n                <table class='info-table'>\r\n                    <tr>\r\n                        <td class='info-label'>Số hóa đơn:</td>\r\n                        <td class='info-value'>#{{InvoiceNumber}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Ký hiệu (Serial):</td>\r\n                        <td class='info-value'>{{Serial}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Ngày phát hành:</td>\r\n                        <td class='info-value'>{{IssuedDate}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Tổng thanh toán:</td>\r\n                        <td class='info-value highlight-amount'>{{TotalAmount}} VND</td>\r\n                    </tr>\r\n                </table>\r\n\r\n                <div class='lookup-box'>\r\n                    <span>Mã tra cứu hóa đơn</span>\r\n                    <span class='lookup-code'>{{LookupCode}}</span>\r\n                </div>\r\n\r\n                <p>📂 <strong>Tài liệu đính kèm:</strong></p>\r\n                <ul class='attachment-list'>\r\n                    {{AttachmentList}}\r\n                </ul>\r\n\r\n                <p style='margin-top: 30px;'>Nếu quý khách có thắc mắc, vui lòng liên hệ bộ phận hỗ trợ.</p>\r\n                <p>Trân trọng,<br><strong>Đội ngũ EIMS</strong></p>\r\n            </div>\r\n\r\n            <div class='email-footer'>\r\n                <p>&copy; 2026 EIMS KNS Solutions. All rights reserved.</p>\r\n                <p>Email này được gửi tự động, vui lòng không trả lời.</p>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>" });

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 18, 48, 41, 477, DateTimeKind.Utc).AddTicks(5113));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 18, 48, 41, 477, DateTimeKind.Utc).AddTicks(5115));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 18, 48, 41, 477, DateTimeKind.Utc).AddTicks(5118));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 18, 48, 40, 709, DateTimeKind.Utc).AddTicks(8382));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 18, 48, 40, 709, DateTimeKind.Utc).AddTicks(8387));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 18, 48, 40, 709, DateTimeKind.Utc).AddTicks(8390));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 18, 48, 40, 863, DateTimeKind.Utc).AddTicks(6567), "$2a$11$mj1gleiOEY8hL7LB170zh.I8WvolcLH0k6Xuk8nrMhZU4Ty9EzI1O" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 18, 48, 41, 17, DateTimeKind.Utc).AddTicks(3809), "$2a$11$fmvUkGgeFiVjyCTTr2vBdelS273PTL/PZe9SaGWWGZWykG1TEQSMu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 18, 48, 41, 170, DateTimeKind.Utc).AddTicks(6324), "$2a$11$nq.nwqPzs4LVFDfNATXumuWc24mIOJfcu8DBOwxfCD26DIm3bjl1S" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 18, 48, 41, 323, DateTimeKind.Utc).AddTicks(6681), "$2a$11$5YKbdwOvOF1xOjB03MQKd.3rMb9k9lhFxfslvMXzC.MY7Hggg3u2a" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 18, 48, 41, 477, DateTimeKind.Utc).AddTicks(3311), "$2a$11$oDuB7sqpBa1ddTlscS.Uj.WpcArN5CndrHM1fscS32NceL51ukNbW" });
        }
    }
}
