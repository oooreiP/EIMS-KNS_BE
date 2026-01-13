using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedETemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                columns: new[] { "BodyContent", "CreatedAt" },
                values: new object[] { "<div style=''font-family:Arial, sans-serif; padding: 20px; border: 1px solid #ddd; max-width: 600px; margin: 0 auto;''>\r\n    <h2 style=''color:#007BFF;''>Xin chào {{CustomerName}},</h2>\r\n    <p style=''background:#f0f8ff; padding:10px; border-left:4px solid #007BFF; font-style:italic;''>{{Message}}</p>\r\n    <p>Chúng tôi xin thông báo hóa đơn điện tử đã được phát hành:</p>\r\n    <table style=''width:100%; margin:15px 0;''>\r\n        <tr><td><strong>Số hóa đơn:</strong></td><td>#{{InvoiceNumber}}</td></tr>\r\n        <tr><td><strong>Ký hiệu:</strong></td><td>{{Serial}}</td></tr>\r\n        <tr><td><strong>Ngày phát hành:</strong></td><td>{{IssuedDate}}</td></tr>\r\n        <tr><td><strong>Tổng tiền:</strong></td><td style=''color:#D63384; font-weight:bold;''>{{TotalAmount}}</td></tr>\r\n    </table>\r\n    \r\n    <div style=''background-color: #f8f9fa; padding: 10px; margin: 10px 0; border: 1px dashed #007bff;''>\r\n         <strong>Mã tra cứu: </strong> <span style=''font-family: monospace; font-size: 1.2em; color: #007bff;''>{{LookupCode}}</span>\r\n    </div>\r\n\r\n    <p>📂 <strong>File đính kèm:</strong></p>\r\n    <ul>{{AttachmentList}}</ul>\r\n    <p style=''color:#777; font-size:12px;''>Trân trọng,<br>EIMS Team</p>\r\n</div>", new DateTime(2026, 1, 13, 18, 15, 55, 79, DateTimeKind.Utc).AddTicks(4426) });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 12, 34, 19, 828, DateTimeKind.Utc).AddTicks(2944));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 12, 34, 19, 828, DateTimeKind.Utc).AddTicks(2954));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 12, 34, 19, 828, DateTimeKind.Utc).AddTicks(2957));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                columns: new[] { "BodyContent", "CreatedAt" },
                values: new object[] { "<div style='font-family:Arial, sans-serif; padding: 20px; border: 1px solid #ddd; max-width: 600px; margin: 0 auto;'>\r\n                <h2 style='color:#007BFF;'>Xin chào {{CustomerName}},</h2>\r\n                <p style='background:#f0f8ff; padding:10px; border-left:4px solid #007BFF; font-style:italic;'>{{Message}}</p>\r\n                <p>Chúng tôi xin thông báo hóa đơn điện tử đã được phát hành:</p>\r\n                <table style='width:100%; margin:15px 0;'>\r\n                    <tr><td><strong>Số hóa đơn:</strong></td><td>#{{InvoiceNumber}}</td></tr>\r\n                    <tr><td><strong>Tổng tiền:</strong></td><td style='color:#D63384; font-weight:bold;'>{{TotalAmount}} VND</td></tr>\r\n                </table>\r\n                <p>📂 <strong>File đính kèm:</strong></p>\r\n                <ul>{{AttachmentList}}</ul>\r\n                <p style='color:#777; font-size:12px;'>Trân trọng,<br>EIMS Team</p>\r\n            </div>", new DateTime(2026, 1, 11, 12, 34, 20, 413, DateTimeKind.Utc).AddTicks(604) });

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 12, 34, 20, 413, DateTimeKind.Utc).AddTicks(609));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 12, 34, 20, 413, DateTimeKind.Utc).AddTicks(611));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                columns: new[] { "BodyContent", "CreatedAt" },
                values: new object[] { "<div style='font-family:Arial,Helvetica,sans-serif; font-size:14px; color:#333; line-height:1.6; border: 1px solid #ddd; padding: 20px; max-width: 600px; margin: 0 auto;'>\r\n            <h2 style='color:#007BFF;'>Xin chào {{CustomerName}},</h2>\r\n\r\n            <p style='font-size: 16px;'>{{Message}}</p>\r\n\r\n            <div style='background: #f5f5f5; padding: 15px; border-radius: 5px; margin: 20px 0;'>\r\n                <table style='width:100%; border-collapse:collapse;'>\r\n                    <tr>\r\n                        <td style='padding:5px 0; font-weight:bold;'>Mã hóa đơn:</td>\r\n                        <td style='padding:5px 0;'>{{InvoiceNumber}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td style='padding:5px 0; font-weight:bold;'>Ngày tạo:</td>\r\n                        <td style='padding:5px 0;'>{{CreatedAt}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td style='padding:5px 0; font-weight:bold;'>Ngày lập:</td>\r\n                        <td style='padding:5px 0;'>{{IssuedDate}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td style='padding:5px 0; font-weight:bold;'>Tổng tiền:</td>\r\n                        <td style='padding:5px 0; color:#D63384; font-weight:bold;'>{{TotalAmount}} VND</td>\r\n                    </tr>\r\n                </table>\r\n            </div>\r\n\r\n            <p>\r\n                🧾 <strong>File đính kèm:</strong><br/>\r\n                Bạn có thể tải xuống qua các liên kết bên dưới:\r\n            </p>\r\n\r\n            <ul>{{AttachmentList}}</ul>\r\n\r\n            <p style='margin-top:20px; font-size: 13px; color: #777;'>\r\n                Trân trọng,<br/><strong>Đội ngũ E-Invoice System</strong>\r\n            </p>\r\n        </div>", new DateTime(2026, 1, 11, 12, 34, 20, 413, DateTimeKind.Utc).AddTicks(612) });

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 12, 34, 20, 413, DateTimeKind.Utc).AddTicks(613));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 12, 34, 20, 413, DateTimeKind.Utc).AddTicks(615));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 12, 34, 19, 828, DateTimeKind.Utc).AddTicks(2984));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 12, 34, 19, 828, DateTimeKind.Utc).AddTicks(2987));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 12, 34, 19, 828, DateTimeKind.Utc).AddTicks(2989));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 11, 12, 34, 19, 940, DateTimeKind.Utc).AddTicks(9169), "$2a$11$Lg5jdAdM4zIwGqGJrNT5FOnm/17STspS8sdWPPLEm6jLPgZ08jfsK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 11, 12, 34, 20, 59, DateTimeKind.Utc).AddTicks(5123), "$2a$11$VzKJod/S23BlWiczEa27DOma04E.hQWY7WGSfnYkuuCh8W5Bsvcvi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 11, 12, 34, 20, 179, DateTimeKind.Utc).AddTicks(9925), "$2a$11$bS26lCzjhwx7KFxDNXZL1e3y5jHx8.E50GRZLNzTDN0XQfj0J1SjS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 11, 12, 34, 20, 298, DateTimeKind.Utc).AddTicks(2436), "$2a$11$rIXW7fXkFSzecMyoRpd.U.ovIhoKrCS5BzUcEmtXMjzGF.Pr3Kr6." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 11, 12, 34, 20, 411, DateTimeKind.Utc).AddTicks(266), "$2a$11$lRc3bBS2vHNj6SJb6JzUdO2rx/yCjL1UgexiYf.6Yeiw0chV0.B3e" });
        }
    }
}
