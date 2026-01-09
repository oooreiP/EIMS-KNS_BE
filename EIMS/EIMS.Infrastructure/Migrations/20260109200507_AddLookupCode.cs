using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLookupCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LookupCode",
                table: "Invoices",
                type: "text",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LookupCode",
                table: "Invoices");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 8, 10, 29, 23, 947, DateTimeKind.Utc).AddTicks(8978));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 8, 10, 29, 23, 947, DateTimeKind.Utc).AddTicks(8985));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 8, 10, 29, 23, 947, DateTimeKind.Utc).AddTicks(8987));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 8, 10, 29, 24, 519, DateTimeKind.Utc).AddTicks(8856));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 8, 10, 29, 24, 519, DateTimeKind.Utc).AddTicks(8860));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 8, 10, 29, 24, 519, DateTimeKind.Utc).AddTicks(8862));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 8, 10, 29, 23, 947, DateTimeKind.Utc).AddTicks(9021));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 8, 10, 29, 23, 947, DateTimeKind.Utc).AddTicks(9024));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 8, 10, 29, 23, 947, DateTimeKind.Utc).AddTicks(9026));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 8, 10, 29, 24, 61, DateTimeKind.Utc).AddTicks(7616), "$2a$11$UqaSY7voYBxQh7Xn5G.yl.xIOTy.zfQyxRoU..MhsCOOgNWvdK9tS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 8, 10, 29, 24, 174, DateTimeKind.Utc).AddTicks(2179), "$2a$11$BuSUDIWleGVNr.1bS/6uPu4K4DvAX/gel4y3Hp5RbD2Lu6N15uhkm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 8, 10, 29, 24, 286, DateTimeKind.Utc).AddTicks(7637), "$2a$11$IMre0tLew.nyxIDrLSiJiexqeyM9S.GnN91EWKcvV0V68zuL0gFCy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 8, 10, 29, 24, 405, DateTimeKind.Utc).AddTicks(1151), "$2a$11$Hpl2Grke05q0UFK/9ywI9.6YEgXtHujIJ3I7gFdGjhNWLkLxiXtFa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 8, 10, 29, 24, 518, DateTimeKind.Utc).AddTicks(196), "$2a$11$r//pV6K1Gnw25C2zp.SaION9ed1tfMuRGnE2jTb5eQ93iRAm7pfZy" });
        }
    }
}
