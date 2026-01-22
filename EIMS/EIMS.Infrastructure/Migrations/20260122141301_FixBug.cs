using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixBug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RenderedHtml",
                table: "InvoiceTemplates");

            migrationBuilder.AddColumn<decimal>(
                name: "NewCharges",
                table: "InvoiceStatements",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OpeningBalance",
                table: "InvoiceStatements",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PeriodMonth",
                table: "InvoiceStatements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PeriodYear",
                table: "InvoiceStatements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SaleID",
                table: "Customers",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 22, 14, 12, 59, 717, DateTimeKind.Utc).AddTicks(5169));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 22, 14, 12, 59, 717, DateTimeKind.Utc).AddTicks(5180));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 22, 14, 12, 59, 717, DateTimeKind.Utc).AddTicks(5183));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 13, 0, 506, DateTimeKind.Utc).AddTicks(1846));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 13, 0, 506, DateTimeKind.Utc).AddTicks(1852));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 13, 0, 506, DateTimeKind.Utc).AddTicks(1854));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 13, 0, 506, DateTimeKind.Utc).AddTicks(1856));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 13, 0, 506, DateTimeKind.Utc).AddTicks(1858));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 13, 0, 506, DateTimeKind.Utc).AddTicks(1860));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 22, 14, 12, 59, 717, DateTimeKind.Utc).AddTicks(5285));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 22, 14, 12, 59, 717, DateTimeKind.Utc).AddTicks(5290));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 22, 14, 12, 59, 717, DateTimeKind.Utc).AddTicks(5293));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 22, 14, 12, 59, 876, DateTimeKind.Utc).AddTicks(488), "$2a$11$wKNJnuwQHsUj0BnwBpKGBe5kuJUbcbPtSVaFv7FIHOAVf21gTB.4W" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 22, 14, 13, 0, 33, DateTimeKind.Utc).AddTicks(3893), "$2a$11$w5RrXo62vGn//TpPln5Exu/PwU4Xvi1LRFpxTk2OubI4xse7kNkg2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 22, 14, 13, 0, 191, DateTimeKind.Utc).AddTicks(7539), "$2a$11$yPdTeElKJMZa4uWJxgoQD.xXCyZ0Mc3oN8eSeJNuaJJHV6bqRCczW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 22, 14, 13, 0, 346, DateTimeKind.Utc).AddTicks(8539), "$2a$11$rneUA3eiCj8nNIGZwuYePOxY4n9CtZhSMWNNmydYBuiwhN3fmqzV2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 22, 14, 13, 0, 503, DateTimeKind.Utc).AddTicks(6867), "$2a$11$OjqpHgFWr95tb9N2U5fqIedIlDcEjVoViS2uieb7hZVLGbsjT5zZ." });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_SaleID",
                table: "Customers",
                column: "SaleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Users_SaleID",
                table: "Customers",
                column: "SaleID",
                principalTable: "Users",
                principalColumn: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Users_SaleID",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_SaleID",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "NewCharges",
                table: "InvoiceStatements");

            migrationBuilder.DropColumn(
                name: "OpeningBalance",
                table: "InvoiceStatements");

            migrationBuilder.DropColumn(
                name: "PeriodMonth",
                table: "InvoiceStatements");

            migrationBuilder.DropColumn(
                name: "PeriodYear",
                table: "InvoiceStatements");

            migrationBuilder.DropColumn(
                name: "SaleID",
                table: "Customers");

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
                value: new DateTime(2026, 1, 22, 11, 50, 11, 143, DateTimeKind.Utc).AddTicks(9072));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 22, 11, 50, 11, 143, DateTimeKind.Utc).AddTicks(9084));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 22, 11, 50, 11, 143, DateTimeKind.Utc).AddTicks(9090));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 11, 50, 11, 992, DateTimeKind.Utc).AddTicks(3904));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 11, 50, 11, 992, DateTimeKind.Utc).AddTicks(3912));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 11, 50, 11, 992, DateTimeKind.Utc).AddTicks(3916));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 11, 50, 11, 992, DateTimeKind.Utc).AddTicks(3919));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 11, 50, 11, 992, DateTimeKind.Utc).AddTicks(3922));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 11, 50, 11, 992, DateTimeKind.Utc).AddTicks(3925));

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
                value: new DateTime(2026, 1, 22, 11, 50, 11, 143, DateTimeKind.Utc).AddTicks(9163));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 22, 11, 50, 11, 143, DateTimeKind.Utc).AddTicks(9172));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 22, 11, 50, 11, 143, DateTimeKind.Utc).AddTicks(9178));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 22, 11, 50, 11, 299, DateTimeKind.Utc).AddTicks(7190), "$2a$11$2d4yACvb55pkqR7udIAUtOQrG56UXoPcYHC9jDsEMnSgFDQPeHrj." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 22, 11, 50, 11, 460, DateTimeKind.Utc).AddTicks(8528), "$2a$11$wDP1WLfN6R4qr79BIQIExuqR047Uat4nMmAK8vkDNNJXMxBx8sk.." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 22, 11, 50, 11, 618, DateTimeKind.Utc).AddTicks(1645), "$2a$11$47pmqo0wA7GmPhN0KpS0P.8XfWPRhfJENi4agnDEl.9y6cNPQDIGC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 22, 11, 50, 11, 773, DateTimeKind.Utc).AddTicks(3612), "$2a$11$U/gGy/CMSvjo29QREMA06OdMvFhBPJfHfvdRpkx2M.zillYGKG5zu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 22, 11, 50, 11, 987, DateTimeKind.Utc).AddTicks(8199), "$2a$11$KRG7L13W.kM3q9Cv3id48efPYQNjHc8Q9OsoVNnC.ItgyIvcAgUM6" });
        }
    }
}
