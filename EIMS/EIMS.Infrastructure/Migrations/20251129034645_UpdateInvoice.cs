using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdjustmentReason",
                table: "Invoices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InvoiceType",
                table: "Invoices",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OriginalInvoiceID",
                table: "Invoices",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 3, 46, 42, 998, DateTimeKind.Utc).AddTicks(9886));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 3, 46, 42, 998, DateTimeKind.Utc).AddTicks(9892));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 3, 46, 42, 998, DateTimeKind.Utc).AddTicks(9917));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 3, 46, 42, 998, DateTimeKind.Utc).AddTicks(9952));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 3, 46, 42, 998, DateTimeKind.Utc).AddTicks(9956));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 3, 46, 42, 998, DateTimeKind.Utc).AddTicks(9958));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 29, 3, 46, 43, 108, DateTimeKind.Utc).AddTicks(2083), "$2a$11$7mCYGoq1q5DZdEXskcDRl.DQQpxGPanPb/F3KMnKu869tJn80u.Qq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 29, 3, 46, 43, 218, DateTimeKind.Utc).AddTicks(8547), "$2a$11$rgl7R1qyLYRcKFRpB1gj/eM.qS6p14Fv1CNSrqP8LgjNT1IMKfyoi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 29, 3, 46, 43, 330, DateTimeKind.Utc).AddTicks(982), "$2a$11$flfTtUoJQ/vFSgC/e6etMuALvTZHqU.JqIe07FlvVMyvRh50ibDCW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 29, 3, 46, 43, 442, DateTimeKind.Utc).AddTicks(8617), "$2a$11$/BkxR4VzTxO4SB8vL2P04OeHHY2tHP2kwzUt2dR/SXD2w7YDXb1q2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 29, 3, 46, 43, 554, DateTimeKind.Utc).AddTicks(3326), "$2a$11$6jvPuFNsRfq1jyuVYOTJM.JI.FpxfzUdt9DrbpVdszVk9wEEo1Uim" });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_OriginalInvoiceID",
                table: "Invoices",
                column: "OriginalInvoiceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Invoices_OriginalInvoiceID",
                table: "Invoices",
                column: "OriginalInvoiceID",
                principalTable: "Invoices",
                principalColumn: "InvoiceID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Invoices_OriginalInvoiceID",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_OriginalInvoiceID",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "AdjustmentReason",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "InvoiceType",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "OriginalInvoiceID",
                table: "Invoices");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 10, 8, 42, 875, DateTimeKind.Utc).AddTicks(8040));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 10, 8, 42, 875, DateTimeKind.Utc).AddTicks(8059));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 10, 8, 42, 875, DateTimeKind.Utc).AddTicks(8065));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 10, 8, 42, 875, DateTimeKind.Utc).AddTicks(8217));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 10, 8, 42, 875, DateTimeKind.Utc).AddTicks(8224));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 10, 8, 42, 875, DateTimeKind.Utc).AddTicks(8230));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 10, 8, 43, 55, DateTimeKind.Utc).AddTicks(3493), "$2a$11$b/j7DOxd/N3H3cu1A7qJS.KzDDPKNRLfIxe06eQr3DX03dAI9cGwC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 10, 8, 43, 227, DateTimeKind.Utc).AddTicks(3423), "$2a$11$ANSLPs91zsYErxZ.WlZ8iuSFOO.W.2MwBVHVCNpLLO11YBzBEyOXq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 10, 8, 43, 408, DateTimeKind.Utc).AddTicks(7914), "$2a$11$lJMoxiN3aht1oYhgQDRWK.1KqTtLhXzxc.FKgW9V752XpEW2tu9ma" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 10, 8, 43, 590, DateTimeKind.Utc).AddTicks(978), "$2a$11$xwSsHoH0V3oYFuLq8jX0pOrVdoeE.HdUtjrhhDAhRf0ECgk1XdNVy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 10, 8, 43, 768, DateTimeKind.Utc).AddTicks(7722), "$2a$11$wKeTo1IcrcFuTpJ5w08dMOCKjbrQ809wYLUjiW1cKpQNl30alJXam" });
        }
    }
}
