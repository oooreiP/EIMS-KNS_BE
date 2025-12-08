using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoicePaymentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoicePayment",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvoiceID = table.Column<int>(type: "integer", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentMethod = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    TransactionCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePayment", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK_InvoicePayment_Invoices_InvoiceID",
                        column: x => x.InvoiceID,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 11, 58, 14, 119, DateTimeKind.Utc).AddTicks(3312));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 11, 58, 14, 119, DateTimeKind.Utc).AddTicks(3321));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 11, 58, 14, 119, DateTimeKind.Utc).AddTicks(3324));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 11, 58, 14, 119, DateTimeKind.Utc).AddTicks(3351));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 11, 58, 14, 119, DateTimeKind.Utc).AddTicks(3354));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 11, 58, 14, 119, DateTimeKind.Utc).AddTicks(3356));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 8, 11, 58, 14, 235, DateTimeKind.Utc).AddTicks(5578), "$2a$11$MSE0fDYBkvoSNwoVIe6dvufuG5zNfZ1arVYb69TOKrorM4/7hDBPi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 8, 11, 58, 14, 351, DateTimeKind.Utc).AddTicks(4669), "$2a$11$locYOzUXcpdcg4mjjGh5ROC4Qda7MhdtcdYMPvMf8juKk3RS5tS.W" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 8, 11, 58, 14, 465, DateTimeKind.Utc).AddTicks(2398), "$2a$11$HYRfT8j9Nz.l2bOCaZJdyeKQ7pt07rD5UO42hB2nDgh9osvbSFzeG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 8, 11, 58, 14, 577, DateTimeKind.Utc).AddTicks(2735), "$2a$11$wMuLIj9alYdjzrARjkloounEEPH.PVTbUJiWSIIy0mFUl0TdWTtkG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 8, 11, 58, 14, 686, DateTimeKind.Utc).AddTicks(7945), "$2a$11$ax80jg80k9IXRV.Oaz/Md.iNWuT3OvGfAnflsM2EdKjbsMLN4H8BS" });

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePayment_InvoiceID",
                table: "InvoicePayment",
                column: "InvoiceID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoicePayment");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 8, 28, 22, 938, DateTimeKind.Utc).AddTicks(1810));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 8, 28, 22, 938, DateTimeKind.Utc).AddTicks(1815));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 8, 28, 22, 938, DateTimeKind.Utc).AddTicks(1818));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 8, 28, 22, 938, DateTimeKind.Utc).AddTicks(1852));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 8, 28, 22, 938, DateTimeKind.Utc).AddTicks(1855));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 8, 28, 22, 938, DateTimeKind.Utc).AddTicks(1858));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 7, 8, 28, 23, 50, DateTimeKind.Utc).AddTicks(986), "$2a$11$hmkakHGz3mHvXc3y/GZRUenPsmCwfW6pq3b00CbCvRWcAwUmaYrI6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 7, 8, 28, 23, 163, DateTimeKind.Utc).AddTicks(2057), "$2a$11$zzcajXCd/w.Huerg1No0weSumywxdwRm6qIRD/Av56/ZM4HORPSDi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 7, 8, 28, 23, 274, DateTimeKind.Utc).AddTicks(9866), "$2a$11$lpTgr30btg5e/fSFQMvHLenuNg.ST63IVy197BthI6dTMT6eNyis6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 7, 8, 28, 23, 386, DateTimeKind.Utc).AddTicks(4906), "$2a$11$IWWhiPHMcdf/a4ejUfRIROI1OonUDzLY3YXdPy/UCPFhNq6rIJTR6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 7, 8, 28, 23, 497, DateTimeKind.Utc).AddTicks(3691), "$2a$11$isO2p.VSKg8jNReoDUkdTu7.pj8/aXOiVfvzVm/tsCKEk8vf.EtVe" });
        }
    }
}
