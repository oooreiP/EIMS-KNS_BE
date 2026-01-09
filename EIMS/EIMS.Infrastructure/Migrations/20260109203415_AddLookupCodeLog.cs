using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLookupCodeLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceLookupLogs",
                columns: table => new
                {
                    LogID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LookupCode = table.Column<string>(type: "text", nullable: false),
                    IPAddress = table.Column<string>(type: "text", nullable: true),
                    UserAgent = table.Column<string>(type: "text", nullable: true),
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsSuccess = table.Column<bool>(type: "boolean", nullable: false),
                    FoundInvoiceID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLookupLogs", x => x.LogID);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 34, 11, 345, DateTimeKind.Utc).AddTicks(3078));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 34, 11, 345, DateTimeKind.Utc).AddTicks(3087));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 34, 11, 345, DateTimeKind.Utc).AddTicks(3090));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 9, 20, 34, 12, 136, DateTimeKind.Utc).AddTicks(1033));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 9, 20, 34, 12, 136, DateTimeKind.Utc).AddTicks(1037));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 9, 20, 34, 12, 136, DateTimeKind.Utc).AddTicks(1041));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 34, 11, 345, DateTimeKind.Utc).AddTicks(3139));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 34, 11, 345, DateTimeKind.Utc).AddTicks(3143));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 34, 11, 345, DateTimeKind.Utc).AddTicks(3147));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 34, 11, 501, DateTimeKind.Utc).AddTicks(857), "$2a$11$jEvttcfP.eTpj8e7mKkxe./8lxk9WCgo7g9e6PhdGJreNzdY/wUJe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 34, 11, 655, DateTimeKind.Utc).AddTicks(8489), "$2a$11$DyQebkjB7i79k7U60bHsEOP18Gc5VUL5fROyXJrn/mCXGbxnhtUXy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 34, 11, 819, DateTimeKind.Utc).AddTicks(5257), "$2a$11$1ufMYU.8sxpPuYGJGnYrUO/XRqLSKwWRymbscKQSc7PrgeAn5ru2W" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 34, 11, 973, DateTimeKind.Utc).AddTicks(4049), "$2a$11$eFQTJc2prOdyHIwiiZ6s9OzijXMvvjpvk0F8H0n1ANw5o..ZmdR8G" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 34, 12, 133, DateTimeKind.Utc).AddTicks(400), "$2a$11$UBY2V1nJrPZPlvaen4zVReX3b4v8jz98GkMqRDn3O8Ojh72C9Onc." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceLookupLogs");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 5, 5, 598, DateTimeKind.Utc).AddTicks(1270));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 5, 5, 598, DateTimeKind.Utc).AddTicks(1279));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 5, 5, 598, DateTimeKind.Utc).AddTicks(1283));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 9, 20, 5, 6, 424, DateTimeKind.Utc).AddTicks(5628));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 9, 20, 5, 6, 424, DateTimeKind.Utc).AddTicks(5634));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 9, 20, 5, 6, 424, DateTimeKind.Utc).AddTicks(5637));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 5, 5, 598, DateTimeKind.Utc).AddTicks(1341));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 5, 5, 598, DateTimeKind.Utc).AddTicks(1346));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 5, 5, 598, DateTimeKind.Utc).AddTicks(1350));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 5, 5, 798, DateTimeKind.Utc).AddTicks(4679), "$2a$11$.yncXd4W/dzCh8ak3XUpo.AnZ3DJB3FbME5rwUWnhcEUYCVDNBwsm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 5, 5, 951, DateTimeKind.Utc).AddTicks(8208), "$2a$11$F9PhLvizIis/XFfT4GwfWOwDfDr4X165KCPjF89pz7FqXjoJaSosC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 5, 6, 108, DateTimeKind.Utc).AddTicks(3629), "$2a$11$kriJZ0FmpNUXhyUgB74.IO.Ms6cKkOD8LkY5fwZ5QfTY9zBzRxT8." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 5, 6, 262, DateTimeKind.Utc).AddTicks(7892), "$2a$11$AUK8dePJHprlmbtudZDZZuwwyjE5oCOwZ8PQeRy3/Rz4sTopOZFTu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 5, 6, 420, DateTimeKind.Utc).AddTicks(7699), "$2a$11$pnDRBBvKdRwkmGUBt/k6ROSUahHJ1kCxN4udpE3ml1NbGUCpV3eCC" });
        }
    }
}
