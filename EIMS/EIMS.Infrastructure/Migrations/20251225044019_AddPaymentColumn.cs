using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PaidAmount",
                table: "Invoices",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RemainingAmount",
                table: "Invoices",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 25, 4, 40, 15, 267, DateTimeKind.Utc).AddTicks(1540));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 25, 4, 40, 15, 267, DateTimeKind.Utc).AddTicks(1567));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 25, 4, 40, 15, 267, DateTimeKind.Utc).AddTicks(1570));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 25, 4, 40, 15, 267, DateTimeKind.Utc).AddTicks(1680));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 25, 4, 40, 15, 267, DateTimeKind.Utc).AddTicks(1688));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 25, 4, 40, 15, 267, DateTimeKind.Utc).AddTicks(1693));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 25, 4, 40, 15, 394, DateTimeKind.Utc).AddTicks(2640), "$2a$11$V0U1wDJAGvIcudycoznw5OtS6/6Ukwna/XjzCTK1rA3YdzCZdIIVC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 25, 4, 40, 15, 508, DateTimeKind.Utc).AddTicks(8863), "$2a$11$1rSO3nIMoykDA8vSZJnGjekn8gpHPmhQ0y4e79.8wGBH3yAlFzcsi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 25, 4, 40, 15, 627, DateTimeKind.Utc).AddTicks(2295), "$2a$11$8UHNBzm9Fts0XSsvacqaDeKLrNq0LMA5WQVMgWOUplRFi.ZtIbBg6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 25, 4, 40, 15, 746, DateTimeKind.Utc).AddTicks(2277), "$2a$11$elllviozStyYlmbzG4XV5eDg8YBkMmv1dpvjJl9q/XLZ4og7vt4/q" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 25, 4, 40, 15, 865, DateTimeKind.Utc).AddTicks(7185), "$2a$11$hzXGQteggnNW8IZ47DZ3tuw8k//p7zFoEw3gqzv3Svg6TvXESsZqq" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaidAmount",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "RemainingAmount",
                table: "Invoices");

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
        }
    }
}
