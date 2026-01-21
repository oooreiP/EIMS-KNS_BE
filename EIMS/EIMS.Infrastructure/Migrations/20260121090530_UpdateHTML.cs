using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHTML : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceRequests_Users_SalesUserID",
                table: "InvoiceRequests");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceRequests_SalesUserID",
                table: "InvoiceRequests");

            migrationBuilder.DropColumn(
                name: "SalesUserID",
                table: "InvoiceRequests");

            migrationBuilder.AddColumn<string>(
                name: "RenderedHtml",
                table: "InvoiceTemplates",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 21, 9, 5, 27, 33, DateTimeKind.Utc).AddTicks(5490));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 21, 9, 5, 27, 33, DateTimeKind.Utc).AddTicks(5495));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 21, 9, 5, 27, 33, DateTimeKind.Utc).AddTicks(5497));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 9, 5, 27, 587, DateTimeKind.Utc).AddTicks(872));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 9, 5, 27, 587, DateTimeKind.Utc).AddTicks(876));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 9, 5, 27, 587, DateTimeKind.Utc).AddTicks(878));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 9, 5, 27, 587, DateTimeKind.Utc).AddTicks(879));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 9, 5, 27, 587, DateTimeKind.Utc).AddTicks(880));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 9, 5, 27, 587, DateTimeKind.Utc).AddTicks(882));

            migrationBuilder.UpdateData(
                table: "InvoiceTemplates",
                keyColumn: "TemplateID",
                keyValue: -1,
                column: "RenderedHtml",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 21, 9, 5, 27, 33, DateTimeKind.Utc).AddTicks(5529));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 21, 9, 5, 27, 33, DateTimeKind.Utc).AddTicks(5532));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 21, 9, 5, 27, 33, DateTimeKind.Utc).AddTicks(5534));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 21, 9, 5, 27, 143, DateTimeKind.Utc).AddTicks(6904), "$2a$11$suZdFIokeNrXwJTHz89kgegVoYBrejHSqlyER96QG44n/iXBbJsZO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 21, 9, 5, 27, 253, DateTimeKind.Utc).AddTicks(6979), "$2a$11$2HXcAb7Ke4vjg84YrpD5zOOKnm3WCdEdJzx/MUHynkDFw13g2NUSS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 21, 9, 5, 27, 364, DateTimeKind.Utc).AddTicks(6830), "$2a$11$ME7MeIrzRdi/PV1LQ3EoYum6BNJ9DsCRyre7SipYCAEvg6Ms36/bS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 21, 9, 5, 27, 475, DateTimeKind.Utc).AddTicks(9495), "$2a$11$vevKT2hFol0www74Hvw7z.WJD6.iDrW1b1M7.iqVgPIeTwHRgw/my" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 21, 9, 5, 27, 585, DateTimeKind.Utc).AddTicks(3226), "$2a$11$tpT4lq0yoBf8KvuflKWl9u0TErfQDdMeaGjfPpY1Pln28zu7dzm5." });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRequests_SaleID",
                table: "InvoiceRequests",
                column: "SaleID");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceRequests_Users_SaleID",
                table: "InvoiceRequests",
                column: "SaleID",
                principalTable: "Users",
                principalColumn: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceRequests_Users_SaleID",
                table: "InvoiceRequests");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceRequests_SaleID",
                table: "InvoiceRequests");

            migrationBuilder.DropColumn(
                name: "RenderedHtml",
                table: "InvoiceTemplates");

            migrationBuilder.AddColumn<int>(
                name: "SalesUserID",
                table: "InvoiceRequests",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 19, 12, 31, 34, 178, DateTimeKind.Utc).AddTicks(9461));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 19, 12, 31, 34, 178, DateTimeKind.Utc).AddTicks(9468));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 19, 12, 31, 34, 178, DateTimeKind.Utc).AddTicks(9470));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 19, 12, 31, 34, 773, DateTimeKind.Utc).AddTicks(1415));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 19, 12, 31, 34, 773, DateTimeKind.Utc).AddTicks(1421));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 19, 12, 31, 34, 773, DateTimeKind.Utc).AddTicks(1423));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 19, 12, 31, 34, 773, DateTimeKind.Utc).AddTicks(1424));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 19, 12, 31, 34, 773, DateTimeKind.Utc).AddTicks(1425));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 19, 12, 31, 34, 773, DateTimeKind.Utc).AddTicks(1427));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 19, 12, 31, 34, 178, DateTimeKind.Utc).AddTicks(9504));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 19, 12, 31, 34, 178, DateTimeKind.Utc).AddTicks(9507));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 19, 12, 31, 34, 178, DateTimeKind.Utc).AddTicks(9509));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 19, 12, 31, 34, 296, DateTimeKind.Utc).AddTicks(8801), "$2a$11$L8lzuKIEsrDNu84yR5FQGO0K6J4eO.GdQB7XNDAg7QnDkGl3p3kki" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 19, 12, 31, 34, 417, DateTimeKind.Utc).AddTicks(5509), "$2a$11$zeUzreDAhfiqQKwLfiSLFugN.XnV1lL2fNsa1MRr9s7ZyEZTrXjWC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 19, 12, 31, 34, 537, DateTimeKind.Utc).AddTicks(2685), "$2a$11$rBhKbPLDnEquQyshqhZDOOU0F6MaLTk4NZhplZH8kOF.LBfXAxrfG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 19, 12, 31, 34, 653, DateTimeKind.Utc).AddTicks(3600), "$2a$11$K0Xz4fu4L/6mqcMcSKd5me4ovF31NqaJuRY4B3hk8Nl2j6qkvNtN6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 19, 12, 31, 34, 770, DateTimeKind.Utc).AddTicks(6453), "$2a$11$MAX3HURBOUfE1OEj.4yIget.1O0h68dXyu2YRzmp95Ap3/6YhsRei" });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRequests_SalesUserID",
                table: "InvoiceRequests",
                column: "SalesUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceRequests_Users_SalesUserID",
                table: "InvoiceRequests",
                column: "SalesUserID",
                principalTable: "Users",
                principalColumn: "UserID");
        }
    }
}
