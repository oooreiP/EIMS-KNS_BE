using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addFKToCreatedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 18, 12, 7, 58, 800, DateTimeKind.Utc).AddTicks(5760));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 18, 12, 7, 58, 800, DateTimeKind.Utc).AddTicks(5770));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 18, 12, 7, 58, 800, DateTimeKind.Utc).AddTicks(5773));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 18, 12, 7, 59, 832, DateTimeKind.Utc).AddTicks(6673));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 18, 12, 7, 59, 832, DateTimeKind.Utc).AddTicks(6678));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 18, 12, 7, 59, 832, DateTimeKind.Utc).AddTicks(6680));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 18, 12, 7, 59, 832, DateTimeKind.Utc).AddTicks(6683));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 18, 12, 7, 59, 832, DateTimeKind.Utc).AddTicks(6685));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 18, 12, 7, 59, 832, DateTimeKind.Utc).AddTicks(6689));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 18, 12, 7, 58, 800, DateTimeKind.Utc).AddTicks(5975));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 18, 12, 7, 58, 800, DateTimeKind.Utc).AddTicks(5979));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 18, 12, 7, 58, 800, DateTimeKind.Utc).AddTicks(5982));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 18, 12, 7, 59, 19, DateTimeKind.Utc).AddTicks(4490), "$2a$11$6qgFE9l23dbirlk7LMZS3.4KvmQ8lvSnKDCPLQNOaDTyCm0od6EsW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 18, 12, 7, 59, 222, DateTimeKind.Utc).AddTicks(4646), "$2a$11$vNwpgkvcWM9XLk4elWRADuIhQb0NZ/sZFqOA/CELsmmj1pTJOm92G" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 18, 12, 7, 59, 426, DateTimeKind.Utc).AddTicks(7661), "$2a$11$No5D41uYuG1.5fEyngGxfOmWZ90oAJ6L9TYDAfRF4C/y.Gu9zxCmm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 18, 12, 7, 59, 625, DateTimeKind.Utc).AddTicks(4897), "$2a$11$BXRh0a8QAOLNePs28YLRue7WsXBVqoS2CNScss.36ArRnhUne5Vza" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 18, 12, 7, 59, 827, DateTimeKind.Utc).AddTicks(7567), "$2a$11$ayDd/66iN/SFEbKPLcBfduqgzgWGo9GApiYjz4/oHGlAKUq/wmCXG" });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CreatedBy",
                table: "Invoices",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Users_CreatedBy",
                table: "Invoices",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Users_CreatedBy",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_CreatedBy",
                table: "Invoices");

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
    }
}
