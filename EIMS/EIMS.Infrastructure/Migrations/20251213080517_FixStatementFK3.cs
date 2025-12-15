using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixStatementFK3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceStatements_Users_CreatorUserID",
                table: "InvoiceStatements");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceStatements_CreatorUserID",
                table: "InvoiceStatements");

            migrationBuilder.DropColumn(
                name: "CreatorUserID",
                table: "InvoiceStatements");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 8, 5, 16, 136, DateTimeKind.Utc).AddTicks(2996));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 8, 5, 16, 136, DateTimeKind.Utc).AddTicks(3005));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 8, 5, 16, 136, DateTimeKind.Utc).AddTicks(3007));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 8, 5, 16, 136, DateTimeKind.Utc).AddTicks(3053));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 8, 5, 16, 136, DateTimeKind.Utc).AddTicks(3056));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 8, 5, 16, 136, DateTimeKind.Utc).AddTicks(3059));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 8, 5, 16, 259, DateTimeKind.Utc).AddTicks(6071), "$2a$11$P21uJbmtSja/OvzCzf2Po.Pnodzz2RiB1CYYjE9WfcuPrc38Jh4rW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 8, 5, 16, 378, DateTimeKind.Utc).AddTicks(1635), "$2a$11$jrySRgCcoUPo.s0yrsTxRurRaHnL6fTfZ.UM9ywHgxiq1.zFSTD2C" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 8, 5, 16, 496, DateTimeKind.Utc).AddTicks(6011), "$2a$11$0iL5nd5Nu177pkkw3F0tWO4GhrTRTMMly.Gl9nnHTPzBSHWE3q2FK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 8, 5, 16, 611, DateTimeKind.Utc).AddTicks(5121), "$2a$11$.1HRoe0RFLpOQzdQJnnJm.D10AeFSrKNqCQgnwL8vQn8hiheAR3ja" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 8, 5, 16, 722, DateTimeKind.Utc).AddTicks(9085), "$2a$11$OYVfFo073OKjj9HJrI8Fj.EhTqyC7F.Rybz7j/hEq9avcRMKwne5W" });

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceStatements_Users_CustomerID",
                table: "InvoiceStatements",
                column: "CustomerID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceStatements_Users_CustomerID",
                table: "InvoiceStatements");

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserID",
                table: "InvoiceStatements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 8, 0, 36, 142, DateTimeKind.Utc).AddTicks(9751));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 8, 0, 36, 142, DateTimeKind.Utc).AddTicks(9762));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 8, 0, 36, 142, DateTimeKind.Utc).AddTicks(9765));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 8, 0, 36, 142, DateTimeKind.Utc).AddTicks(9858));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 8, 0, 36, 142, DateTimeKind.Utc).AddTicks(9862));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 8, 0, 36, 142, DateTimeKind.Utc).AddTicks(9868));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 8, 0, 36, 265, DateTimeKind.Utc).AddTicks(8653), "$2a$11$oKB6SB5fAicl8ikURRgVbOazQjmu7bfyNd3c78jojmQ0xM7mM.SE2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 8, 0, 36, 380, DateTimeKind.Utc).AddTicks(1889), "$2a$11$nNsgj2yy6gHLVblvKa6jROlBVqJByH.Dvi4OMjgERWgxiFLDerjEG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 8, 0, 36, 498, DateTimeKind.Utc).AddTicks(3180), "$2a$11$601tMPw7cU0dY.wgRGtbheN8w30z/u3YE7Frlb6r7SJuk6DRATxJG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 8, 0, 36, 616, DateTimeKind.Utc).AddTicks(7971), "$2a$11$tIrOXNpWMszURIsYjGF5K.70c/S85iXEEjVzvYXSDaD2Ra1YYd2aS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 8, 0, 36, 730, DateTimeKind.Utc).AddTicks(837), "$2a$11$seMkFVLespeOZH9nJVKbv.QnExqWY1Bj9D5Oov6IWGB2UGH5XCe3u" });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceStatements_CreatorUserID",
                table: "InvoiceStatements",
                column: "CreatorUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceStatements_Users_CreatorUserID",
                table: "InvoiceStatements",
                column: "CreatorUserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
