using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SnapshotFieldsInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvoiceCustomerAddress",
                table: "Invoices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InvoiceCustomerName",
                table: "Invoices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InvoiceCustomerTaxCode",
                table: "Invoices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 17, 42, 9, 756, DateTimeKind.Utc).AddTicks(9792));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 17, 42, 9, 756, DateTimeKind.Utc).AddTicks(9841));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 17, 42, 9, 756, DateTimeKind.Utc).AddTicks(9850));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 17, 42, 9, 757, DateTimeKind.Utc).AddTicks(13));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 17, 42, 9, 757, DateTimeKind.Utc).AddTicks(19));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 17, 42, 9, 757, DateTimeKind.Utc).AddTicks(22));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 29, 17, 42, 10, 6, DateTimeKind.Utc).AddTicks(5773), "$2a$11$qdeoUe.tgFasJjPuoy26Qe9bUNQ7CHyj1MDYAyFmAp4qt4V2qY.5y" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 29, 17, 42, 10, 157, DateTimeKind.Utc).AddTicks(8124), "$2a$11$x3gyTcHL1kNvHzIz8RQ2I.B4rjRO8C1hKJU5fk0eTXrEia/JExuIS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 29, 17, 42, 10, 308, DateTimeKind.Utc).AddTicks(55), "$2a$11$ERHwOXU13avD7WUUrTmCwup5/1EUUgWkCs9Wb7yZwb191wvsISXZa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 29, 17, 42, 10, 449, DateTimeKind.Utc).AddTicks(9268), "$2a$11$RU34FC3QPAcZ35zYnCwZJuKyYVVFt1pLUTCK9vOFYWi9cS4xxULzW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 29, 17, 42, 10, 588, DateTimeKind.Utc).AddTicks(3876), "$2a$11$lT8z8bHaUhuru/q5LO1oyu2zKrJLV6uRqlONtTccijW5x/L0FXHCa" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceCustomerAddress",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "InvoiceCustomerName",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "InvoiceCustomerTaxCode",
                table: "Invoices");

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
    }
}
