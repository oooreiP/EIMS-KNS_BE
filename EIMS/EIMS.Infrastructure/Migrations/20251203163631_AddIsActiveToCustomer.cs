using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveToCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Customers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 3, 16, 36, 29, 373, DateTimeKind.Utc).AddTicks(3612));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 3, 16, 36, 29, 373, DateTimeKind.Utc).AddTicks(3630));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 3, 16, 36, 29, 373, DateTimeKind.Utc).AddTicks(3633));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 3, 16, 36, 29, 373, DateTimeKind.Utc).AddTicks(3769));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 3, 16, 36, 29, 373, DateTimeKind.Utc).AddTicks(3773));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 3, 16, 36, 29, 373, DateTimeKind.Utc).AddTicks(3782));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 3, 16, 36, 29, 589, DateTimeKind.Utc).AddTicks(6491), "$2a$11$hyU4BiORa9f2GIetZvmKNuRO9qA1BtOApx8iBVTskXGPQg0Occ8iu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 3, 16, 36, 29, 804, DateTimeKind.Utc).AddTicks(533), "$2a$11$A1KBp8IgME.Uvk1pNqa7BeCcJNZkeaBYQB7PrqvZqfqpFujNsO9F2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 3, 16, 36, 29, 992, DateTimeKind.Utc).AddTicks(4866), "$2a$11$WRsqZgNNktn4coSowfks6OVmE3U9oUCl1.TLqejrTl7mUaZOUpVxG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 3, 16, 36, 30, 221, DateTimeKind.Utc).AddTicks(6433), "$2a$11$Hce5v8zkjwJ4r52Wxhz93.i3oywCzdOoyA01BhqTFb9OO8BrtxOqW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 3, 16, 36, 30, 418, DateTimeKind.Utc).AddTicks(7329), "$2a$11$aZ6oH6aqc2Wanlz8nOiRmefhdvGrSketRUuuA04zda/TRKXlnRecC" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Customers");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 3, 15, 6, 32, 461, DateTimeKind.Utc).AddTicks(8060));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 3, 15, 6, 32, 461, DateTimeKind.Utc).AddTicks(8085));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 3, 15, 6, 32, 461, DateTimeKind.Utc).AddTicks(8088));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 3, 15, 6, 32, 461, DateTimeKind.Utc).AddTicks(8227));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 3, 15, 6, 32, 461, DateTimeKind.Utc).AddTicks(8232));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 3, 15, 6, 32, 461, DateTimeKind.Utc).AddTicks(8239));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 3, 15, 6, 32, 657, DateTimeKind.Utc).AddTicks(1197), "$2a$11$tlBn9eiCpciICMDBk4P8W.EtZUJd9/I44msItWUUqsTVX05drcoU6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 3, 15, 6, 32, 819, DateTimeKind.Utc).AddTicks(412), "$2a$11$QYor6MZ59LNfFMNwrRi3yu5WbrXFuSWA8pXCDkoFaxgTWUjU2BES6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 3, 15, 6, 32, 992, DateTimeKind.Utc).AddTicks(2431), "$2a$11$Epvt67oisQtjrXHbSiUD7O9Br16A7sm32MJelfwc3DzzImR3f4U0e" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 3, 15, 6, 33, 164, DateTimeKind.Utc).AddTicks(2201), "$2a$11$GldopbS4h8cxB8MrcyxWquW1z9LaLAesmxO.xiy7VsuOeaXBDx7Hq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 3, 15, 6, 33, 368, DateTimeKind.Utc).AddTicks(1586), "$2a$11$GC8ik5vPEFYBIYEy3XRd.uHHGYlEm3g1BEkNFXWdFZ/PyBhgCDZsG" });
        }
    }
}
