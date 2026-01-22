using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLogoCompany : Migration
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
                name: "LogoUrl",
                table: "Companies",
                type: "character varying(500)",
                maxLength: 500,
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
                table: "Companies",
                keyColumn: "CompanyID",
                keyValue: 1,
                column: "LogoUrl",
                value: null);

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
                name: "LogoUrl",
                table: "Companies");

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
