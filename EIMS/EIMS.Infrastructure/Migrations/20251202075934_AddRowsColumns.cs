using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRowsColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinRows",
                table: "Invoices",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 2, 7, 59, 29, 825, DateTimeKind.Utc).AddTicks(1771));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 2, 7, 59, 29, 825, DateTimeKind.Utc).AddTicks(1782));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 2, 7, 59, 29, 825, DateTimeKind.Utc).AddTicks(1786));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 2, 7, 59, 29, 825, DateTimeKind.Utc).AddTicks(1859));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 2, 7, 59, 29, 825, DateTimeKind.Utc).AddTicks(1863));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 2, 7, 59, 29, 825, DateTimeKind.Utc).AddTicks(1867));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 2, 7, 59, 30, 26, DateTimeKind.Utc).AddTicks(7676), "$2a$11$ZUPaY7MTEIAQMlzFIvppUOpqWANCnxG6BvBD1r5aEZ0VXfE2YPtji" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 2, 7, 59, 30, 225, DateTimeKind.Utc).AddTicks(7186), "$2a$11$LXlfi0h4UnacTrbSRvOTGuGmClnJ9UngVSuLUNoRAnFXAdWzhUWL." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 2, 7, 59, 30, 427, DateTimeKind.Utc).AddTicks(636), "$2a$11$79mjFoEmU1qpZko/qMXC9OYndy.6NvyI.xEp3xRS0ylzF5R55iJXO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 2, 7, 59, 30, 628, DateTimeKind.Utc).AddTicks(8728), "$2a$11$j458UMrU9yU1d6L3Ke1LpOJ.aRyBim.dezQ8DaI6ipcFid4TyHKwK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 2, 7, 59, 30, 827, DateTimeKind.Utc).AddTicks(8744), "$2a$11$JTq/xM4dtMIc0gxcKDYwe.sKSd7zyfxISjoFLwUvWU2pyhxvGAjBS" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinRows",
                table: "Invoices");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 6, 10, 6, 285, DateTimeKind.Utc).AddTicks(2998));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 6, 10, 6, 285, DateTimeKind.Utc).AddTicks(3009));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 6, 10, 6, 285, DateTimeKind.Utc).AddTicks(3015));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 6, 10, 6, 285, DateTimeKind.Utc).AddTicks(3065));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 6, 10, 6, 285, DateTimeKind.Utc).AddTicks(3071));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 6, 10, 6, 285, DateTimeKind.Utc).AddTicks(3075));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 29, 6, 10, 6, 520, DateTimeKind.Utc).AddTicks(1429), "$2a$11$DNhZbDW25T9Oiuq6hitR2OPgIYJn.jSknkBskJIzdqSpRW6LJYoa." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 29, 6, 10, 6, 711, DateTimeKind.Utc).AddTicks(2017), "$2a$11$/lzImO4eT7e9Z8Mdi5V3FOvSoW62fHpRnCDVNeCVEpTeztd/./TFG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 29, 6, 10, 6, 910, DateTimeKind.Utc).AddTicks(3069), "$2a$11$YGDA1WeF3yOBBjfyCGXTxOMCmLX3zOkEh.stkcoCQpDc8TDVHCZs6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 29, 6, 10, 7, 150, DateTimeKind.Utc).AddTicks(9801), "$2a$11$81xBzyv1xw5sRPkjkSdE.OFUZ01oK6OH2a1v3Ns8UxJQ1YxoMvuQy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 29, 6, 10, 7, 406, DateTimeKind.Utc).AddTicks(1893), "$2a$11$8GtVrPLsDI/LD.4wzPl4r.K/6TZYqBdmMw93W1qbCb92SmpH9i66O" });
        }
    }
}
