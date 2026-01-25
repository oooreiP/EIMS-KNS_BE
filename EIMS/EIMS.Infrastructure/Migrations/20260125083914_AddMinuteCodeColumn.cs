using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMinuteCodeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MinuteCode",
                table: "Invoices",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 8, 39, 7, 972, DateTimeKind.Utc).AddTicks(3548));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 8, 39, 7, 972, DateTimeKind.Utc).AddTicks(3558));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 8, 39, 7, 972, DateTimeKind.Utc).AddTicks(3561));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 25, 8, 39, 8, 760, DateTimeKind.Utc).AddTicks(4776));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 25, 8, 39, 8, 760, DateTimeKind.Utc).AddTicks(4789));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 25, 8, 39, 8, 760, DateTimeKind.Utc).AddTicks(4794));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 25, 8, 39, 8, 760, DateTimeKind.Utc).AddTicks(4799));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 25, 8, 39, 8, 760, DateTimeKind.Utc).AddTicks(4804));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 25, 8, 39, 8, 760, DateTimeKind.Utc).AddTicks(4808));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 25, 8, 39, 8, 760, DateTimeKind.Utc).AddTicks(4816));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 8, 39, 7, 972, DateTimeKind.Utc).AddTicks(3615));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 8, 39, 7, 972, DateTimeKind.Utc).AddTicks(3622));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 8, 39, 7, 972, DateTimeKind.Utc).AddTicks(3625));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 25, 8, 39, 8, 131, DateTimeKind.Utc).AddTicks(8176), "$2a$11$OjLTslJY203coLtRBhBTJu2kxxLFn9KzE1kFTmucpJ.ANXgEzjZy6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 25, 8, 39, 8, 289, DateTimeKind.Utc).AddTicks(9897), "$2a$11$Cz6DnNXJQ/fr3xNO9Ojl/u0UcuTN3WRNalLQ3QGPr6XuI3W92FscK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 25, 8, 39, 8, 448, DateTimeKind.Utc).AddTicks(7086), "$2a$11$maBToXuQSZXQpV3YuiTuueilouHEBKI0bRNFI4BbIb43OspESZo2W" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 25, 8, 39, 8, 605, DateTimeKind.Utc).AddTicks(5082), "$2a$11$Mq2d3/unrWM8WbrlgHztW.MMFLjmR9vaa7hkZa2qEapJf8/Oo1wwu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 25, 8, 39, 8, 760, DateTimeKind.Utc).AddTicks(683), "$2a$11$cCYIeThzScfAcF0KEauy7.HNJi43qXrLaqy27Gx3bnOxD6HuX9sve" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinuteCode",
                table: "Invoices");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 7, 58, 46, 805, DateTimeKind.Utc).AddTicks(5376));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 7, 58, 46, 805, DateTimeKind.Utc).AddTicks(5386));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 7, 58, 46, 805, DateTimeKind.Utc).AddTicks(5392));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 25, 7, 58, 47, 385, DateTimeKind.Utc).AddTicks(9303));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 25, 7, 58, 47, 385, DateTimeKind.Utc).AddTicks(9308));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 25, 7, 58, 47, 385, DateTimeKind.Utc).AddTicks(9310));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 25, 7, 58, 47, 385, DateTimeKind.Utc).AddTicks(9311));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 25, 7, 58, 47, 385, DateTimeKind.Utc).AddTicks(9313));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 25, 7, 58, 47, 385, DateTimeKind.Utc).AddTicks(9314));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 25, 7, 58, 47, 385, DateTimeKind.Utc).AddTicks(9320));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 7, 58, 46, 805, DateTimeKind.Utc).AddTicks(5422));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 7, 58, 46, 805, DateTimeKind.Utc).AddTicks(5426));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 7, 58, 46, 805, DateTimeKind.Utc).AddTicks(5428));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 25, 7, 58, 46, 925, DateTimeKind.Utc).AddTicks(7376), "$2a$11$csX5Q9SXiTBV2QEUXmBr7uNP2b09glSSxW9gYypggq.D0o9gXBsiO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 25, 7, 58, 47, 42, DateTimeKind.Utc).AddTicks(463), "$2a$11$avPz95Gvb14vw83TPNGDa.h.34wJE0m7f60u3kFngiVeT3fWCLfDC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 25, 7, 58, 47, 156, DateTimeKind.Utc).AddTicks(2153), "$2a$11$LpbS3Wj9SC/Y6pWvJTmtPOAaQuzivV2xrpIIslQI0YynkuqWkTgSe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 25, 7, 58, 47, 266, DateTimeKind.Utc).AddTicks(2572), "$2a$11$/1fI5ASa42QVJ1Qm46iPae3bc5xeCqn0w9CaoJNBGrKIxK1bF/LEi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 25, 7, 58, 47, 385, DateTimeKind.Utc).AddTicks(8045), "$2a$11$xafv3JbTy9edNZ1KZhR8g.xV4F4u9mFEhlr25a0lsGVnJi2bb11Eq" });
        }
    }
}
