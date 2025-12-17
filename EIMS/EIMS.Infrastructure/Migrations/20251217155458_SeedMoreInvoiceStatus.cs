using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoreInvoiceStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 15, 54, 51, 300, DateTimeKind.Utc).AddTicks(2296));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 15, 54, 51, 300, DateTimeKind.Utc).AddTicks(2307));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 15, 54, 51, 300, DateTimeKind.Utc).AddTicks(2309));

            migrationBuilder.InsertData(
                table: "InvoiceStatuses",
                columns: new[] { "InvoiceStatusID", "StatusName" },
                values: new object[,]
                {
                    { 13, "TaxAuthority Rejected" },
                    { 14, "Processing" },
                    { 15, "Send Error" },
                    { 16, "Rejected" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 15, 54, 51, 300, DateTimeKind.Utc).AddTicks(2410));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 15, 54, 51, 300, DateTimeKind.Utc).AddTicks(2414));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 15, 54, 51, 300, DateTimeKind.Utc).AddTicks(2418));

            migrationBuilder.InsertData(
                table: "TaxApiStatuses",
                columns: new[] { "TaxApiStatusID", "Code", "StatusName" },
                values: new object[,]
                {
                    { 8, "NOT_SENT", "Hoá đơn đang trong trạng thái nháp/chờ kí" },
                    { 9, "TECHNICAL_ERROR", "Lỗi kĩ thuật" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 17, 15, 54, 51, 461, DateTimeKind.Utc).AddTicks(2459), "$2a$11$YpA1mPYFXL7c9daYoAV94uHjuWnCw.09NCwyi4gI2Ph/iAQeP8gUm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 17, 15, 54, 51, 623, DateTimeKind.Utc).AddTicks(1933), "$2a$11$OlbJWOHNK361whM7ciDCZeh2tcauWx.WVY1xX46VV71snTlM3mQTO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 17, 15, 54, 51, 796, DateTimeKind.Utc).AddTicks(1549), "$2a$11$A0hC9NJdRv89P3Ya.CPp2eLBXOhDIhF7oU7/RvZ/48nPuZiwiMkTi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 17, 15, 54, 51, 971, DateTimeKind.Utc).AddTicks(3390), "$2a$11$5/EGxV76A2zIjEtE4Ba0UuRZw.crNlexSdSDGuRS2xQqHUX/zEez6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 17, 15, 54, 52, 128, DateTimeKind.Utc).AddTicks(6933), "$2a$11$onHyimXpL9R498fx3ChhO.EZC0s/OcCIRX4P7DR7BZzp7JTvxDhvC" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InvoiceStatuses",
                keyColumn: "InvoiceStatusID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "InvoiceStatuses",
                keyColumn: "InvoiceStatusID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "InvoiceStatuses",
                keyColumn: "InvoiceStatusID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "InvoiceStatuses",
                keyColumn: "InvoiceStatusID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "TaxApiStatuses",
                keyColumn: "TaxApiStatusID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TaxApiStatuses",
                keyColumn: "TaxApiStatusID",
                keyValue: 9);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 8, 5, 16, 136, DateTimeKind.Utc).AddTicks(2996));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 8, 5, 16, 136, DateTimeKind.Utc).AddTicks(3005));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 8, 5, 16, 136, DateTimeKind.Utc).AddTicks(3007));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 8, 5, 16, 136, DateTimeKind.Utc).AddTicks(3053));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 8, 5, 16, 136, DateTimeKind.Utc).AddTicks(3056));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 8, 5, 16, 136, DateTimeKind.Utc).AddTicks(3059));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 8, 5, 16, 259, DateTimeKind.Utc).AddTicks(6071), "$2a$11$P21uJbmtSja/OvzCzf2Po.Pnodzz2RiB1CYYjE9WfcuPrc38Jh4rW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 8, 5, 16, 378, DateTimeKind.Utc).AddTicks(1635), "$2a$11$jrySRgCcoUPo.s0yrsTxRurRaHnL6fTfZ.UM9ywHgxiq1.zFSTD2C" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 8, 5, 16, 496, DateTimeKind.Utc).AddTicks(6011), "$2a$11$0iL5nd5Nu177pkkw3F0tWO4GhrTRTMMly.Gl9nnHTPzBSHWE3q2FK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 8, 5, 16, 611, DateTimeKind.Utc).AddTicks(5121), "$2a$11$.1HRoe0RFLpOQzdQJnnJm.D10AeFSrKNqCQgnwL8vQn8hiheAR3ja" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 8, 5, 16, 722, DateTimeKind.Utc).AddTicks(9085), "$2a$11$OYVfFo073OKjj9HJrI8Fj.EhTqyC7F.Rybz7j/hEq9avcRMKwne5W" });
        }
    }
}
