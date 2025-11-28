using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInvoiceProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SignDate",
                table: "Invoices",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<DateTime>(
                name: "IssuedDate",
                table: "Invoices",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 9, 39, 28, 40, DateTimeKind.Utc).AddTicks(6240));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 9, 39, 28, 40, DateTimeKind.Utc).AddTicks(6252));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 9, 39, 28, 40, DateTimeKind.Utc).AddTicks(6255));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 9, 39, 28, 40, DateTimeKind.Utc).AddTicks(6292));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 9, 39, 28, 40, DateTimeKind.Utc).AddTicks(6295));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 9, 39, 28, 40, DateTimeKind.Utc).AddTicks(6299));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 9, 39, 28, 150, DateTimeKind.Utc).AddTicks(8540), "$2a$11$uduRLy5tc.pNRs1qrY6UjuHu5NvgzB0OmlPScVhRGDsPHgLE6WbGO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 9, 39, 28, 260, DateTimeKind.Utc).AddTicks(9453), "$2a$11$uqTA.vOhEMq4uq.Z33I4celjr/NfUyWffFHqfsU.Z2nEdgYLmqx5W" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 9, 39, 28, 370, DateTimeKind.Utc).AddTicks(7447), "$2a$11$9vydIpxxUOB2nz71aWoX8O2Hd.ySr3fgOOOevqkhQgFwEi7QhGUNy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 9, 39, 28, 480, DateTimeKind.Utc).AddTicks(9764), "$2a$11$f/N6KtiEhxYWDsMm./WOyeIoFiZ13fH1GB.3pj5dlOxHrbXSQm.lG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 9, 39, 28, 590, DateTimeKind.Utc).AddTicks(9241), "$2a$11$39XnPd8DlG/eR5HENe5NSu8i13ioZv9UJcd20eQHL8J3Molj/3Uci" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IssuedDate",
                table: "Invoices");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SignDate",
                table: "Invoices",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 22, 18, 15, 16, 357, DateTimeKind.Utc).AddTicks(4555));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 22, 18, 15, 16, 357, DateTimeKind.Utc).AddTicks(4562));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 22, 18, 15, 16, 357, DateTimeKind.Utc).AddTicks(4564));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 22, 18, 15, 16, 357, DateTimeKind.Utc).AddTicks(4596));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 22, 18, 15, 16, 357, DateTimeKind.Utc).AddTicks(4600));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 22, 18, 15, 16, 357, DateTimeKind.Utc).AddTicks(4602));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 22, 18, 15, 16, 469, DateTimeKind.Utc).AddTicks(497), "$2a$11$uo5aQNwVXfcQzOT47OSGJuFoEPE3dbq2vZMOhyuOaVjc/qYWPboTO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 22, 18, 15, 16, 580, DateTimeKind.Utc).AddTicks(7399), "$2a$11$rVgZaCfX1j9.G8fjG1VtLOPxsrrz7CCgkK.vWDTAIxz.7xixVGmGW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 22, 18, 15, 16, 694, DateTimeKind.Utc).AddTicks(2809), "$2a$11$uxZpXX0Y0BoFCrREPvwB4O1v7hdSVPhGaDK6OqXJtuUhPsb3qtHgC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 22, 18, 15, 16, 806, DateTimeKind.Utc).AddTicks(7035), "$2a$11$.aJZpgGSa2DDbAc3.cn5p.IhRE.zeIjh14QuEfWpKtMW55iIpRXa." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 22, 18, 15, 16, 924, DateTimeKind.Utc).AddTicks(1706), "$2a$11$bHr1kyZA9NSuEQijhqcic.9zbTUoZDgZg7qM5c1SzL3IMUwBUlFAG" });
        }
    }
}
