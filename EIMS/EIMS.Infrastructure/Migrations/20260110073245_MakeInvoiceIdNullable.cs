using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeInvoiceIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaxApiLogs_Invoices_InvoiceID",
                table: "TaxApiLogs");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceID",
                table: "TaxApiLogs",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 7, 32, 42, 909, DateTimeKind.Utc).AddTicks(3299));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 7, 32, 42, 909, DateTimeKind.Utc).AddTicks(3305));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 7, 32, 42, 909, DateTimeKind.Utc).AddTicks(3307));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 10, 7, 32, 43, 471, DateTimeKind.Utc).AddTicks(7244));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 10, 7, 32, 43, 471, DateTimeKind.Utc).AddTicks(7248));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 10, 7, 32, 43, 471, DateTimeKind.Utc).AddTicks(7249));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 7, 32, 42, 909, DateTimeKind.Utc).AddTicks(3373));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 7, 32, 42, 909, DateTimeKind.Utc).AddTicks(3376));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 7, 32, 42, 909, DateTimeKind.Utc).AddTicks(3378));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 7, 32, 43, 19, DateTimeKind.Utc).AddTicks(7192), "$2a$11$USo62fC7LniogSHAC6thtu43VVs.sck4EYOmOg0suF8U3vrWQIok6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 7, 32, 43, 131, DateTimeKind.Utc).AddTicks(2037), "$2a$11$zyCKh5B79NlmKCsX/W0dA.jp0nvPjuvv5udEtMifZWr6H4zLzBJWq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 7, 32, 43, 246, DateTimeKind.Utc).AddTicks(5159), "$2a$11$YnjU4V5H/TBhH7Otw.pi0umkRAWkJly.LIF4.DAFGz0bqdHvK3DgO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 7, 32, 43, 356, DateTimeKind.Utc).AddTicks(4113), "$2a$11$LprriinVJ3PZTa6eyz9wRurUy/3u1FXx5y7LBVd1teUzrxIYoslAu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 7, 32, 43, 469, DateTimeKind.Utc).AddTicks(9907), "$2a$11$O1p8QVhOh.sCvf/21Y3f6.W.2E5pEK1NLTRaN/2ngOqRbKAp7tvoG" });

            migrationBuilder.AddForeignKey(
                name: "FK_TaxApiLogs_Invoices_InvoiceID",
                table: "TaxApiLogs",
                column: "InvoiceID",
                principalTable: "Invoices",
                principalColumn: "InvoiceID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaxApiLogs_Invoices_InvoiceID",
                table: "TaxApiLogs");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceID",
                table: "TaxApiLogs",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 21, 36, 35, 430, DateTimeKind.Utc).AddTicks(5315));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 21, 36, 35, 430, DateTimeKind.Utc).AddTicks(5328));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 21, 36, 35, 430, DateTimeKind.Utc).AddTicks(5331));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 9, 21, 36, 35, 995, DateTimeKind.Utc).AddTicks(5896));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 9, 21, 36, 35, 995, DateTimeKind.Utc).AddTicks(5901));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 9, 21, 36, 35, 995, DateTimeKind.Utc).AddTicks(5903));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 21, 36, 35, 430, DateTimeKind.Utc).AddTicks(5373));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 21, 36, 35, 430, DateTimeKind.Utc).AddTicks(5376));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 21, 36, 35, 430, DateTimeKind.Utc).AddTicks(5434));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 21, 36, 35, 546, DateTimeKind.Utc).AddTicks(8147), "$2a$11$ZW5vpsDxHOQI20Ovuv0.NeHoKMw5sFwxRwkhhSsnB/3Kid/ZSNXAS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 21, 36, 35, 659, DateTimeKind.Utc).AddTicks(9121), "$2a$11$VAAR6ro9zaoTMciYs5oNp.WhsxbRoqW2krlJPsD/dsvat8M4qVlKK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 21, 36, 35, 770, DateTimeKind.Utc).AddTicks(8297), "$2a$11$EbwNJ8WcRODlQZKMexwbhu1HDbPk4Ez.3uUZFyS7cRFEpwFEdspMO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 21, 36, 35, 881, DateTimeKind.Utc).AddTicks(9965), "$2a$11$VSI2hVZK5kjFfJCIS7vkl.vf8qnEs1So/l7n6/M4tH0d.yqJBXxK2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 21, 36, 35, 994, DateTimeKind.Utc).AddTicks(1016), "$2a$11$dU3omGlriSRrQd63PfjjWuMBXgy8uZAFztAcolP8ygrW3kF60oGBG" });

            migrationBuilder.AddForeignKey(
                name: "FK_TaxApiLogs_Invoices_InvoiceID",
                table: "TaxApiLogs",
                column: "InvoiceID",
                principalTable: "Invoices",
                principalColumn: "InvoiceID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
