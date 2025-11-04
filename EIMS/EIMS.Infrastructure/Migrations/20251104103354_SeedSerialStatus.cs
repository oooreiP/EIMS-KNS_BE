using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedSerialStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 10, 33, 52, 535, DateTimeKind.Utc).AddTicks(5971));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 10, 33, 52, 535, DateTimeKind.Utc).AddTicks(5980));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 10, 33, 52, 535, DateTimeKind.Utc).AddTicks(5983));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 10, 33, 52, 535, DateTimeKind.Utc).AddTicks(6034));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 10, 33, 52, 535, DateTimeKind.Utc).AddTicks(6038));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 10, 33, 52, 535, DateTimeKind.Utc).AddTicks(6042));

            migrationBuilder.InsertData(
                table: "SerialStatuses",
                columns: new[] { "SerialStatusID", "StatusName", "Symbol" },
                values: new object[,]
                {
                    { 1, "Hóa đơn có mã của cơ quan thuế", "C" },
                    { 2, "Hóa đơn không có mã của cơ quan thuế", "K" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 10, 33, 52, 692, DateTimeKind.Utc).AddTicks(3507), "$2a$11$Uq3.Eqf0/IY82F5SXicwsuTdTk9qAq5m0x0IGTFXNVSTeaOXmHAXu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 10, 33, 52, 853, DateTimeKind.Utc).AddTicks(2083), "$2a$11$NsKsCViWgyyMGDxkBh8c5.76DZffkxA7EtsUmP517dDWdL2JnTA5u" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 10, 33, 53, 9, DateTimeKind.Utc).AddTicks(238), "$2a$11$de53I21Px.56GsZ16EUll.DKaTbu.C2pVSEod7ADiclSGkIWLXV7G" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 10, 33, 53, 164, DateTimeKind.Utc).AddTicks(6781), "$2a$11$64VI4BtrOzGstEWuLoeynuJXhlpN8Le33VsxJrUdTWB0YCSpuZKuW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 10, 33, 53, 321, DateTimeKind.Utc).AddTicks(184), "$2a$11$eIDNj/6/IdVu70l1XU26Hub6kFRW2yIhaYo2uxvbZVwhh4d3XpKty" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SerialStatuses",
                keyColumn: "SerialStatusID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SerialStatuses",
                keyColumn: "SerialStatusID",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 8, 13, 8, 268, DateTimeKind.Utc).AddTicks(5052));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 8, 13, 8, 268, DateTimeKind.Utc).AddTicks(5063));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 8, 13, 8, 268, DateTimeKind.Utc).AddTicks(5068));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 8, 13, 8, 268, DateTimeKind.Utc).AddTicks(5147));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 8, 13, 8, 268, DateTimeKind.Utc).AddTicks(5152));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 8, 13, 8, 268, DateTimeKind.Utc).AddTicks(5156));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 8, 13, 8, 467, DateTimeKind.Utc).AddTicks(1480), "$2a$11$gJ3UTJszrr4kpQGrUAw9auovtz2lPbdk5qdwEgpx6LgTY0R9TlQ9." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 8, 13, 8, 624, DateTimeKind.Utc).AddTicks(2865), "$2a$11$LgLCkTLBcNjJhPGvzncPtu3krOTqrO28wXelp5emJ5/gk/Ip3LkSm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 8, 13, 8, 810, DateTimeKind.Utc).AddTicks(6476), "$2a$11$qm0Sy66sXt26dA2or2hkJuftwBbPQrj8qyUHbp8yvWJIRvIj3s.Hu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 8, 13, 8, 973, DateTimeKind.Utc).AddTicks(4821), "$2a$11$616vfFHiQQsVISer44Jh7OMuQ5vZ2FLLWpO/cbDlFp1nk1qS7pyB." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 8, 13, 9, 149, DateTimeKind.Utc).AddTicks(6220), "$2a$11$gBw9FkyRNuNda6961tW3PuBnmdGmHM5nHHuRYOeSFLfVVtGklFfUe" });
        }
    }
}
