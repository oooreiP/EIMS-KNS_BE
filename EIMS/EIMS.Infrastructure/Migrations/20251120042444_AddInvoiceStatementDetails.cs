using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoiceStatementDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceStatementDetails",
                columns: table => new
                {
                    DetailID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatementID = table.Column<int>(type: "integer", nullable: false),
                    InvoiceID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceStatementDetails", x => x.DetailID);
                    table.ForeignKey(
                        name: "FK_InvoiceStatementDetails_InvoiceStatements_StatementID",
                        column: x => x.StatementID,
                        principalTable: "InvoiceStatements",
                        principalColumn: "StatementID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceStatementDetails_Invoices_InvoiceID",
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
                value: new DateTime(2025, 11, 20, 4, 24, 39, 216, DateTimeKind.Utc).AddTicks(5356));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 20, 4, 24, 39, 216, DateTimeKind.Utc).AddTicks(5366));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 20, 4, 24, 39, 216, DateTimeKind.Utc).AddTicks(5370));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 20, 4, 24, 39, 216, DateTimeKind.Utc).AddTicks(5452));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 20, 4, 24, 39, 216, DateTimeKind.Utc).AddTicks(5457));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 20, 4, 24, 39, 216, DateTimeKind.Utc).AddTicks(5461));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 20, 4, 24, 39, 440, DateTimeKind.Utc).AddTicks(1089), "$2a$11$y29PtaszoeesOT.kKjqTXuPO2Gb0q3LtlWD399aIlon8EmEsyCEyS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 20, 4, 24, 39, 634, DateTimeKind.Utc).AddTicks(959), "$2a$11$vBtd5t.FCr9nV8/TIZg0PeFKMWWnS/1OwoseU5mUOAO1e/qoVJnT2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 20, 4, 24, 39, 827, DateTimeKind.Utc).AddTicks(9623), "$2a$11$wLbvDIyMbQXYnAg4IJIBbOFopmdx2Wn7W5wgrfEEk0bH78fGAVrQO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 20, 4, 24, 40, 22, DateTimeKind.Utc).AddTicks(8274), "$2a$11$tQ9QQ0dElvMz9PJjVQtol.ToT1DSy6Zk0T6t11gduBktIcJidFjuW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 20, 4, 24, 40, 217, DateTimeKind.Utc).AddTicks(9690), "$2a$11$.rC/BlHQUeHmBWoIx/V2SuQwleKb161rUUorVw9hD2oQf.3OdT3bq" });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceStatementDetails_InvoiceID",
                table: "InvoiceStatementDetails",
                column: "InvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceStatementDetails_StatementID",
                table: "InvoiceStatementDetails",
                column: "StatementID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceStatementDetails");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 19, 7, 28, 36, 393, DateTimeKind.Utc).AddTicks(9049));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 19, 7, 28, 36, 393, DateTimeKind.Utc).AddTicks(9058));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 19, 7, 28, 36, 393, DateTimeKind.Utc).AddTicks(9061));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 19, 7, 28, 36, 393, DateTimeKind.Utc).AddTicks(9136));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 19, 7, 28, 36, 393, DateTimeKind.Utc).AddTicks(9141));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 19, 7, 28, 36, 393, DateTimeKind.Utc).AddTicks(9145));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 19, 7, 28, 36, 617, DateTimeKind.Utc).AddTicks(380), "$2a$11$.0/nXX9HaZ1poXkfzCy8HOW4n55EhWJqEfB48j6EifWTHG8xaSDfa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 19, 7, 28, 36, 814, DateTimeKind.Utc).AddTicks(7568), "$2a$11$Pwt1l/c38qyM2QXOCBPtiOjrdrqS1pv4YzB7xMrjXQI3qBX76uwtm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 19, 7, 28, 37, 8, DateTimeKind.Utc).AddTicks(2443), "$2a$11$5l.nfUGKMyNXoQBcVvwUiurHkLnYQQA1FpqakxlM0nnJH0P9racMS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 19, 7, 28, 37, 209, DateTimeKind.Utc).AddTicks(4112), "$2a$11$VYQ3/yQjBt5YQogDlG2yQe/OCzMstC7U4h52/U8Oq3mO/VbVadu1W" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 19, 7, 28, 37, 415, DateTimeKind.Utc).AddTicks(4562), "$2a$11$2E4594PudZRyAlqqRPFGQOReXdMoS.JeK81RW3bGrssUXM3MT0HKi" });
        }
    }
}
