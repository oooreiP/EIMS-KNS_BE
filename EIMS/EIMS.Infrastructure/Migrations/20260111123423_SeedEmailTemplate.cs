using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedEmailTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 12, 34, 20, 413, DateTimeKind.Utc).AddTicks(604));

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

            migrationBuilder.InsertData(
                table: "EmailTemplate",
                columns: new[] { "EmailTemplateID", "BodyContent", "Category", "CreatedAt", "IsActive", "IsSystemTemplate", "LanguageCode", "Name", "Subject", "TemplateCode", "UpdatedAt" },
                values: new object[,]
                {
                    { 4, "<div style='font-family:Arial,Helvetica,sans-serif; font-size:14px; color:#333; line-height:1.6; border: 1px solid #ddd; padding: 20px; max-width: 600px; margin: 0 auto;'>\r\n            <h2 style='color:#007BFF;'>Xin chào {{CustomerName}},</h2>\r\n\r\n            <p style='font-size: 16px;'>{{Message}}</p>\r\n\r\n            <div style='background: #f5f5f5; padding: 15px; border-radius: 5px; margin: 20px 0;'>\r\n                <table style='width:100%; border-collapse:collapse;'>\r\n                    <tr>\r\n                        <td style='padding:5px 0; font-weight:bold;'>Mã hóa đơn:</td>\r\n                        <td style='padding:5px 0;'>{{InvoiceNumber}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td style='padding:5px 0; font-weight:bold;'>Ngày tạo:</td>\r\n                        <td style='padding:5px 0;'>{{CreatedAt}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td style='padding:5px 0; font-weight:bold;'>Ngày lập:</td>\r\n                        <td style='padding:5px 0;'>{{IssuedDate}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td style='padding:5px 0; font-weight:bold;'>Tổng tiền:</td>\r\n                        <td style='padding:5px 0; color:#D63384; font-weight:bold;'>{{TotalAmount}} VND</td>\r\n                    </tr>\r\n                </table>\r\n            </div>\r\n\r\n            <p>\r\n                🧾 <strong>File đính kèm:</strong><br/>\r\n                Bạn có thể tải xuống qua các liên kết bên dưới:\r\n            </p>\r\n\r\n            <ul>{{AttachmentList}}</ul>\r\n\r\n            <p style='margin-top:20px; font-size: 13px; color: #777;'>\r\n                Trân trọng,<br/><strong>Đội ngũ E-Invoice System</strong>\r\n            </p>\r\n        </div>", "invoice", new DateTime(2026, 1, 11, 12, 34, 20, 413, DateTimeKind.Utc).AddTicks(612), true, true, "vi", "Mẫu gửi hóa đơn (Giao diện bảng chi tiết)", "🔔 [Hóa đơn] #{{InvoiceNumber}} - Thông báo phát hành", "INVOICE_SEND_2", null },
                    { 7, "<div style='font-family:Arial, sans-serif; padding: 20px; border: 1px solid #ddd; max-width: 600px; margin: 0 auto;'>\r\n            <h2 style='color:#007BFF;'>Kính gửi {{CustomerName}},</h2>\r\n            <p>Do có sự sai sót về thông tin trên hóa đơn số <strong>#{{InvoiceNumber}}</strong> (Ngày lập: {{IssuedDate}}), chúng tôi đã lập biên bản thu hồi/thay thế hóa đơn này.</p>\r\n            \r\n            <div style='background:#fff3cd; color:#856404; padding:10px; margin:15px 0;'>\r\n                <strong>Lý do sai sót:</strong> {{Reason}}\r\n            </div>\r\n\r\n            <p>Kính đề nghị Quý khách xem xét, <strong>ký xác nhận</strong> vào biên bản đính kèm và phản hồi lại email này để chúng tôi tiến hành xuất hóa đơn thay thế mới.</p>\r\n            \r\n            <p>📂 <strong>Tài liệu đính kèm:</strong></p>\r\n            <ul>{{AttachmentList}}</ul>\r\n            \r\n            <p style='color:#777; font-size:12px;'>Trân trọng,<br><strong>Đội ngũ Kế toán EIMS</strong></p>\r\n        </div>", "minutes", new DateTime(2026, 1, 11, 12, 34, 20, 413, DateTimeKind.Utc).AddTicks(613), true, true, "vi", "Mẫu biên bản thu hồi/thay thế", "📝 [XÁC NHẬN] Biên bản thu hồi hóa đơn #{{InvoiceNumber}}", "MINUTES_REPLACE", null },
                    { 8, "<div style='font-family:Arial, sans-serif; padding: 20px; border: 1px solid #ddd; max-width: 600px; margin: 0 auto;'>\r\n            <h2 style='color:#007BFF;'>Kính gửi {{CustomerName}},</h2>\r\n            <p>Chúng tôi gửi đến Quý khách biên bản thỏa thuận điều chỉnh cho hóa đơn số <strong>#{{InvoiceNumber}}</strong>.</p>\r\n            \r\n            <div style='background:#e2e3e5; color:#383d41; padding:10px; margin:15px 0;'>\r\n                <strong>Nội dung điều chỉnh:</strong> {{Reason}}\r\n            </div>\r\n\r\n            <p>Quý khách vui lòng kiểm tra, <strong>ký số (hoặc ký tươi)</strong> vào biên bản đính kèm và gửi lại cho chúng tôi.</p>\r\n            \r\n            <ul>{{AttachmentList}}</ul>\r\n            <p style='color:#777; font-size:12px;'>Trân trọng,<br><strong>Đội ngũ Kế toán EIMS</strong></p>\r\n        </div>", "minutes", new DateTime(2026, 1, 11, 12, 34, 20, 413, DateTimeKind.Utc).AddTicks(615), true, true, "vi", "Mẫu biên bản điều chỉnh", "📝 [XÁC NHẬN] Biên bản điều chỉnh hóa đơn #{{InvoiceNumber}}", "MINUTES_ADJUST", null }
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 9, 49, 32, 390, DateTimeKind.Utc).AddTicks(5929));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 9, 49, 32, 390, DateTimeKind.Utc).AddTicks(5939));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 9, 49, 32, 390, DateTimeKind.Utc).AddTicks(5993));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 10, 9, 49, 32, 987, DateTimeKind.Utc).AddTicks(5734));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 10, 9, 49, 32, 987, DateTimeKind.Utc).AddTicks(5737));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 10, 9, 49, 32, 987, DateTimeKind.Utc).AddTicks(5739));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 9, 49, 32, 390, DateTimeKind.Utc).AddTicks(6029));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 9, 49, 32, 390, DateTimeKind.Utc).AddTicks(6032));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 9, 49, 32, 390, DateTimeKind.Utc).AddTicks(6034));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 9, 49, 32, 508, DateTimeKind.Utc).AddTicks(3939), "$2a$11$vT9hLjUTY6KFe2mcFxoxp.SBARcgzN0bsWE9Xf/K5EpgdyJ/ILEbq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 9, 49, 32, 625, DateTimeKind.Utc).AddTicks(4996), "$2a$11$tkfF46mPZv218A2knvsZU.S5qYkKVvLlBYYHHv7VlWkxruIGbvfvS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 9, 49, 32, 743, DateTimeKind.Utc).AddTicks(2784), "$2a$11$irGtCypCdIPeDCJ7BTUUSuF3IVwsZFOvpM5ckmzYR/kwqRYIzsKWa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 9, 49, 32, 863, DateTimeKind.Utc).AddTicks(8847), "$2a$11$dAneCBVuK4.FWtcZtextxev9Jp2BnXP7NtnXycXpadAgszhi0rMHy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 9, 49, 32, 984, DateTimeKind.Utc).AddTicks(8268), "$2a$11$llzIZciK8MzR.rHbXrqP0uum4W0/FFER0AZ0Jlfe3BzfuYJ3kWByC" });
        }
    }
}
