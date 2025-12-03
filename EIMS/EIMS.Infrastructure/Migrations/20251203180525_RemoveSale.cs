using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                value: new DateTime(2025, 12, 3, 18, 5, 21, 680, DateTimeKind.Utc).AddTicks(1762));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 3, 18, 5, 21, 680, DateTimeKind.Utc).AddTicks(1770));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 3, 18, 5, 21, 680, DateTimeKind.Utc).AddTicks(1774));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 3, 18, 5, 21, 680, DateTimeKind.Utc).AddTicks(1832));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 3, 18, 5, 21, 680, DateTimeKind.Utc).AddTicks(1837));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 3, 18, 5, 21, 680, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 3, 18, 5, 21, 835, DateTimeKind.Utc).AddTicks(6483), "$2a$11$WI7cLDSpLfXClaxjaigiH.4sV5Fsjhf310m6.CnmUHcYb1Yn72tZC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 3, 18, 5, 21, 989, DateTimeKind.Utc).AddTicks(8040), "$2a$11$7vppO1ulOcAXfytXsQEczu7dprRXwJ0oUh8YW7T6GKFkCItDDEp.C" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 3, 18, 5, 22, 146, DateTimeKind.Utc).AddTicks(7189), "$2a$11$1xHsMT3yP8tPEInHw4ssw.2oRl0UX7V8kHdrCV0UAz4weAKsWmbOe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 3, 18, 5, 22, 304, DateTimeKind.Utc).AddTicks(9525), "$2a$11$VCLxPG6DX76Z0EQD2SsgruvI5rY7iHYP79gApa4CqWhIlvVKgwkeW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 3, 18, 5, 22, 459, DateTimeKind.Utc).AddTicks(4985), "$2a$11$HflCSavsxtB6Wx/5GK0Y1uzk4hOo88bD5xZNNeHoPYHHnbeP25u2S" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
