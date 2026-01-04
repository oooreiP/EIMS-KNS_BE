using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateForAdjustment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReferenceNote",
                table: "Invoices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdjustmentItem",
                table: "InvoiceItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "OriginalInvoiceItemID",
                table: "InvoiceItems",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 4, 7, 12, 42, 933, DateTimeKind.Utc).AddTicks(4697));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 4, 7, 12, 42, 933, DateTimeKind.Utc).AddTicks(4748));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 4, 7, 12, 42, 933, DateTimeKind.Utc).AddTicks(4750));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 7, 12, 43, 487, DateTimeKind.Utc).AddTicks(7233));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 7, 12, 43, 487, DateTimeKind.Utc).AddTicks(7237));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 7, 12, 43, 487, DateTimeKind.Utc).AddTicks(7239));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 4, 7, 12, 42, 933, DateTimeKind.Utc).AddTicks(4782));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 4, 7, 12, 42, 933, DateTimeKind.Utc).AddTicks(4785));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 4, 7, 12, 42, 933, DateTimeKind.Utc).AddTicks(4787));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 4, 7, 12, 43, 42, DateTimeKind.Utc).AddTicks(3494), "$2a$11$9WHpZ2BTK961AiB0SxVHhe/acbvK7yY88scz66hxItj47rERls6/q" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 4, 7, 12, 43, 152, DateTimeKind.Utc).AddTicks(7779), "$2a$11$yt6XrG94dv2s.2jVWOF4hOsbtks3Z62oyKJ2tztrhMw8kVD0u.OZC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 4, 7, 12, 43, 265, DateTimeKind.Utc).AddTicks(51), "$2a$11$t4MbDJhT6uSK3Eoqry9OZeuBut8QWDKJXX51Ckm4M9lo9AteHK1m." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 4, 7, 12, 43, 376, DateTimeKind.Utc).AddTicks(6727), "$2a$11$hYriq88V0ZMYv5FberW3ueURUV2KodAQsVf.1lns/k.1OsST2h1oy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 4, 7, 12, 43, 486, DateTimeKind.Utc).AddTicks(1011), "$2a$11$X1tzW5wKS2b8JMkI9D0mLO6zjg7GiziLrHA5/W.nzVZlW6SeDWDUe" });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_OriginalInvoiceItemID",
                table: "InvoiceItems",
                column: "OriginalInvoiceItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItems_Invoices_OriginalInvoiceItemID",
                table: "InvoiceItems",
                column: "OriginalInvoiceItemID",
                principalTable: "Invoices",
                principalColumn: "InvoiceID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItems_Invoices_OriginalInvoiceItemID",
                table: "InvoiceItems");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItems_OriginalInvoiceItemID",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "ReferenceNote",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "IsAdjustmentItem",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "OriginalInvoiceItemID",
                table: "InvoiceItems");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 31, 18, 27, 0, 813, DateTimeKind.Utc).AddTicks(1212));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 31, 18, 27, 0, 813, DateTimeKind.Utc).AddTicks(1226));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 31, 18, 27, 0, 813, DateTimeKind.Utc).AddTicks(1229));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 31, 18, 27, 1, 383, DateTimeKind.Utc).AddTicks(6557));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 31, 18, 27, 1, 383, DateTimeKind.Utc).AddTicks(6562));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 31, 18, 27, 1, 383, DateTimeKind.Utc).AddTicks(6564));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 31, 18, 27, 0, 813, DateTimeKind.Utc).AddTicks(1263));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 31, 18, 27, 0, 813, DateTimeKind.Utc).AddTicks(1266));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 31, 18, 27, 0, 813, DateTimeKind.Utc).AddTicks(1269));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 31, 18, 27, 0, 930, DateTimeKind.Utc).AddTicks(4581), "$2a$11$aKGez5cU9WCNsA2ep.10h.rmVBJ.6gYLkn/Ovrquejap/L1ShuLQ." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 31, 18, 27, 1, 46, DateTimeKind.Utc).AddTicks(2879), "$2a$11$9tepChIA0gfMgjnf/Lo6cu3FRoRxtFIjrAmHfo608GknObLl9ZYie" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 31, 18, 27, 1, 159, DateTimeKind.Utc).AddTicks(303), "$2a$11$MEIYIpiX06Xh4W2x2Kj6AO/POER08Y71vhxMrmgYEcMvqifZlqCJ6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 31, 18, 27, 1, 271, DateTimeKind.Utc).AddTicks(1173), "$2a$11$iCVB4wKd0/pWKZD1eerEWOLn.ITDK0HPbGLOdcbs0RfPeg2W7.tVe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 31, 18, 27, 1, 381, DateTimeKind.Utc).AddTicks(7331), "$2a$11$m/staNvXBT8RdhGuFub5me8ONwjr0bzQovlVCjU.U9.WWgDywri4O" });
        }
    }
}
