using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addLastModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Invoices",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 18, 7, 43, 11, 548, DateTimeKind.Utc).AddTicks(8081));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 18, 7, 43, 11, 548, DateTimeKind.Utc).AddTicks(8092));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 18, 7, 43, 11, 548, DateTimeKind.Utc).AddTicks(8099));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 18, 7, 43, 12, 521, DateTimeKind.Utc).AddTicks(9964));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 18, 7, 43, 12, 521, DateTimeKind.Utc).AddTicks(9975));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 18, 7, 43, 12, 521, DateTimeKind.Utc).AddTicks(9978));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 18, 7, 43, 12, 521, DateTimeKind.Utc).AddTicks(9980));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 18, 7, 43, 12, 521, DateTimeKind.Utc).AddTicks(9983));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 18, 7, 43, 12, 521, DateTimeKind.Utc).AddTicks(9985));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 18, 7, 43, 11, 548, DateTimeKind.Utc).AddTicks(8264));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 18, 7, 43, 11, 548, DateTimeKind.Utc).AddTicks(8269));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 18, 7, 43, 11, 548, DateTimeKind.Utc).AddTicks(8273));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 18, 7, 43, 11, 748, DateTimeKind.Utc).AddTicks(2681), "$2a$11$18jfQhBa/uXIBLKZzAo8J.78or9SG0OW6hSqM/8ZyyyW0pN99y2QO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 18, 7, 43, 11, 943, DateTimeKind.Utc).AddTicks(1316), "$2a$11$sPLdK5FKt5FjmTfE84e.g.JcjhQUlFnhBO52puXv9.k9pgOOAXuIG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 18, 7, 43, 12, 136, DateTimeKind.Utc).AddTicks(6593), "$2a$11$.LFlesdB.rOfMIhzk1m8p.t0XB8tMPMr29BsWrh4DbhNiwNVNWQSG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 18, 7, 43, 12, 325, DateTimeKind.Utc).AddTicks(2793), "$2a$11$kVyUuoiqQXb30ZKxjgfSlOs/zId6hz/Lka7TfIL78tdfT9pCNOjWC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 18, 7, 43, 12, 517, DateTimeKind.Utc).AddTicks(4065), "$2a$11$vnl/iywtpqf6yUG/wJYFaOYEhNbRZVbFG7o5IKaMveOC6BwN3S0ZS" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Invoices");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 17, 3, 11, 4, 868, DateTimeKind.Utc).AddTicks(7981));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 17, 3, 11, 4, 868, DateTimeKind.Utc).AddTicks(8020));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 17, 3, 11, 4, 868, DateTimeKind.Utc).AddTicks(8027));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 3, 11, 6, 41, DateTimeKind.Utc).AddTicks(8883));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 3, 11, 6, 41, DateTimeKind.Utc).AddTicks(8890));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 3, 11, 6, 41, DateTimeKind.Utc).AddTicks(8892));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 3, 11, 6, 41, DateTimeKind.Utc).AddTicks(8895));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 3, 11, 6, 41, DateTimeKind.Utc).AddTicks(8897));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 3, 11, 6, 41, DateTimeKind.Utc).AddTicks(8899));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 17, 3, 11, 4, 868, DateTimeKind.Utc).AddTicks(8140));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 17, 3, 11, 4, 868, DateTimeKind.Utc).AddTicks(8147));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 17, 3, 11, 4, 868, DateTimeKind.Utc).AddTicks(8154));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 17, 3, 11, 5, 147, DateTimeKind.Utc).AddTicks(2377), "$2a$11$mdUITCyNSK7fkDJMBXEIsuqh70xFzGQwEj6VKng8Igt2RBOoTFfCi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 17, 3, 11, 5, 388, DateTimeKind.Utc).AddTicks(3055), "$2a$11$45FLJlvnaJlxyUruwLLOruUY/UCI12wufXhGsH0lAUXjM2xDrTXie" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 17, 3, 11, 5, 585, DateTimeKind.Utc).AddTicks(8422), "$2a$11$k96CwOEO8bQq4xlKsSHV2eMJltYxP6Rr4fZj2GHLemLSu7O94qC1C" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 17, 3, 11, 5, 821, DateTimeKind.Utc).AddTicks(2576), "$2a$11$kbGus920GpTojFBichJD7.8wvYrZAHUYw0/4k4PcBBuqbr3zP6JcG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 17, 3, 11, 6, 36, DateTimeKind.Utc).AddTicks(1095), "$2a$11$aZkjbubNCrFMQl2eaIe7hO/mZdWqqsvcgghpCYLFzLXcgpP0QLX7y" });
        }
    }
}
