using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateErrorNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxCode",
                table: "InvoiceErrorDetails");

            migrationBuilder.DropColumn(
                name: "TaxpayerName",
                table: "InvoiceErrorDetails");

            migrationBuilder.AddColumn<string>(
                name: "TaxCode",
                table: "InvoiceErrorNotifications",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TaxResponsePath",
                table: "InvoiceErrorNotifications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxpayerName",
                table: "InvoiceErrorNotifications",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 15, 9, 8, 24, 937, DateTimeKind.Utc).AddTicks(9775));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 15, 9, 8, 24, 937, DateTimeKind.Utc).AddTicks(9782));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 15, 9, 8, 24, 937, DateTimeKind.Utc).AddTicks(9786));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 15, 9, 8, 25, 512, DateTimeKind.Utc).AddTicks(2141));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 15, 9, 8, 25, 512, DateTimeKind.Utc).AddTicks(2146));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 15, 9, 8, 25, 512, DateTimeKind.Utc).AddTicks(2148));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 15, 9, 8, 25, 512, DateTimeKind.Utc).AddTicks(2149));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 15, 9, 8, 25, 512, DateTimeKind.Utc).AddTicks(2151));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 15, 9, 8, 25, 512, DateTimeKind.Utc).AddTicks(2152));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 15, 9, 8, 24, 937, DateTimeKind.Utc).AddTicks(9814));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 15, 9, 8, 24, 937, DateTimeKind.Utc).AddTicks(9817));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 15, 9, 8, 24, 937, DateTimeKind.Utc).AddTicks(9820));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 15, 9, 8, 25, 50, DateTimeKind.Utc).AddTicks(9708), "$2a$11$Lj9t.ifFXf7m8RDgF5mE/uSQI/fW/gU4RoCWVwfc.XxbARcNeruj2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 15, 9, 8, 25, 169, DateTimeKind.Utc).AddTicks(258), "$2a$11$cFyRr9AEWlqtpyFJqtYH2eyfqEtCYwKZ0e0FjSaqIjT5iWchKPZGK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 15, 9, 8, 25, 285, DateTimeKind.Utc).AddTicks(9053), "$2a$11$nn34xUh6R62rlgICo6worunL.cTbwpV65OxhaHt68vbDEXVbSKm9G" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 15, 9, 8, 25, 399, DateTimeKind.Utc).AddTicks(3769), "$2a$11$IhNHgoJkWpJC9PEQ/QR0Se5mUooAHrALOBWiWb2t0fJzlzX6YZGA." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 15, 9, 8, 25, 510, DateTimeKind.Utc).AddTicks(998), "$2a$11$rgleQXO7mdxvOlrh94Aaxu/rZW0iNUGl2hIXkJJZku73In.sH/A56" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxCode",
                table: "InvoiceErrorNotifications");

            migrationBuilder.DropColumn(
                name: "TaxResponsePath",
                table: "InvoiceErrorNotifications");

            migrationBuilder.DropColumn(
                name: "TaxpayerName",
                table: "InvoiceErrorNotifications");

            migrationBuilder.AddColumn<string>(
                name: "TaxCode",
                table: "InvoiceErrorDetails",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TaxpayerName",
                table: "InvoiceErrorDetails",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 13, 18, 39, 39, 478, DateTimeKind.Utc).AddTicks(3853));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 13, 18, 39, 39, 478, DateTimeKind.Utc).AddTicks(3871));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 13, 18, 39, 39, 478, DateTimeKind.Utc).AddTicks(3874));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 39, 40, 301, DateTimeKind.Utc).AddTicks(7926));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 39, 40, 301, DateTimeKind.Utc).AddTicks(7940));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 39, 40, 301, DateTimeKind.Utc).AddTicks(7942));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 39, 40, 301, DateTimeKind.Utc).AddTicks(7944));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 39, 40, 301, DateTimeKind.Utc).AddTicks(7946));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 39, 40, 301, DateTimeKind.Utc).AddTicks(7948));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 13, 18, 39, 39, 478, DateTimeKind.Utc).AddTicks(3921));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 13, 18, 39, 39, 478, DateTimeKind.Utc).AddTicks(3925));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 13, 18, 39, 39, 478, DateTimeKind.Utc).AddTicks(3929));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 13, 18, 39, 39, 637, DateTimeKind.Utc).AddTicks(6227), "$2a$11$eThEpkKaGGX8xXbm9rFQPOQcQz9rGcobVFZHQjd0NJUHDMft3oBj2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 13, 18, 39, 39, 808, DateTimeKind.Utc).AddTicks(8947), "$2a$11$kcVrctrwHPdtMw8vB3MAkOQwUA0HEUV4CxkN7/StHZJ5RRPt0fXJm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 13, 18, 39, 39, 965, DateTimeKind.Utc).AddTicks(517), "$2a$11$2ZzHqverdtcOxqHDrmAgtOZ7bKKjFw5s7B24LLkI4bu8CNBieBaCa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 13, 18, 39, 40, 138, DateTimeKind.Utc).AddTicks(9811), "$2a$11$zAm7oOA1Hs0PyOutVGFwAuqO0XVJ3bPR8KY/DyuKRYoMDYckq8d7q" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 13, 18, 39, 40, 298, DateTimeKind.Utc).AddTicks(769), "$2a$11$YsznbQ8fVul.FWtdf39HCe53DwjW62bp9TQIrlLBMlf2pIwgHwiF." });
        }
    }
}
