using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixStatementFK2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 7, 56, 51, 893, DateTimeKind.Utc).AddTicks(7849));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 7, 56, 51, 893, DateTimeKind.Utc).AddTicks(7855));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 7, 56, 51, 893, DateTimeKind.Utc).AddTicks(7858));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 7, 56, 51, 893, DateTimeKind.Utc).AddTicks(7965));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 7, 56, 51, 893, DateTimeKind.Utc).AddTicks(7968));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 7, 56, 51, 893, DateTimeKind.Utc).AddTicks(7971));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 7, 56, 52, 9, DateTimeKind.Utc).AddTicks(1075), "$2a$11$nvsEd5oXXm6Sa02SKL1zr.M7KFipfAkXwBTNSAN7bYYeqQ3FggIhu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 7, 56, 52, 126, DateTimeKind.Utc).AddTicks(8947), "$2a$11$iQUi3JlHBGnMgBvN/cSfuOTuxmRpjsubxIcPsvkZBvFlJf2on7zqS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 7, 56, 52, 244, DateTimeKind.Utc).AddTicks(2427), "$2a$11$RTJZvZBEk8g9stp6ZnDsBOCl2LqjiEwjgG21eVYimfKc6Ky8AybQG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 7, 56, 52, 364, DateTimeKind.Utc).AddTicks(1868), "$2a$11$.H9Q7kQSLJrA9LX1DLp6beTbao0CtWEWjiYtQIV/yq8cW56P.Ts.O" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 7, 56, 52, 481, DateTimeKind.Utc).AddTicks(1960), "$2a$11$A3LR743mnhxl2ATKu8KNzuCnn4oMA3hRDuCVOLAXBAwsEJaE4ISMO" });
        }
    }
}
