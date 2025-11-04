using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedPrefixData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 7, 15, 24, 703, DateTimeKind.Utc).AddTicks(3241));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 7, 15, 24, 703, DateTimeKind.Utc).AddTicks(3251));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 7, 15, 24, 703, DateTimeKind.Utc).AddTicks(3254));

            migrationBuilder.InsertData(
                table: "Prefixes",
                columns: new[] { "PrefixID", "PrefixName" },
                values: new object[,]
                {
                    { 1, "Hóa đơn điện tử giá trị gia tăng" },
                    { 2, "Hóa đơn điện tử bán hàng" },
                    { 3, "Hóa đơn điện tử bán tài sản công" },
                    { 4, "Hóa đơn điện tử bán hàng dự trữ quốc gia" },
                    { 5, "Hóa đơn điện tử khác là tem điện tử, vé điện tử, thẻ điện tử, phiếu thu điện tử hoặc các chứng từ điện tử có tên gọi khác nhưng có nội dung của hóa đơn điện tử theo quy định tại Nghị định số 123/2020/NĐ-CP" },
                    { 6, "Chứng từ điện tử được sử dụng và quản lý như hóa đơn gồm phiếu xuất kho kiêm vận chuyển nội bộ điện tử, phiếu xuất kho hàng gửi bán đại lý điện tử" },
                    { 7, "Hóa đơn thương mại điện tử" },
                    { 8, "Hóa đơn giá trị gia tăng tích hợp biên lai thu thuế, phí, lệ phí" },
                    { 9, "Hóa đơn bán hàng tích hợp biên lai thu thuế, phí, lệ phí" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 7, 15, 24, 703, DateTimeKind.Utc).AddTicks(3300));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 7, 15, 24, 703, DateTimeKind.Utc).AddTicks(3304));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 7, 15, 24, 703, DateTimeKind.Utc).AddTicks(3307));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 7, 15, 24, 861, DateTimeKind.Utc).AddTicks(4196), "$2a$11$R.MvLRchvT8l4vpG6.S.uuaTEoWPDHL1FAAumi8ITZjGjHBtgFnpm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 7, 15, 25, 16, DateTimeKind.Utc).AddTicks(6730), "$2a$11$hq01q1KpTKHfpcbBVd4b3ekNVLErBu8yXGHmYqnf46gD7nSXsS9Qq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 7, 15, 25, 172, DateTimeKind.Utc).AddTicks(4934), "$2a$11$7sY8rQnZL5BhxXKW3cxZzOIwkJw9FnLUMr7KYN/8RNXNLJf46MP7G" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 7, 15, 25, 335, DateTimeKind.Utc).AddTicks(8006), "$2a$11$aQfhrt8w6z1xvcoVspVh8uT6W0alH3NZOXXH3RfX2ZD2oEkDKz9V." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 7, 15, 25, 496, DateTimeKind.Utc).AddTicks(4345), "$2a$11$WmdeeRX.5TB921UIQKj11.uastXkUIbjMHEjsnSPlBAWA27lee/Tm" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Prefixes",
                keyColumn: "PrefixID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Prefixes",
                keyColumn: "PrefixID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Prefixes",
                keyColumn: "PrefixID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Prefixes",
                keyColumn: "PrefixID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Prefixes",
                keyColumn: "PrefixID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Prefixes",
                keyColumn: "PrefixID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Prefixes",
                keyColumn: "PrefixID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Prefixes",
                keyColumn: "PrefixID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Prefixes",
                keyColumn: "PrefixID",
                keyValue: 9);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 3, 13, 18, 51, 717, DateTimeKind.Utc).AddTicks(7703));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 3, 13, 18, 51, 717, DateTimeKind.Utc).AddTicks(7712));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 3, 13, 18, 51, 717, DateTimeKind.Utc).AddTicks(7715));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 3, 13, 18, 51, 717, DateTimeKind.Utc).AddTicks(7768));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 3, 13, 18, 51, 717, DateTimeKind.Utc).AddTicks(7772));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 3, 13, 18, 51, 717, DateTimeKind.Utc).AddTicks(7776));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 13, 18, 51, 872, DateTimeKind.Utc).AddTicks(8362), "$2a$11$6GjLKMjl5cfgGMbt87fDDOZ50uca.4U3bVT/magt9phA2TsFypzBS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 13, 18, 52, 27, DateTimeKind.Utc).AddTicks(239), "$2a$11$ujZLLqB8SuSotFetUSh7yOxY.NA6uNzelPVdWAiiN7SzLey39TZeO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 13, 18, 52, 184, DateTimeKind.Utc).AddTicks(7), "$2a$11$bnsLs4Pefx1YMOc9aBTtnuC36/b/8qolUchrZoXQk57yrUMcUgDVW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 13, 18, 52, 341, DateTimeKind.Utc).AddTicks(293), "$2a$11$z8UAxmxLHSJ7gXwIN/sDFuziTw.dhPfo3EnsyRykK/hzyJ8riXYv." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 13, 18, 52, 496, DateTimeKind.Utc).AddTicks(9111), "$2a$11$nrQDuD9NBYm/rDFOm8PE3OpIYH8lw2I6umt/R/YDHYplRMx9QJoJ6" });
        }
    }
}
