using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentStatusID",
                table: "Invoices",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PaymentStatuses",
                columns: table => new
                {
                    PaymentStatusID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentStatuses", x => x.PaymentStatusID);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 7, 13, 48, 11, 952, DateTimeKind.Utc).AddTicks(5175));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 7, 13, 48, 11, 952, DateTimeKind.Utc).AddTicks(5187));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 7, 13, 48, 11, 952, DateTimeKind.Utc).AddTicks(5190));

            migrationBuilder.InsertData(
                table: "InvoiceStatuses",
                columns: new[] { "InvoiceStatusID", "StatusName" },
                values: new object[,]
                {
                    { 1, "Draft" },
                    { 2, "Issued" },
                    { 3, "Cancelled" },
                    { 4, "Adjusted" },
                    { 5, "Replaced" }
                });

            migrationBuilder.InsertData(
                table: "PaymentStatuses",
                columns: new[] { "PaymentStatusID", "StatusName" },
                values: new object[,]
                {
                    { 1, "Unpaid" },
                    { 2, "Partially Paid" },
                    { 3, "Paid" },
                    { 4, "Overdue" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 7, 13, 48, 11, 952, DateTimeKind.Utc).AddTicks(5244));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 7, 13, 48, 11, 952, DateTimeKind.Utc).AddTicks(5249));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 7, 13, 48, 11, 952, DateTimeKind.Utc).AddTicks(5252));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 7, 13, 48, 12, 113, DateTimeKind.Utc).AddTicks(5777), "$2a$11$7QJyFHfGcxmjssKMmUzbl.tB7xdonrlLUL28tMG7Ke0c.j6tGfy32" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 7, 13, 48, 12, 273, DateTimeKind.Utc).AddTicks(7107), "$2a$11$vqW0o6gU6eomi.v.IJ.H6Om1od0RjkTt6l4.B/0YX1hB2BdYnXnti" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 7, 13, 48, 12, 430, DateTimeKind.Utc).AddTicks(7639), "$2a$11$kJdr5xpXfGKxb64omcKkuuGvdZKuFIzM4rTPRnxlvkoWBoOHs68w6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 7, 13, 48, 12, 596, DateTimeKind.Utc).AddTicks(2979), "$2a$11$UUWHvfT2ueMVmgoFGejXi.2jYgZ/BEA5dpIL8dOy8/FYIriAjQ5w." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 7, 13, 48, 12, 754, DateTimeKind.Utc).AddTicks(2147), "$2a$11$CM3IR3R2SD77NtAq0dJdguI8YgGHiyxgySXfMjqS6Udtsy2/qImsW" });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PaymentStatusID",
                table: "Invoices",
                column: "PaymentStatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_PaymentStatuses_PaymentStatusID",
                table: "Invoices",
                column: "PaymentStatusID",
                principalTable: "PaymentStatuses",
                principalColumn: "PaymentStatusID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_PaymentStatuses_PaymentStatusID",
                table: "Invoices");

            migrationBuilder.DropTable(
                name: "PaymentStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_PaymentStatusID",
                table: "Invoices");

            migrationBuilder.DeleteData(
                table: "InvoiceStatuses",
                keyColumn: "InvoiceStatusID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "InvoiceStatuses",
                keyColumn: "InvoiceStatusID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "InvoiceStatuses",
                keyColumn: "InvoiceStatusID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "InvoiceStatuses",
                keyColumn: "InvoiceStatusID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "InvoiceStatuses",
                keyColumn: "InvoiceStatusID",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "PaymentStatusID",
                table: "Invoices");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 6, 13, 56, 13, 268, DateTimeKind.Utc).AddTicks(7115));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 6, 13, 56, 13, 268, DateTimeKind.Utc).AddTicks(7134));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 6, 13, 56, 13, 268, DateTimeKind.Utc).AddTicks(7137));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 6, 13, 56, 13, 268, DateTimeKind.Utc).AddTicks(7203));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 6, 13, 56, 13, 268, DateTimeKind.Utc).AddTicks(7207));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 6, 13, 56, 13, 268, DateTimeKind.Utc).AddTicks(7211));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 6, 13, 56, 13, 426, DateTimeKind.Utc).AddTicks(383), "$2a$11$n/9c6eKIiaDI3aVVgJr2uOSLd2FN1M8VDZwTTrtIVdfhAJCUHNMtu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 6, 13, 56, 13, 585, DateTimeKind.Utc).AddTicks(1960), "$2a$11$2aVHNgv9iqMxS.5Uy6b3euAQD5/0.URGZ19MqSDtbTtxEYqFXUBy." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 6, 13, 56, 13, 774, DateTimeKind.Utc).AddTicks(7520), "$2a$11$sJ67ICZvi3cBCwcKWFkuheGtc4wdhTVcH91MRMTy3Y25XSb8FE2z2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 6, 13, 56, 13, 936, DateTimeKind.Utc).AddTicks(2223), "$2a$11$gWPIEqxzwW/Zv6aBC3j1A.j9KvJ6e5h1wNYUQhRwfbitbOdkG0lZ." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 6, 13, 56, 14, 107, DateTimeKind.Utc).AddTicks(6576), "$2a$11$ad5ZGPTMr2pv5QfVVWnkaeHrzJtd3FtLNiZgyFOGGsUqWrLaEI.sa" });
        }
    }
}
