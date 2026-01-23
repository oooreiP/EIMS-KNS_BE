using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentStatement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatementPayments",
                columns: table => new
                {
                    StatementPaymentID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatementID = table.Column<int>(type: "integer", nullable: false),
                    PaymentID = table.Column<int>(type: "integer", nullable: false),
                    AppliedAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    AppliedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatementPayments", x => x.StatementPaymentID);
                    table.ForeignKey(
                        name: "FK_StatementPayments_InvoicePayment_PaymentID",
                        column: x => x.PaymentID,
                        principalTable: "InvoicePayment",
                        principalColumn: "PaymentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatementPayments_InvoiceStatements_StatementID",
                        column: x => x.StatementID,
                        principalTable: "InvoiceStatements",
                        principalColumn: "StatementID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 5, 41, 15, 100, DateTimeKind.Utc).AddTicks(4420));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 5, 41, 15, 100, DateTimeKind.Utc).AddTicks(4429));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 5, 41, 15, 100, DateTimeKind.Utc).AddTicks(4432));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 5, 41, 15, 898, DateTimeKind.Utc).AddTicks(6820));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 5, 41, 15, 898, DateTimeKind.Utc).AddTicks(6825));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 5, 41, 15, 898, DateTimeKind.Utc).AddTicks(6828));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 5, 41, 15, 898, DateTimeKind.Utc).AddTicks(6830));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 5, 41, 15, 898, DateTimeKind.Utc).AddTicks(6832));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 5, 41, 15, 898, DateTimeKind.Utc).AddTicks(6834));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 5, 41, 15, 100, DateTimeKind.Utc).AddTicks(4478));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 5, 41, 15, 100, DateTimeKind.Utc).AddTicks(4482));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 5, 41, 15, 100, DateTimeKind.Utc).AddTicks(4485));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 5, 41, 15, 257, DateTimeKind.Utc).AddTicks(8447), "$2a$11$/FNRWrBzlPxaA6y/3ay4WemPdQEuYbNDIAjmGk25vz00iHd9HQ/JK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 5, 41, 15, 410, DateTimeKind.Utc).AddTicks(5231), "$2a$11$B9TQeRHiYDoXxMxUx30xeOygF18mc7ngA.xGkivWXrJSxzt.Hf5wq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 5, 41, 15, 563, DateTimeKind.Utc).AddTicks(5523), "$2a$11$nKr2BIQBeKrv3/N2HMroeuMkArcQMtvga0HC3g1GyxXd5WMixKwGO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 5, 41, 15, 736, DateTimeKind.Utc).AddTicks(1251), "$2a$11$xiJlyIqGJ0OUC7SzcnSnn.MZtWsnDN4A91Qkq/n6CYFHQOx2KLdXW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 5, 41, 15, 895, DateTimeKind.Utc).AddTicks(4642), "$2a$11$MGOza/dnkcw6k7lVMbkBGOhXND/.14KHgCj1L0pj9N.scdsgxCuMS" });

            migrationBuilder.CreateIndex(
                name: "IX_StatementPayments_PaymentID",
                table: "StatementPayments",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_StatementPayments_StatementID",
                table: "StatementPayments",
                column: "StatementID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatementPayments");

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
        }
    }
}
