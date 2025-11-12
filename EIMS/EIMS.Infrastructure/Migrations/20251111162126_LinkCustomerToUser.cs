using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LinkCustomerToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerID",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 11, 16, 21, 21, 649, DateTimeKind.Utc).AddTicks(8423));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 11, 16, 21, 21, 649, DateTimeKind.Utc).AddTicks(8434));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 11, 16, 21, 21, 649, DateTimeKind.Utc).AddTicks(8438));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 11, 16, 21, 21, 649, DateTimeKind.Utc).AddTicks(8494));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 11, 16, 21, 21, 649, DateTimeKind.Utc).AddTicks(8499));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 11, 16, 21, 21, 649, DateTimeKind.Utc).AddTicks(8507));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "CustomerID", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 11, 16, 21, 21, 833, DateTimeKind.Utc).AddTicks(5561), null, "$2a$11$qoBm9IAkEJ170.TDILcfmOGQ24.FZVTp2dSJsM1Wqn8ELjCC3gF7G" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "CustomerID", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 11, 16, 21, 22, 9, DateTimeKind.Utc).AddTicks(1768), null, "$2a$11$Zi4X58fJb1AzuTxsDilRd.rszQ8n7wN35ScDo1k.h3ztGW1pwgk9q" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "CustomerID", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 11, 16, 21, 22, 181, DateTimeKind.Utc).AddTicks(3897), null, "$2a$11$MuVfv6jknBFkqwe6W.a49OVEnypyltwjZGrUBZB/mnVsAnTGptDZe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "CustomerID", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 11, 16, 21, 22, 348, DateTimeKind.Utc).AddTicks(5162), null, "$2a$11$lcwOqiRuLntAqESgYGcuGeLhT.t39OBrhH5Ub5s9LG50bEOGMMXJG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "CustomerID", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 11, 16, 21, 22, 516, DateTimeKind.Utc).AddTicks(5598), null, "$2a$11$/v/cblDed11BVBMKHRoDYOOohyL01YLwl5SA44kLrVQHWxd5h9ne." });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CustomerID",
                table: "Users",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Customers_CustomerID",
                table: "Users",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Customers_CustomerID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CustomerID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "Users");

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
        }
    }
}
