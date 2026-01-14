using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedETemplate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 13, 18, 31, 17, 891, DateTimeKind.Utc).AddTicks(5683));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 13, 18, 31, 17, 891, DateTimeKind.Utc).AddTicks(5707));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 13, 18, 31, 17, 891, DateTimeKind.Utc).AddTicks(5739));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                columns: new[] { "BodyContent", "CreatedAt" },
                values: new object[] { "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <style>\r\n        .email-container { font-family: ''Helvetica Neue'', Helvetica, Arial, sans-serif; background-color: #f4f4f7; padding: 40px 20px; }\r\n        .email-content { max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); overflow: hidden; }\r\n        .email-header { background-color: #007BFF; padding: 30px; text-align: center; color: #ffffff; }\r\n        .email-header h1 { margin: 0; font-size: 24px; font-weight: bold; }\r\n        .email-body { padding: 30px; color: #333333; line-height: 1.6; }\r\n        .info-table { width: 100%; border-collapse: collapse; margin: 20px 0; }\r\n        .info-table td { padding: 12px 0; border-bottom: 1px solid #eeeeee; }\r\n        .info-label { font-weight: bold; color: #555555; width: 40%; }\r\n        .info-value { text-align: right; color: #333333; }\r\n        .highlight-amount { color: #D63384; font-weight: bold; font-size: 18px; }\r\n        .lookup-box { background-color: #f8f9fa; border: 2px dashed #007BFF; border-radius: 6px; padding: 15px; text-align: center; margin: 25px 0; }\r\n        .lookup-code { display: block; font-size: 24px; letter-spacing: 2px; font-weight: bold; color: #007BFF; margin-top: 5px; }\r\n        .email-footer { background-color: #f4f4f7; padding: 20px; text-align: center; font-size: 12px; color: #888888; }\r\n        .attachment-list { list-style: none; padding: 0; margin: 0; }\r\n        .attachment-list li { margin-bottom: 8px; }\r\n        .attachment-list a { color: #007BFF; text-decoration: none; font-weight: bold; }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=''email-container''>\r\n        <div class=''email-content''>\r\n            <div class=''email-header''>\r\n                <h1>Hóa Đơn Điện Tử</h1>\r\n            </div>\r\n\r\n            <div class=''email-body''>\r\n                <p><strong>Xin chào {{CustomerName}},</strong></p>\r\n                <p>{{Message}}</p>\r\n                \r\n                <p>Hệ thống xin thông báo hóa đơn của quý khách đã được phát hành với thông tin chi tiết như sau:</p>\r\n\r\n                <table class=''info-table''>\r\n                    <tr>\r\n                        <td class=''info-label''>Số hóa đơn:</td>\r\n                        <td class=''info-value''>#{{InvoiceNumber}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class=''info-label''>Ký hiệu (Serial):</td>\r\n                        <td class=''info-value''>{{Serial}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class=''info-label''>Ngày phát hành:</td>\r\n                        <td class=''info-value''>{{IssuedDate}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class=''info-label''>Tổng thanh toán:</td>\r\n                        <td class=''info-value highlight-amount''>{{TotalAmount}} VND</td>\r\n                    </tr>\r\n                </table>\r\n\r\n                <div class=''lookup-box''>\r\n                    <span>Mã tra cứu hóa đơn</span>\r\n                    <span class=''lookup-code''>{{LookupCode}}</span>\r\n                </div>\r\n\r\n                <p>📂 <strong>Tài liệu đính kèm:</strong></p>\r\n                <ul class=''attachment-list''>\r\n                    {{AttachmentList}}\r\n                </ul>\r\n\r\n                <p style=''margin-top: 30px;''>Nếu quý khách có thắc mắc, vui lòng liên hệ bộ phận hỗ trợ.</p>\r\n                <p>Trân trọng,<br><strong>Đội ngũ EIMS</strong></p>\r\n            </div>\r\n\r\n            <div class=''email-footer''>\r\n                <p>&copy; 2026 EIMS KNS Solutions. All rights reserved.</p>\r\n                <p>Email này được gửi tự động, vui lòng không trả lời.</p>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>", new DateTime(2026, 1, 13, 18, 31, 18, 689, DateTimeKind.Utc).AddTicks(1570) });

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 31, 18, 689, DateTimeKind.Utc).AddTicks(1578));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 31, 18, 689, DateTimeKind.Utc).AddTicks(1581));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 31, 18, 689, DateTimeKind.Utc).AddTicks(1582));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 31, 18, 689, DateTimeKind.Utc).AddTicks(1584));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 31, 18, 689, DateTimeKind.Utc).AddTicks(1586));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 13, 18, 31, 17, 891, DateTimeKind.Utc).AddTicks(5788));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 13, 18, 31, 17, 891, DateTimeKind.Utc).AddTicks(5793));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 13, 18, 31, 17, 891, DateTimeKind.Utc).AddTicks(5796));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 13, 18, 31, 18, 50, DateTimeKind.Utc).AddTicks(5969), "$2a$11$f2p6hC1hMjIaGv1x9eBjWOxfbalFUu.ydRNY64aR5CCFPZuR.SLem" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 13, 18, 31, 18, 206, DateTimeKind.Utc).AddTicks(3556), "$2a$11$ifqogcB8YovHgOMzySSEq.mKRCdtHcJYeztBt3s2FdCnb9WMbyH/C" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 13, 18, 31, 18, 361, DateTimeKind.Utc).AddTicks(4951), "$2a$11$lEJ/Sf2HumEnd/TEVRWpoOYl/t.Xapftl.JaGNH2/wsQsm2liszZK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 13, 18, 31, 18, 517, DateTimeKind.Utc).AddTicks(724), "$2a$11$QX8lNt4UVvylfgRftcxCnOWMJrPMKggKzLdCnNkvmpyqBChHlHAuq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 13, 18, 31, 18, 686, DateTimeKind.Utc).AddTicks(5700), "$2a$11$DOQOIrrgM2T/RvSfpeogL.lXsFvENQdr4oR4YxQ5TtyRO1fAsAQI6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 13, 18, 15, 54, 277, DateTimeKind.Utc).AddTicks(6426));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 13, 18, 15, 54, 277, DateTimeKind.Utc).AddTicks(6435));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 13, 18, 15, 54, 277, DateTimeKind.Utc).AddTicks(6438));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                columns: new[] { "BodyContent", "CreatedAt" },
                values: new object[] { "<div style=''font-family:Arial, sans-serif; padding: 20px; border: 1px solid #ddd; max-width: 600px; margin: 0 auto;''>\r\n    <h2 style=''color:#007BFF;''>Xin chào {{CustomerName}},</h2>\r\n    <p style=''background:#f0f8ff; padding:10px; border-left:4px solid #007BFF; font-style:italic;''>{{Message}}</p>\r\n    <p>Chúng tôi xin thông báo hóa đơn điện tử đã được phát hành:</p>\r\n    <table style=''width:100%; margin:15px 0;''>\r\n        <tr><td><strong>Số hóa đơn:</strong></td><td>#{{InvoiceNumber}}</td></tr>\r\n        <tr><td><strong>Ký hiệu:</strong></td><td>{{Serial}}</td></tr>\r\n        <tr><td><strong>Ngày phát hành:</strong></td><td>{{IssuedDate}}</td></tr>\r\n        <tr><td><strong>Tổng tiền:</strong></td><td style=''color:#D63384; font-weight:bold;''>{{TotalAmount}}</td></tr>\r\n    </table>\r\n    \r\n    <div style=''background-color: #f8f9fa; padding: 10px; margin: 10px 0; border: 1px dashed #007bff;''>\r\n         <strong>Mã tra cứu: </strong> <span style=''font-family: monospace; font-size: 1.2em; color: #007bff;''>{{LookupCode}}</span>\r\n    </div>\r\n\r\n    <p>📂 <strong>File đính kèm:</strong></p>\r\n    <ul>{{AttachmentList}}</ul>\r\n    <p style=''color:#777; font-size:12px;''>Trân trọng,<br>EIMS Team</p>\r\n</div>", new DateTime(2026, 1, 13, 18, 15, 55, 79, DateTimeKind.Utc).AddTicks(4417) });

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 15, 55, 79, DateTimeKind.Utc).AddTicks(4421));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 15, 55, 79, DateTimeKind.Utc).AddTicks(4424));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 15, 55, 79, DateTimeKind.Utc).AddTicks(4426));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 15, 55, 79, DateTimeKind.Utc).AddTicks(4428));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 15, 55, 79, DateTimeKind.Utc).AddTicks(4430));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 13, 18, 15, 54, 277, DateTimeKind.Utc).AddTicks(6477));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 13, 18, 15, 54, 277, DateTimeKind.Utc).AddTicks(6482));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 13, 18, 15, 54, 277, DateTimeKind.Utc).AddTicks(6485));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 13, 18, 15, 54, 439, DateTimeKind.Utc).AddTicks(8982), "$2a$11$p3G074EaSdegIT3/9AbLqu/Xry/xSFz/XfAD6Hn3IeNmNz8yj9Yla" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 13, 18, 15, 54, 606, DateTimeKind.Utc).AddTicks(2001), "$2a$11$0cINrt5tsuEU4a9EekM96e1B9lSSGcRaSIv8dyh22pOWD5u3fZuMa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 13, 18, 15, 54, 762, DateTimeKind.Utc).AddTicks(3803), "$2a$11$mCujt0azcZfshWFWpi.I1O1yHk9RxLgwxUsieHSvn87Uyo5KRLUBS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 13, 18, 15, 54, 920, DateTimeKind.Utc).AddTicks(6742), "$2a$11$1OZQRvEAo1sLIpINyC0T0u9DVe6lbCohp5wEGaMa3jaX5CEjnl4UK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 13, 18, 15, 55, 76, DateTimeKind.Utc).AddTicks(5513), "$2a$11$TgluXWGDE8eFlww52t6zUuKcaa0RmbM1jdGZ6u0Vg6ZzlUa4wP4Ym" });
        }
    }
}
