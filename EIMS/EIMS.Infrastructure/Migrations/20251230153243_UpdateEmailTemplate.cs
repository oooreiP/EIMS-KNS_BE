using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmailTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "EmailTemplate");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "EmailTemplate",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "EmailTemplate",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsSystemTemplate",
                table: "EmailTemplate",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "EmailTemplate",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "EmailTemplate",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 30, 15, 32, 40, 586, DateTimeKind.Utc).AddTicks(8761));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 30, 15, 32, 40, 586, DateTimeKind.Utc).AddTicks(8767));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 30, 15, 32, 40, 586, DateTimeKind.Utc).AddTicks(8770));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                columns: new[] { "Category", "CreatedAt", "IsSystemTemplate", "Name", "UpdatedAt" },
                values: new object[] { "invoice", new DateTime(2025, 12, 30, 15, 32, 41, 151, DateTimeKind.Utc).AddTicks(7328), true, "Mẫu gửi hóa đơn mặc định", null });

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                columns: new[] { "Category", "CreatedAt", "IsSystemTemplate", "Name", "UpdatedAt" },
                values: new object[] { "invoice", new DateTime(2025, 12, 30, 15, 32, 41, 151, DateTimeKind.Utc).AddTicks(7334), true, "Standard Invoice Email (English)", null });

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                columns: new[] { "Category", "CreatedAt", "IsSystemTemplate", "Name", "UpdatedAt" },
                values: new object[] { "payment", new DateTime(2025, 12, 30, 15, 32, 41, 151, DateTimeKind.Utc).AddTicks(7336), true, "Mẫu nhắc nợ khẩn cấp", null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 30, 15, 32, 40, 586, DateTimeKind.Utc).AddTicks(8809));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 30, 15, 32, 40, 586, DateTimeKind.Utc).AddTicks(8813));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 30, 15, 32, 40, 586, DateTimeKind.Utc).AddTicks(8815));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 30, 15, 32, 40, 695, DateTimeKind.Utc).AddTicks(8772), "$2a$11$KzWjX/aepvJBsWPYFxFKsOnbs0CusDfUeMjNy16RDDHavZPqkNea6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 30, 15, 32, 40, 807, DateTimeKind.Utc).AddTicks(3891), "$2a$11$j9MowsOOJDMaUvY4xGK8suVrh5WsYnWyc9uCuFCkY1LcjxGh9kqee" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 30, 15, 32, 40, 920, DateTimeKind.Utc).AddTicks(2684), "$2a$11$1zhotJhWpdIDhNz/HbJuMupknJ7jkMqExwAG/FcCKnORFRAwkihzi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 30, 15, 32, 41, 36, DateTimeKind.Utc).AddTicks(8166), "$2a$11$XX957/O2sKdeT6Wz111oBesL16NYywJgGYgSe8yclVleEgp6CXkzy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 30, 15, 32, 41, 149, DateTimeKind.Utc).AddTicks(2644), "$2a$11$pGfRjacQU/HJiV81fjAHZOytaNYZEeiBw0DfAix/X.lUtKUAWAM4u" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "EmailTemplate");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "EmailTemplate");

            migrationBuilder.DropColumn(
                name: "IsSystemTemplate",
                table: "EmailTemplate");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "EmailTemplate");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "EmailTemplate");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "EmailTemplate",
                type: "text",
                nullable: true);

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
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "Description",
                value: "Mẫu gửi hóa đơn mặc định");

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "Description",
                value: "Standard Invoice Email (English)");

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "Description",
                value: "Mẫu nhắc nợ khẩn cấp");

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
    }
}
