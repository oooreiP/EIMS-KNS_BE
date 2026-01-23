using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLinkTraCuu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                columns: new[] { "BodyContent", "CreatedAt", "OriginalBodyContent" },
                values: new object[] { "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <style>\r\n        .email-container { font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; background-color: #f4f4f7; padding: 40px 20px; }\r\n        .email-content { max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); overflow: hidden; }\r\n        .email-header { background-color: #007BFF; padding: 30px; text-align: center; color: #ffffff; }\r\n        .email-header h1 { margin: 0; font-size: 24px; font-weight: bold; }\r\n        .email-body { padding: 30px; color: #333333; line-height: 1.6; }\r\n        .info-table { width: 100%; border-collapse: collapse; margin: 20px 0; }\r\n        .info-table td { padding: 12px 0; border-bottom: 1px solid #eeeeee; }\r\n        .info-label { font-weight: bold; color: #555555; width: 40%; }\r\n        .info-value { text-align: right; color: #333333; }\r\n        .highlight-amount { color: #D63384; font-weight: bold; font-size: 18px; }\r\n        .lookup-box { background-color: #f8f9fa; border: 2px dashed #007BFF; border-radius: 6px; padding: 15px; text-align: center; margin: 25px 0; }\r\n        .lookup-code { display: block; font-size: 24px; letter-spacing: 2px; font-weight: bold; color: #007BFF; margin-top: 5px; }\r\n        .email-footer { background-color: #f4f4f7; padding: 20px; text-align: center; font-size: 12px; color: #888888; }\r\n        .attachment-list { list-style: none; padding: 0; margin: 0; }\r\n        .attachment-list li { margin-bottom: 8px; }\r\n        .attachment-list a { color: #007BFF; text-decoration: none; font-weight: bold; }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class='email-container'>\r\n        <div class='email-content'>\r\n            <div class='email-header'>\r\n                <h1>Hóa Đơn Điện Tử</h1>\r\n            </div>\r\n\r\n            <div class='email-body'>\r\n                <p><strong>Xin chào {{CustomerName}},</strong></p>\r\n                <p>{{Message}}</p>\r\n                \r\n                <p>Hệ thống xin thông báo hóa đơn của quý khách đã được phát hành với thông tin chi tiết như sau:</p>\r\n\r\n                <table class='info-table'>\r\n                    <tr>\r\n                        <td class='info-label'>Số hóa đơn:</td>\r\n                        <td class='info-value'>#{{InvoiceNumber}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Ký hiệu (Serial):</td>\r\n                        <td class='info-value'>{{Serial}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Ngày phát hành:</td>\r\n                        <td class='info-value'>{{IssuedDate}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Tổng thanh toán:</td>\r\n                        <td class='info-value highlight-amount'>{{TotalAmount}} VND</td>\r\n                    </tr>\r\n                </table>\r\n\r\n                <div class='lookup-box'>\r\n                    <span>Mã tra cứu hóa đơn</span>\r\n                    <span class='lookup-code'>{{LookupCode}}</span>\r\n                    <div style='margin-top:10px;'>\r\n                        <a href='{{LookupUrl}}' target='_blank' style='color:#007BFF; font-weight:bold; text-decoration:none;'>Tra cứu hóa đơn</a>\r\n                    </div>\r\n                </div>\r\n\r\n                <p>📂 <strong>Tài liệu đính kèm:</strong></p>\r\n                <ul class='attachment-list'>\r\n                    {{AttachmentList}}\r\n                </ul>\r\n\r\n                <p style='margin-top: 30px;'>Nếu quý khách có thắc mắc, vui lòng liên hệ bộ phận hỗ trợ.</p>\r\n                <p>Trân trọng,<br><strong>Đội ngũ EIMS</strong></p>\r\n            </div>\r\n\r\n            <div class='email-footer'>\r\n                <p>&copy; 2026 EIMS KNS Solutions. All rights reserved.</p>\r\n                <p>Email này được gửi tự động, vui lòng không trả lời.</p>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>", new DateTime(2026, 1, 23, 18, 48, 41, 477, DateTimeKind.Utc).AddTicks(5101), "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <style>\r\n        .email-container { font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; background-color: #f4f4f7; padding: 40px 20px; }\r\n        .email-content { max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); overflow: hidden; }\r\n        .email-header { background-color: #007BFF; padding: 30px; text-align: center; color: #ffffff; }\r\n        .email-header h1 { margin: 0; font-size: 24px; font-weight: bold; }\r\n        .email-body { padding: 30px; color: #333333; line-height: 1.6; }\r\n        .info-table { width: 100%; border-collapse: collapse; margin: 20px 0; }\r\n        .info-table td { padding: 12px 0; border-bottom: 1px solid #eeeeee; }\r\n        .info-label { font-weight: bold; color: #555555; width: 40%; }\r\n        .info-value { text-align: right; color: #333333; }\r\n        .highlight-amount { color: #D63384; font-weight: bold; font-size: 18px; }\r\n        .lookup-box { background-color: #f8f9fa; border: 2px dashed #007BFF; border-radius: 6px; padding: 15px; text-align: center; margin: 25px 0; }\r\n        .lookup-code { display: block; font-size: 24px; letter-spacing: 2px; font-weight: bold; color: #007BFF; margin-top: 5px; }\r\n        .email-footer { background-color: #f4f4f7; padding: 20px; text-align: center; font-size: 12px; color: #888888; }\r\n        .attachment-list { list-style: none; padding: 0; margin: 0; }\r\n        .attachment-list li { margin-bottom: 8px; }\r\n        .attachment-list a { color: #007BFF; text-decoration: none; font-weight: bold; }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class='email-container'>\r\n        <div class='email-content'>\r\n            <div class='email-header'>\r\n                <h1>Hóa Đơn Điện Tử</h1>\r\n            </div>\r\n\r\n            <div class='email-body'>\r\n                <p><strong>Xin chào {{CustomerName}},</strong></p>\r\n                <p>{{Message}}</p>\r\n                \r\n                <p>Hệ thống xin thông báo hóa đơn của quý khách đã được phát hành với thông tin chi tiết như sau:</p>\r\n\r\n                <table class='info-table'>\r\n                    <tr>\r\n                        <td class='info-label'>Số hóa đơn:</td>\r\n                        <td class='info-value'>#{{InvoiceNumber}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Ký hiệu (Serial):</td>\r\n                        <td class='info-value'>{{Serial}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Ngày phát hành:</td>\r\n                        <td class='info-value'>{{IssuedDate}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Tổng thanh toán:</td>\r\n                        <td class='info-value highlight-amount'>{{TotalAmount}} VND</td>\r\n                    </tr>\r\n                </table>\r\n\r\n                <div class='lookup-box'>\r\n                    <span>Mã tra cứu hóa đơn</span>\r\n                    <span class='lookup-code'>{{LookupCode}}</span>\r\n                    <div style='margin-top:10px;'>\r\n                        <a href='{{LookupUrl}}' target='_blank' style='color:#007BFF; font-weight:bold; text-decoration:none;'>Tra cứu hóa đơn</a>\r\n                    </div>\r\n                </div>\r\n\r\n                <p>📂 <strong>Tài liệu đính kèm:</strong></p>\r\n                <ul class='attachment-list'>\r\n                    {{AttachmentList}}\r\n                </ul>\r\n\r\n                <p style='margin-top: 30px;'>Nếu quý khách có thắc mắc, vui lòng liên hệ bộ phận hỗ trợ.</p>\r\n                <p>Trân trọng,<br><strong>Đội ngũ EIMS</strong></p>\r\n            </div>\r\n\r\n            <div class='email-footer'>\r\n                <p>&copy; 2026 EIMS KNS Solutions. All rights reserved.</p>\r\n                <p>Email này được gửi tự động, vui lòng không trả lời.</p>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>" });

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 18, 48, 41, 477, DateTimeKind.Utc).AddTicks(5107));

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
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 18, 48, 41, 477, DateTimeKind.Utc).AddTicks(5111));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 15, 57, 43, 608, DateTimeKind.Utc).AddTicks(577));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 15, 57, 43, 608, DateTimeKind.Utc).AddTicks(584));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 15, 57, 43, 608, DateTimeKind.Utc).AddTicks(588));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                columns: new[] { "BodyContent", "CreatedAt", "OriginalBodyContent" },
                values: new object[] { "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <style>\r\n        .email-container { font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; background-color: #f4f4f7; padding: 40px 20px; }\r\n        .email-content { max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); overflow: hidden; }\r\n        .email-header { background-color: #007BFF; padding: 30px; text-align: center; color: #ffffff; }\r\n        .email-header h1 { margin: 0; font-size: 24px; font-weight: bold; }\r\n        .email-body { padding: 30px; color: #333333; line-height: 1.6; }\r\n        .info-table { width: 100%; border-collapse: collapse; margin: 20px 0; }\r\n        .info-table td { padding: 12px 0; border-bottom: 1px solid #eeeeee; }\r\n        .info-label { font-weight: bold; color: #555555; width: 40%; }\r\n        .info-value { text-align: right; color: #333333; }\r\n        .highlight-amount { color: #D63384; font-weight: bold; font-size: 18px; }\r\n        .lookup-box { background-color: #f8f9fa; border: 2px dashed #007BFF; border-radius: 6px; padding: 15px; text-align: center; margin: 25px 0; }\r\n        .lookup-code { display: block; font-size: 24px; letter-spacing: 2px; font-weight: bold; color: #007BFF; margin-top: 5px; }\r\n        .email-footer { background-color: #f4f4f7; padding: 20px; text-align: center; font-size: 12px; color: #888888; }\r\n        .attachment-list { list-style: none; padding: 0; margin: 0; }\r\n        .attachment-list li { margin-bottom: 8px; }\r\n        .attachment-list a { color: #007BFF; text-decoration: none; font-weight: bold; }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class='email-container'>\r\n        <div class='email-content'>\r\n            <div class='email-header'>\r\n                <h1>Hóa Đơn Điện Tử</h1>\r\n            </div>\r\n\r\n            <div class='email-body'>\r\n                <p><strong>Xin chào {{CustomerName}},</strong></p>\r\n                <p>{{Message}}</p>\r\n                \r\n                <p>Hệ thống xin thông báo hóa đơn của quý khách đã được phát hành với thông tin chi tiết như sau:</p>\r\n\r\n                <table class='info-table'>\r\n                    <tr>\r\n                        <td class='info-label'>Số hóa đơn:</td>\r\n                        <td class='info-value'>#{{InvoiceNumber}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Ký hiệu (Serial):</td>\r\n                        <td class='info-value'>{{Serial}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Ngày phát hành:</td>\r\n                        <td class='info-value'>{{IssuedDate}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Tổng thanh toán:</td>\r\n                        <td class='info-value highlight-amount'>{{TotalAmount}} VND</td>\r\n                    </tr>\r\n                </table>\r\n\r\n                <div class='lookup-box'>\r\n                    <span>Mã tra cứu hóa đơn</span>\r\n                    <span class='lookup-code'>{{LookupCode}}</span>\r\n                </div>\r\n\r\n                <p>📂 <strong>Tài liệu đính kèm:</strong></p>\r\n                <ul class='attachment-list'>\r\n                    {{AttachmentList}}\r\n                </ul>\r\n\r\n                <p style='margin-top: 30px;'>Nếu quý khách có thắc mắc, vui lòng liên hệ bộ phận hỗ trợ.</p>\r\n                <p>Trân trọng,<br><strong>Đội ngũ EIMS</strong></p>\r\n            </div>\r\n\r\n            <div class='email-footer'>\r\n                <p>&copy; 2026 EIMS KNS Solutions. All rights reserved.</p>\r\n                <p>Email này được gửi tự động, vui lòng không trả lời.</p>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>", new DateTime(2026, 1, 23, 15, 57, 44, 613, DateTimeKind.Utc).AddTicks(8863), "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <style>\r\n        .email-container { font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; background-color: #f4f4f7; padding: 40px 20px; }\r\n        .email-content { max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); overflow: hidden; }\r\n        .email-header { background-color: #007BFF; padding: 30px; text-align: center; color: #ffffff; }\r\n        .email-header h1 { margin: 0; font-size: 24px; font-weight: bold; }\r\n        .email-body { padding: 30px; color: #333333; line-height: 1.6; }\r\n        .info-table { width: 100%; border-collapse: collapse; margin: 20px 0; }\r\n        .info-table td { padding: 12px 0; border-bottom: 1px solid #eeeeee; }\r\n        .info-label { font-weight: bold; color: #555555; width: 40%; }\r\n        .info-value { text-align: right; color: #333333; }\r\n        .highlight-amount { color: #D63384; font-weight: bold; font-size: 18px; }\r\n        .lookup-box { background-color: #f8f9fa; border: 2px dashed #007BFF; border-radius: 6px; padding: 15px; text-align: center; margin: 25px 0; }\r\n        .lookup-code { display: block; font-size: 24px; letter-spacing: 2px; font-weight: bold; color: #007BFF; margin-top: 5px; }\r\n        .email-footer { background-color: #f4f4f7; padding: 20px; text-align: center; font-size: 12px; color: #888888; }\r\n        .attachment-list { list-style: none; padding: 0; margin: 0; }\r\n        .attachment-list li { margin-bottom: 8px; }\r\n        .attachment-list a { color: #007BFF; text-decoration: none; font-weight: bold; }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class='email-container'>\r\n        <div class='email-content'>\r\n            <div class='email-header'>\r\n                <h1>Hóa Đơn Điện Tử</h1>\r\n            </div>\r\n\r\n            <div class='email-body'>\r\n                <p><strong>Xin chào {{CustomerName}},</strong></p>\r\n                <p>{{Message}}</p>\r\n                \r\n                <p>Hệ thống xin thông báo hóa đơn của quý khách đã được phát hành với thông tin chi tiết như sau:</p>\r\n\r\n                <table class='info-table'>\r\n                    <tr>\r\n                        <td class='info-label'>Số hóa đơn:</td>\r\n                        <td class='info-value'>#{{InvoiceNumber}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Ký hiệu (Serial):</td>\r\n                        <td class='info-value'>{{Serial}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Ngày phát hành:</td>\r\n                        <td class='info-value'>{{IssuedDate}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class='info-label'>Tổng thanh toán:</td>\r\n                        <td class='info-value highlight-amount'>{{TotalAmount}} VND</td>\r\n                    </tr>\r\n                </table>\r\n\r\n                <div class='lookup-box'>\r\n                    <span>Mã tra cứu hóa đơn</span>\r\n                    <span class='lookup-code'>{{LookupCode}}</span>\r\n                </div>\r\n\r\n                <p>📂 <strong>Tài liệu đính kèm:</strong></p>\r\n                <ul class='attachment-list'>\r\n                    {{AttachmentList}}\r\n                </ul>\r\n\r\n                <p style='margin-top: 30px;'>Nếu quý khách có thắc mắc, vui lòng liên hệ bộ phận hỗ trợ.</p>\r\n                <p>Trân trọng,<br><strong>Đội ngũ EIMS</strong></p>\r\n            </div>\r\n\r\n            <div class='email-footer'>\r\n                <p>&copy; 2026 EIMS KNS Solutions. All rights reserved.</p>\r\n                <p>Email này được gửi tự động, vui lòng không trả lời.</p>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>" });

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 15, 57, 44, 613, DateTimeKind.Utc).AddTicks(8874));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 15, 57, 44, 613, DateTimeKind.Utc).AddTicks(8877));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 15, 57, 44, 613, DateTimeKind.Utc).AddTicks(8880));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 15, 57, 44, 613, DateTimeKind.Utc).AddTicks(8882));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 15, 57, 44, 613, DateTimeKind.Utc).AddTicks(8885));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 15, 57, 44, 613, DateTimeKind.Utc).AddTicks(8889));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 15, 57, 43, 608, DateTimeKind.Utc).AddTicks(664));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 15, 57, 43, 608, DateTimeKind.Utc).AddTicks(671));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 15, 57, 43, 608, DateTimeKind.Utc).AddTicks(675));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 15, 57, 43, 813, DateTimeKind.Utc).AddTicks(2982), "$2a$11$gvUcxBA0aM.0eJMTcrhfSePOQYg3BKxSWzUpMvQAimkAl6qRpamR." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 15, 57, 44, 14, DateTimeKind.Utc).AddTicks(7376), "$2a$11$fJCQcTviBMtEW6eLh2mwXOdT6NCVdWm/V5hlt0gctJqEjydNpAgZi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 15, 57, 44, 214, DateTimeKind.Utc).AddTicks(9264), "$2a$11$SaqkVK4SVV4WFe4Ts8QzKuiwr5.n.ZFrytMhnwO4WBn4YdtEBh8zC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 15, 57, 44, 413, DateTimeKind.Utc).AddTicks(6720), "$2a$11$Gay24O4kWCLz4bWM68Zl3OzmmnEB2ZPpcB/ZQIJt6YM6owPixpIPG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 15, 57, 44, 613, DateTimeKind.Utc).AddTicks(6276), "$2a$11$Ptw6LGIi3XipOa72dZ0Rw.myr3hwfQbjq3Gj4lGPkyoZYNu2nqXNq" });
        }
    }
}
