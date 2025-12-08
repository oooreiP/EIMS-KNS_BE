using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EmailTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailTemplate",
                columns: table => new
                {
                    EmailTemplateID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TemplateCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LanguageCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Subject = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    BodyContent = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplate", x => x.EmailTemplateID);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 12, 18, 26, 992, DateTimeKind.Utc).AddTicks(6219));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 12, 18, 26, 992, DateTimeKind.Utc).AddTicks(6227));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 12, 18, 26, 992, DateTimeKind.Utc).AddTicks(6230));

            migrationBuilder.InsertData(
                table: "EmailTemplate",
                columns: new[] { "EmailTemplateID", "BodyContent", "Description", "IsActive", "LanguageCode", "Subject", "TemplateCode" },
                values: new object[,]
                {
                    { 1, "<div style='font-family:Arial, sans-serif; padding: 20px; border: 1px solid #ddd; max-width: 600px; margin: 0 auto;'>\r\n                <h2 style='color:#007BFF;'>Xin chào {{CustomerName}},</h2>\r\n                <p style='background:#f0f8ff; padding:10px; border-left:4px solid #007BFF; font-style:italic;'>{{Message}}</p>\r\n                <p>Chúng tôi xin thông báo hóa đơn điện tử đã được phát hành:</p>\r\n                <table style='width:100%; margin:15px 0;'>\r\n                    <tr><td><strong>Số hóa đơn:</strong></td><td>#{{InvoiceNumber}}</td></tr>\r\n                    <tr><td><strong>Tổng tiền:</strong></td><td style='color:#D63384; font-weight:bold;'>{{TotalAmount}} VND</td></tr>\r\n                </table>\r\n                <p>📂 <strong>File đính kèm:</strong></p>\r\n                <ul>{{AttachmentList}}</ul>\r\n                <p style='color:#777; font-size:12px;'>Trân trọng,<br>EIMS Team</p>\r\n            </div>", "Mẫu gửi hóa đơn mặc định", true, "vi", "🔔 [Hóa đơn] #{{InvoiceNumber}} - Thông báo phát hành", "INVOICE_SEND" },
                    { 2, "<div style='font-family:Arial, sans-serif; padding: 20px; border: 1px solid #ddd; max-width: 600px; margin: 0 auto;'>\r\n                <h2 style='color:#007BFF;'>Hello {{CustomerName}},</h2>\r\n                <p style='background:#f0f8ff; padding:10px; border-left:4px solid #007BFF; font-style:italic;'>{{Message}}</p>\r\n                <p>We are pleased to inform you that your e-invoice has been issued:</p>\r\n                <table style='width:100%; margin:15px 0;'>\r\n                    <tr><td><strong>Invoice No:</strong></td><td>#{{InvoiceNumber}}</td></tr>\r\n                    <tr><td><strong>Total Amount:</strong></td><td style='color:#D63384; font-weight:bold;'>{{TotalAmount}} VND</td></tr>\r\n                </table>\r\n                <p>📂 <strong>Attachments:</strong></p>\r\n                <ul>{{AttachmentList}}</ul>\r\n                <p style='color:#777; font-size:12px;'>Best Regards,<br>EIMS Team</p>\r\n            </div>", "Standard Invoice Email (English)", true, "en", "🧾 [Invoice] #{{InvoiceNumber}} - Issued Notification", "INVOICE_SEND" },
                    { 3, "<div style='font-family:Arial, sans-serif; border: 2px solid #dc3545; padding: 20px; max-width: 600px; margin: 0 auto;'>\r\n                <h2 style='color:#dc3545;'>⚠️ Thông báo Nhắc thanh toán</h2>\r\n                <p>Kính gửi {{CustomerName}},</p>\r\n                <div style='background:#fff3cd; color:#856404; padding:10px; margin:10px 0;'>\r\n                    <strong>Lời nhắn:</strong> {{Message}}\r\n                </div>\r\n                <p>Hóa đơn <strong>#{{InvoiceNumber}}</strong> ({{TotalAmount}} VND) hiện chưa được thanh toán.</p>\r\n                <ul>{{AttachmentList}}</ul>\r\n            </div>", "Mẫu nhắc nợ khẩn cấp", true, "vi", "🔥 [NHẮC THANH TOÁN] Hóa đơn #{{InvoiceNumber}} quá hạn", "PAYMENT_REMINDER" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 12, 18, 26, 992, DateTimeKind.Utc).AddTicks(6264));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 12, 18, 26, 992, DateTimeKind.Utc).AddTicks(6268));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 12, 18, 26, 992, DateTimeKind.Utc).AddTicks(6271));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 8, 12, 18, 27, 105, DateTimeKind.Utc).AddTicks(3013), "$2a$11$WVyNPcOy0pZfNpFSdsgEGOPsfGBN11GijNltNaAYkGVxfurHbEI4O" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 8, 12, 18, 27, 217, DateTimeKind.Utc).AddTicks(2865), "$2a$11$91m8RTCz.pDxA8fh7jSjK.YnBrrMiHiH93xlu0oPE6rwjMGEokvrO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 8, 12, 18, 27, 329, DateTimeKind.Utc).AddTicks(4034), "$2a$11$4YTHM2/QI68rghuTKhqjcuWMoHjGLx7bwVrjPU1RK9JTIVKbJvPTO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 8, 12, 18, 27, 441, DateTimeKind.Utc).AddTicks(8793), "$2a$11$W1OoU4fqibbxbD2G0k6VKOFHSPenNhhlXQAnBqee36uqIOKKk9j1W" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 8, 12, 18, 27, 556, DateTimeKind.Utc).AddTicks(2511), "$2a$11$SvYLfqrF6K9GawhcH3MRaeJUHAP.Zkr5O7xg1ah1BbLDxpHV4KLIS" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailTemplate");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 8, 28, 22, 938, DateTimeKind.Utc).AddTicks(1810));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 8, 28, 22, 938, DateTimeKind.Utc).AddTicks(1815));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 8, 28, 22, 938, DateTimeKind.Utc).AddTicks(1818));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 8, 28, 22, 938, DateTimeKind.Utc).AddTicks(1852));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 8, 28, 22, 938, DateTimeKind.Utc).AddTicks(1855));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 8, 28, 22, 938, DateTimeKind.Utc).AddTicks(1858));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 7, 8, 28, 23, 50, DateTimeKind.Utc).AddTicks(986), "$2a$11$hmkakHGz3mHvXc3y/GZRUenPsmCwfW6pq3b00CbCvRWcAwUmaYrI6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 7, 8, 28, 23, 163, DateTimeKind.Utc).AddTicks(2057), "$2a$11$zzcajXCd/w.Huerg1No0weSumywxdwRm6qIRD/Av56/ZM4HORPSDi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 7, 8, 28, 23, 274, DateTimeKind.Utc).AddTicks(9866), "$2a$11$lpTgr30btg5e/fSFQMvHLenuNg.ST63IVy197BthI6dTMT6eNyis6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 7, 8, 28, 23, 386, DateTimeKind.Utc).AddTicks(4906), "$2a$11$IWWhiPHMcdf/a4ejUfRIROI1OonUDzLY3YXdPy/UCPFhNq6rIJTR6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 7, 8, 28, 23, 497, DateTimeKind.Utc).AddTicks(3691), "$2a$11$isO2p.VSKg8jNReoDUkdTu7.pj8/aXOiVfvzVm/tsCKEk8vf.EtVe" });
        }
    }
}
