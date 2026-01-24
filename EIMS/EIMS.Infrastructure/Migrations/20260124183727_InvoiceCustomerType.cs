using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceCustomerType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvoiceCustomerType",
                table: "Invoices",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 18, 37, 23, 993, DateTimeKind.Utc).AddTicks(8755));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 18, 37, 23, 993, DateTimeKind.Utc).AddTicks(8764));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 18, 37, 23, 993, DateTimeKind.Utc).AddTicks(8767));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 18, 37, 24, 561, DateTimeKind.Utc).AddTicks(1897));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 18, 37, 24, 561, DateTimeKind.Utc).AddTicks(1901));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 18, 37, 24, 561, DateTimeKind.Utc).AddTicks(1903));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 18, 37, 24, 561, DateTimeKind.Utc).AddTicks(1905));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 18, 37, 24, 561, DateTimeKind.Utc).AddTicks(1907));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 18, 37, 24, 561, DateTimeKind.Utc).AddTicks(1908));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 18, 37, 24, 561, DateTimeKind.Utc).AddTicks(1912));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 18, 37, 23, 993, DateTimeKind.Utc).AddTicks(8806));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 18, 37, 23, 993, DateTimeKind.Utc).AddTicks(8808));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 18, 37, 23, 993, DateTimeKind.Utc).AddTicks(8811));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 24, 18, 37, 24, 106, DateTimeKind.Utc).AddTicks(8369), "$2a$11$mEy72ZZSdvlpgYr1vkxu9eKp9O5ke8fSrWpRvl6SO9rOp/2Zv1raa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 24, 18, 37, 24, 219, DateTimeKind.Utc).AddTicks(3570), "$2a$11$wF5XYvBR0jk4xokwNspTmu0ky0XWvfj3D8S4cZ59Em9kTOREGzHpa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 24, 18, 37, 24, 336, DateTimeKind.Utc).AddTicks(4494), "$2a$11$BeIFucvrB55Fc8CFcvF4weUs78JRNLWRe8xD6NnY/Lt8qql6shvvG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 24, 18, 37, 24, 449, DateTimeKind.Utc).AddTicks(4165), "$2a$11$WJtq3G5cj26Zlw2hs88l/evhMrKCZQn.bgQ2PSsza2Yy86UlZpgLe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 24, 18, 37, 24, 561, DateTimeKind.Utc).AddTicks(466), "$2a$11$MtI6GiIXPMiT7lOjjitDjOnCQgE1s9jFlJchdQTAoMu8fMxBBOjv6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceCustomerType",
                table: "Invoices");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 13, 9, 50, 537, DateTimeKind.Utc).AddTicks(1163));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 13, 9, 50, 537, DateTimeKind.Utc).AddTicks(1171));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 13, 9, 50, 537, DateTimeKind.Utc).AddTicks(1173));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 13, 9, 51, 121, DateTimeKind.Utc).AddTicks(1779));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 13, 9, 51, 121, DateTimeKind.Utc).AddTicks(1783));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 13, 9, 51, 121, DateTimeKind.Utc).AddTicks(1785));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 13, 9, 51, 121, DateTimeKind.Utc).AddTicks(1787));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 13, 9, 51, 121, DateTimeKind.Utc).AddTicks(1788));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 13, 9, 51, 121, DateTimeKind.Utc).AddTicks(1790));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 13, 9, 51, 121, DateTimeKind.Utc).AddTicks(1792));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 13, 9, 50, 537, DateTimeKind.Utc).AddTicks(1211));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 13, 9, 50, 537, DateTimeKind.Utc).AddTicks(1247));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 13, 9, 50, 537, DateTimeKind.Utc).AddTicks(1250));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 24, 13, 9, 50, 653, DateTimeKind.Utc).AddTicks(7942), "$2a$11$khVzUf078IQU7xOnrpGWn.xao47RFwY/.NZIPRTslcSbQvkt3KKra" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 24, 13, 9, 50, 767, DateTimeKind.Utc).AddTicks(9723), "$2a$11$FgJ6SNtba6DTdBnhxSpMH.fGuFtnlXLSULZmXVXNaRZVYpNDArQSC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 24, 13, 9, 50, 888, DateTimeKind.Utc).AddTicks(5954), "$2a$11$gOAL4HZd2wh0lKxdPVPQpupYu/QmIks7eRLI3JRkBeJNgKjYY/v3y" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 24, 13, 9, 51, 6, DateTimeKind.Utc).AddTicks(2900), "$2a$11$7oiwaDQkZqxP5gZJdFxY4.wjTWXzZT.FBQtZZf3MiqvCKPFLgFllC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 24, 13, 9, 51, 121, DateTimeKind.Utc).AddTicks(523), "$2a$11$tD9Rpp8pnE9wzDusuY0Q1eGF88ZI9J0aSV0HSFQLiGAW7uCLxCq3W" });
        }
    }
}
