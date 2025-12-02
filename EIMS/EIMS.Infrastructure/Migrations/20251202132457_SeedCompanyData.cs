using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedCompanyData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 2, 13, 24, 54, 518, DateTimeKind.Utc).AddTicks(4358));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 2, 13, 24, 54, 518, DateTimeKind.Utc).AddTicks(4369));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 2, 13, 24, 54, 518, DateTimeKind.Utc).AddTicks(4373));

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyID", "AccountNumber", "Address", "BankName", "CompanyName", "ContactPhone", "TaxCode" },
                values: new object[] { 1, "1012148510", "26 Nguyễn Đình Khơi, Phường Tân Sơn Nhất, TP Hồ Chí Minh, Việt Nam", "Vietcombank - CN Tan Son Nhat", "CÔNG TY CỔ PHẦN GIẢI PHÁP TỔNG THỂ KỶ NGUYÊN SỐ", "02873000789", "0311357436" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 2, 13, 24, 54, 518, DateTimeKind.Utc).AddTicks(4454));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 2, 13, 24, 54, 518, DateTimeKind.Utc).AddTicks(4459));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 2, 13, 24, 54, 518, DateTimeKind.Utc).AddTicks(4463));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 2, 13, 24, 54, 761, DateTimeKind.Utc).AddTicks(8127), "$2a$11$LOIzkj/XXmVxICbaovtzPOFozprVUFsqaqonWPCWdy13KcxGT8tkK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 2, 13, 24, 54, 960, DateTimeKind.Utc).AddTicks(4819), "$2a$11$YO.25PrZU39s6L7D3olOrOlEJDGrskzssMELmKj7DhFDfTYfW1H1S" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 2, 13, 24, 55, 168, DateTimeKind.Utc).AddTicks(178), "$2a$11$B.l4D3pOxvrHSi6oN2jNAOjSgfGDw4LNGuV8Qy3tESO.jOwxxmx9G" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 2, 13, 24, 55, 369, DateTimeKind.Utc).AddTicks(7133), "$2a$11$m.8at/CcPShRoYNiJYiB8OkTNRvwqiwUsBcxWXOiOUUCMYs06BNzC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 2, 13, 24, 55, 569, DateTimeKind.Utc).AddTicks(2988), "$2a$11$A0.35mah848HfEAEp7wmYOYFU7BrtnlsrxnFVD1YKoioAJz8WCJHa" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyID",
                keyValue: 1);

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
    }
}
