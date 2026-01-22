using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnForStatement : Migration
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
                value: new DateTime(2026, 1, 22, 11, 41, 36, 141, DateTimeKind.Utc).AddTicks(9939));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 22, 11, 41, 36, 141, DateTimeKind.Utc).AddTicks(9947));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 22, 11, 41, 36, 141, DateTimeKind.Utc).AddTicks(9950));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 11, 41, 36, 702, DateTimeKind.Utc).AddTicks(1010));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 11, 41, 36, 702, DateTimeKind.Utc).AddTicks(1015));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 11, 41, 36, 702, DateTimeKind.Utc).AddTicks(1017));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 11, 41, 36, 702, DateTimeKind.Utc).AddTicks(1018));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 11, 41, 36, 702, DateTimeKind.Utc).AddTicks(1019));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 11, 41, 36, 702, DateTimeKind.Utc).AddTicks(1020));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 22, 11, 41, 36, 141, DateTimeKind.Utc).AddTicks(9977));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 22, 11, 41, 36, 141, DateTimeKind.Utc).AddTicks(9980));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 22, 11, 41, 36, 141, DateTimeKind.Utc).AddTicks(9982));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 22, 11, 41, 36, 252, DateTimeKind.Utc).AddTicks(8927), "$2a$11$4KVTL4he4T1rLYHt8YLglOlMcGazKVghmaxYa.BvQw0a7k8sFI.oC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 22, 11, 41, 36, 365, DateTimeKind.Utc).AddTicks(2254), "$2a$11$0AMip1w0HHcPK0VjXK98PuQbltUcafEcWCBHMXnq0pBpX85lFq5fS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 22, 11, 41, 36, 478, DateTimeKind.Utc).AddTicks(1677), "$2a$11$PEVkfstlBGmyCGMDfm3mG.ANpQGqxRkEsMpG1SuphdQbk6sjWHOou" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 22, 11, 41, 36, 590, DateTimeKind.Utc).AddTicks(1760), "$2a$11$4oD/z71LfLwFyvOOM8w/OOiUzb8Z7wPH6HH.5u9Sc5iGwL4iBl/ca" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 22, 11, 41, 36, 701, DateTimeKind.Utc).AddTicks(196), "$2a$11$CCQD4db6WKlhnz/ElBi.nOaMKFO7pGA1IjGYvHyYJVo/M7Max.a8y" });

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
        }
    }
}
