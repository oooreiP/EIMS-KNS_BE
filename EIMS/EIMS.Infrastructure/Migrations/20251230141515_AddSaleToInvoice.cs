using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSaleToInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalesID",
                table: "Invoices",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 30, 14, 15, 11, 112, DateTimeKind.Utc).AddTicks(9107));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 30, 14, 15, 11, 112, DateTimeKind.Utc).AddTicks(9126));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 30, 14, 15, 11, 112, DateTimeKind.Utc).AddTicks(9129));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 30, 14, 15, 11, 112, DateTimeKind.Utc).AddTicks(9258));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 30, 14, 15, 11, 112, DateTimeKind.Utc).AddTicks(9263));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 30, 14, 15, 11, 112, DateTimeKind.Utc).AddTicks(9266));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 30, 14, 15, 11, 294, DateTimeKind.Utc).AddTicks(160), "$2a$11$evbsCtbp7em2F/P8.IxgYu.b7y43o0zPKOjZSzcI5V8gOmsI/u2M." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 30, 14, 15, 11, 472, DateTimeKind.Utc).AddTicks(6516), "$2a$11$CxUOuTbAVL5UqDr8TmrLUOTBeBzueZDfJC/CyS42ld7mvDgCk7LPG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 30, 14, 15, 11, 663, DateTimeKind.Utc).AddTicks(1481), "$2a$11$yo3GbPBGyLBKUt4b5xp8luMYRArPPeieukCl9jJJtktfdTJZ/Ytyu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 30, 14, 15, 11, 861, DateTimeKind.Utc).AddTicks(7413), "$2a$11$inbNnnWHv69/PoE7in87/uJrpEwNzcAVvY7FUEr7xMIppH34T5Spe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 30, 14, 15, 12, 47, DateTimeKind.Utc).AddTicks(4602), "$2a$11$G14ruG7VqTQKQr8X9H.4IebPSvG4IhBzVPEUgyi10s9GhfI2VK4om" });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_SalesID",
                table: "Invoices",
                column: "SalesID");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Users_SalesID",
                table: "Invoices",
                column: "SalesID",
                principalTable: "Users",
                principalColumn: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Users_SalesID",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_SalesID",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "SalesID",
                table: "Invoices");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 17, 42, 9, 756, DateTimeKind.Utc).AddTicks(9792));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 17, 42, 9, 756, DateTimeKind.Utc).AddTicks(9841));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 17, 42, 9, 756, DateTimeKind.Utc).AddTicks(9850));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 17, 42, 9, 757, DateTimeKind.Utc).AddTicks(13));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 17, 42, 9, 757, DateTimeKind.Utc).AddTicks(19));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 17, 42, 9, 757, DateTimeKind.Utc).AddTicks(22));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 29, 17, 42, 10, 6, DateTimeKind.Utc).AddTicks(5773), "$2a$11$qdeoUe.tgFasJjPuoy26Qe9bUNQ7CHyj1MDYAyFmAp4qt4V2qY.5y" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 29, 17, 42, 10, 157, DateTimeKind.Utc).AddTicks(8124), "$2a$11$x3gyTcHL1kNvHzIz8RQ2I.B4rjRO8C1hKJU5fk0eTXrEia/JExuIS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 29, 17, 42, 10, 308, DateTimeKind.Utc).AddTicks(55), "$2a$11$ERHwOXU13avD7WUUrTmCwup5/1EUUgWkCs9Wb7yZwb191wvsISXZa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 29, 17, 42, 10, 449, DateTimeKind.Utc).AddTicks(9268), "$2a$11$RU34FC3QPAcZ35zYnCwZJuKyYVVFt1pLUTCK9vOFYWi9cS4xxULzW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 29, 17, 42, 10, 588, DateTimeKind.Utc).AddTicks(3876), "$2a$11$lT8z8bHaUhuru/q5LO1oyu2zKrJLV6uRqlONtTccijW5x/L0FXHCa" });
        }
    }
}
