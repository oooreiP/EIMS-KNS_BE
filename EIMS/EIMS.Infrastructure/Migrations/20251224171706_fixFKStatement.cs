using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixFKStatement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceStatements_Users_CustomerID",
                table: "InvoiceStatements");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 24, 17, 17, 1, 727, DateTimeKind.Utc).AddTicks(2563));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 24, 17, 17, 1, 727, DateTimeKind.Utc).AddTicks(2580));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 24, 17, 17, 1, 727, DateTimeKind.Utc).AddTicks(2586));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 24, 17, 17, 1, 727, DateTimeKind.Utc).AddTicks(2742));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 24, 17, 17, 1, 727, DateTimeKind.Utc).AddTicks(2751));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 24, 17, 17, 1, 727, DateTimeKind.Utc).AddTicks(2757));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 24, 17, 17, 1, 940, DateTimeKind.Utc).AddTicks(2897), "$2a$11$yniuAKxMY2hKw9ghe1ww1O0bojVIgBIEuBNWF4WwKfx47ocZMtoOa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 24, 17, 17, 2, 140, DateTimeKind.Utc).AddTicks(9806), "$2a$11$mv4/A4Ys2fmfF.7.KtvJVOsxiW42LMK/TqX.3ulfthTVFh2THl4ty" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 24, 17, 17, 2, 335, DateTimeKind.Utc).AddTicks(3693), "$2a$11$9ClsC69CTVYwduX96dhZ1e.daRxZLlT3uifM2mutES9jWf.ZMg.ce" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 24, 17, 17, 2, 540, DateTimeKind.Utc).AddTicks(1536), "$2a$11$zff6UA5LBPmv9GCbNjkQkeYUBZfkVIob8y7pfYqcjieUczKNhY.4S" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 24, 17, 17, 2, 743, DateTimeKind.Utc).AddTicks(9309), "$2a$11$LvyeiPGA9QhBVGh1YYhU3uzLPrnJhrsrqx4MzTNiEgJzWYmpYAFJ2" });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceStatements_CreatedBy",
                table: "InvoiceStatements",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceStatements_Users_CreatedBy",
                table: "InvoiceStatements",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceStatements_Users_CreatedBy",
                table: "InvoiceStatements");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceStatements_CreatedBy",
                table: "InvoiceStatements");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 19, 4, 7, 16, 700, DateTimeKind.Utc).AddTicks(5730));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 19, 4, 7, 16, 700, DateTimeKind.Utc).AddTicks(5736));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 19, 4, 7, 16, 700, DateTimeKind.Utc).AddTicks(5738));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 19, 4, 7, 16, 700, DateTimeKind.Utc).AddTicks(5766));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 19, 4, 7, 16, 700, DateTimeKind.Utc).AddTicks(5770));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 19, 4, 7, 16, 700, DateTimeKind.Utc).AddTicks(5773));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 19, 4, 7, 16, 811, DateTimeKind.Utc).AddTicks(9269), "$2a$11$qFEOqvNoRIhsDxKrd42s1O8My4togJFNswpWC8IwlvWNgE7uToyiq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 19, 4, 7, 16, 926, DateTimeKind.Utc).AddTicks(1783), "$2a$11$XkHFZVg63Mie5WbwUzp7leL4ijO/oMV5wnybOeorBPTYMd17GrAya" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 19, 4, 7, 17, 44, DateTimeKind.Utc).AddTicks(3578), "$2a$11$lXRRXYJuHQBNMl..FzOku.gxJox37TwtMbAmU/he577BcPCrxfALG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 19, 4, 7, 17, 158, DateTimeKind.Utc).AddTicks(1809), "$2a$11$2XaJ5ANGpWSmVughVjV.eOPnu/l/siIod6w.7jY8f3c0f2aFluimK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 19, 4, 7, 17, 268, DateTimeKind.Utc).AddTicks(4857), "$2a$11$woIciiROBYskwWY0ZaZWkeerMBl5I66xkNxreexsma/qIi.lq6s0C" });

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceStatements_Users_CustomerID",
                table: "InvoiceStatements",
                column: "CustomerID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
