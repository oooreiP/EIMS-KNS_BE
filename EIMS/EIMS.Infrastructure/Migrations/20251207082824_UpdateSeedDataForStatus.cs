using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedDataForStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "InvoiceStatuses",
                columns: new[] { "InvoiceStatusID", "StatusName" },
                values: new object[,]
                {
                    { 10, "Adjustment_in_process" },
                    { 11, "Replacement_in_process" },
                    { 12, "TaxAuthority Approved" }
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InvoiceStatuses",
                keyColumn: "InvoiceStatusID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "InvoiceStatuses",
                keyColumn: "InvoiceStatusID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "InvoiceStatuses",
                keyColumn: "InvoiceStatusID",
                keyValue: 12);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 4, 9, 48, 230, DateTimeKind.Utc).AddTicks(2022));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 4, 9, 48, 230, DateTimeKind.Utc).AddTicks(2028));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 4, 9, 48, 230, DateTimeKind.Utc).AddTicks(2030));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 4, 9, 48, 230, DateTimeKind.Utc).AddTicks(2110));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 4, 9, 48, 230, DateTimeKind.Utc).AddTicks(2113));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 4, 9, 48, 230, DateTimeKind.Utc).AddTicks(2116));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 7, 4, 9, 48, 339, DateTimeKind.Utc).AddTicks(2121), "$2a$11$moq6PS6oJNW7s8iXDhzhlucBCKTQdsAtO5m26F/vr6Ra9LjBS/LDO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 7, 4, 9, 48, 448, DateTimeKind.Utc).AddTicks(8224), "$2a$11$G8HQSz5AKEM1PDsh0sP0UOrdTU3AYPUhjwVGHQyq4.EoeOccwgcCG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 7, 4, 9, 48, 558, DateTimeKind.Utc).AddTicks(3094), "$2a$11$M28zCvl1ZccdI91GqjlFUueQqpZZKt98neCMgNBtU9bIGgD5.48dC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 7, 4, 9, 48, 667, DateTimeKind.Utc).AddTicks(4001), "$2a$11$4z65Jg/I/i3VXBBWnILykukdhtocEX6hEXH.TeL/7yyavuksFk1IC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 7, 4, 9, 48, 777, DateTimeKind.Utc).AddTicks(1190), "$2a$11$AvbyRJADnDXsYS.Dlz1RAec8Z7gk9W0tZ6rG1VJva0cfbj2wt17tC" });
        }
    }
}
