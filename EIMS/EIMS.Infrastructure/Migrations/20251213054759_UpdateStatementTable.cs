using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStatementTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceStatements_Customers_CustomerID",
                table: "InvoiceStatements");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "InvoiceStatements",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StatementDate",
                table: "InvoiceStatements",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "InvoiceStatements",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "InvoiceStatements",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "PaidAmount",
                table: "InvoiceStatements",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 5, 47, 56, 696, DateTimeKind.Utc).AddTicks(6105));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 5, 47, 56, 696, DateTimeKind.Utc).AddTicks(6112));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 5, 47, 56, 696, DateTimeKind.Utc).AddTicks(6114));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 5, 47, 56, 696, DateTimeKind.Utc).AddTicks(6168));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 5, 47, 56, 696, DateTimeKind.Utc).AddTicks(6171));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 5, 47, 56, 696, DateTimeKind.Utc).AddTicks(6174));

            migrationBuilder.UpdateData(
                table: "StatementStatuses",
                keyColumn: "StatusID",
                keyValue: 2,
                column: "StatusName",
                value: "Published");

            migrationBuilder.UpdateData(
                table: "StatementStatuses",
                keyColumn: "StatusID",
                keyValue: 3,
                column: "StatusName",
                value: "Sent");

            migrationBuilder.UpdateData(
                table: "StatementStatuses",
                keyColumn: "StatusID",
                keyValue: 4,
                column: "StatusName",
                value: "Partially Paid");

            migrationBuilder.InsertData(
                table: "StatementStatuses",
                columns: new[] { "StatusID", "StatusName" },
                values: new object[,]
                {
                    { 5, "Paid" },
                    { 6, "Cancelled" },
                    { 7, "Refunded" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 5, 47, 56, 813, DateTimeKind.Utc).AddTicks(3992), "$2a$11$oxFrnJF8huMQk3cAMYoM0OwvpqE7Szzc07qRPRUGHjgZSVCUmd11a" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 5, 47, 56, 930, DateTimeKind.Utc).AddTicks(658), "$2a$11$BasiRD0/eGBTDl3mgBmlH.od7IKrTRFxFccvydwKQjsuSJJ.QuJPy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 5, 47, 57, 48, DateTimeKind.Utc).AddTicks(1484), "$2a$11$G6S87Sr/C1LjSmszBOktOeUWbLINwEqcRw1rY5lyct6OpN22hLd5u" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 5, 47, 57, 161, DateTimeKind.Utc).AddTicks(2911), "$2a$11$6vvnybE8bCtKvi5F3ZcDTexbkDvIB0IPdSqX2saVdjL2nfIxnEnGC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 5, 47, 57, 275, DateTimeKind.Utc).AddTicks(4537), "$2a$11$E62QsZqZTqW4pAHGSjgvnurLK.i293ZkMXffU2itYTdR1tYklpUNK" });

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceStatements_Customers_CustomerID",
                table: "InvoiceStatements",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceStatements_Customers_CustomerID",
                table: "InvoiceStatements");

            migrationBuilder.DeleteData(
                table: "StatementStatuses",
                keyColumn: "StatusID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "StatementStatuses",
                keyColumn: "StatusID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "StatementStatuses",
                keyColumn: "StatusID",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "InvoiceStatements");

            migrationBuilder.DropColumn(
                name: "PaidAmount",
                table: "InvoiceStatements");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "InvoiceStatements",
                type: "numeric(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StatementDate",
                table: "InvoiceStatements",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "InvoiceStatements",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 10, 14, 53, 3, 281, DateTimeKind.Utc).AddTicks(5572));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 10, 14, 53, 3, 281, DateTimeKind.Utc).AddTicks(5579));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 10, 14, 53, 3, 281, DateTimeKind.Utc).AddTicks(5581));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 10, 14, 53, 3, 281, DateTimeKind.Utc).AddTicks(5796));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 10, 14, 53, 3, 281, DateTimeKind.Utc).AddTicks(5799));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 10, 14, 53, 3, 281, DateTimeKind.Utc).AddTicks(5803));

            migrationBuilder.UpdateData(
                table: "StatementStatuses",
                keyColumn: "StatusID",
                keyValue: 2,
                column: "StatusName",
                value: "Sent");

            migrationBuilder.UpdateData(
                table: "StatementStatuses",
                keyColumn: "StatusID",
                keyValue: 3,
                column: "StatusName",
                value: "Paid");

            migrationBuilder.UpdateData(
                table: "StatementStatuses",
                keyColumn: "StatusID",
                keyValue: 4,
                column: "StatusName",
                value: "Overdue");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 53, 3, 513, DateTimeKind.Utc).AddTicks(1424), "$2a$11$gnlLYqwf4RDOoaFxrpP20uaRQ5FVlphReIOv2Iwo7a3A7nAK2yeKy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 53, 3, 744, DateTimeKind.Utc).AddTicks(2539), "$2a$11$xKQYCOFsV7WhvdmBhXPEoOKYy51XdAZMPsGnMCm62dB0Pg4RzUfoC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 53, 3, 950, DateTimeKind.Utc).AddTicks(8564), "$2a$11$u/Jlwf86qcU9ZmiYjd2JbeorTIcemrq5fSGJldzMHVnIiz0r59Kny" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 53, 4, 196, DateTimeKind.Utc).AddTicks(2183), "$2a$11$7W8n3W2OCy/wz10jr/GutO3476vH3jm8HwdL6bJA3HSGPO96aBcTK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 53, 4, 403, DateTimeKind.Utc).AddTicks(8000), "$2a$11$Totul.QxNhvOP7tGZeJ3t.EVzpyfmqL6Y0XTlO/L8SO6eR07SdqKy" });

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceStatements_Customers_CustomerID",
                table: "InvoiceStatements",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID");
        }
    }
}
