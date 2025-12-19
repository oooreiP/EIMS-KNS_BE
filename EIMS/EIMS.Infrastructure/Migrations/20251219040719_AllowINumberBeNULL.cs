using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AllowINumberBeNULL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "InvoiceNumber",
                table: "Invoices",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "InvoiceNumber",
                table: "Invoices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

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
    }
}
