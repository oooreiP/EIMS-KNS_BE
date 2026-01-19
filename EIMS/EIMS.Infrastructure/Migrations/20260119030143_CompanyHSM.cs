using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CompanyHSM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "DigitalSignature",
                table: "Companies",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DigitalSignaturePassword",
                table: "Companies",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 19, 3, 1, 40, 492, DateTimeKind.Utc).AddTicks(5929));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 19, 3, 1, 40, 492, DateTimeKind.Utc).AddTicks(5934));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 19, 3, 1, 40, 492, DateTimeKind.Utc).AddTicks(5937));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "CompanyID",
                keyValue: 1,
                columns: new[] { "DigitalSignature", "DigitalSignaturePassword" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 19, 3, 1, 41, 54, DateTimeKind.Utc).AddTicks(2420));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 19, 3, 1, 41, 54, DateTimeKind.Utc).AddTicks(2425));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 19, 3, 1, 41, 54, DateTimeKind.Utc).AddTicks(2427));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 19, 3, 1, 41, 54, DateTimeKind.Utc).AddTicks(2428));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 19, 3, 1, 41, 54, DateTimeKind.Utc).AddTicks(2463));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 19, 3, 1, 41, 54, DateTimeKind.Utc).AddTicks(2465));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 19, 3, 1, 40, 492, DateTimeKind.Utc).AddTicks(5967));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 19, 3, 1, 40, 492, DateTimeKind.Utc).AddTicks(5971));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 19, 3, 1, 40, 492, DateTimeKind.Utc).AddTicks(5974));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 19, 3, 1, 40, 603, DateTimeKind.Utc).AddTicks(1230), "$2a$11$XJ6UfZcHq7V2ZNI.HXnSsOG6i6PJCTgtTQX6URied3X8fZAasMTV2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 19, 3, 1, 40, 718, DateTimeKind.Utc).AddTicks(6102), "$2a$11$fi9kkkU9NsbY/bUs1T8MguxNY/LpbVKDHIexm.3Lrw/Vl2DOC1KUO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 19, 3, 1, 40, 829, DateTimeKind.Utc).AddTicks(2894), "$2a$11$nZwF4W5PW1nMbm9SCJ9XOO0.AF0u0YfYLo7LOfb0WdQnYaj.H16.W" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 19, 3, 1, 40, 941, DateTimeKind.Utc).AddTicks(7475), "$2a$11$qqha8q37IsuYWa.ogIMONO8nXs/ALEunlV8sQOnR.gbKPODVRs3JS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 19, 3, 1, 41, 52, DateTimeKind.Utc).AddTicks(961), "$2a$11$rQq3seCNWs5XcPe43UtTuuWotByIMwtTJ.sK9eWzCf.LBPcCEvWsO" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DigitalSignature",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "DigitalSignaturePassword",
                table: "Companies");

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
        }
    }
}
