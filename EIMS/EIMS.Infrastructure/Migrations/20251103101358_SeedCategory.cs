using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "CategoryType", "Code", "CreatedDate", "Description", "IsActive", "IsTaxable", "Name", "VATRate" },
                values: new object[,]
                {
                    { 1, "Goods", "HH", new DateTime(2025, 11, 3, 10, 13, 53, 842, DateTimeKind.Utc).AddTicks(8234), "Mặt hàng vật lý chịu thuế GTGT 10%", true, true, "Hàng hóa chịu thuế 10%", 10m },
                    { 2, "Service", "DV", new DateTime(2025, 11, 3, 10, 13, 53, 842, DateTimeKind.Utc).AddTicks(8242), "Dịch vụ lưu trữ, cho thuê máy chủ", true, true, "Dịch vụ chịu thuế 8%", 8m },
                    { 3, "Software", "SW", new DateTime(2025, 11, 3, 10, 13, 53, 842, DateTimeKind.Utc).AddTicks(8245), "Sản phẩm phần mềm và bản quyền", true, false, "Phần mềm không chịu thuế", 0m }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 10, 13, 53, 952, DateTimeKind.Utc).AddTicks(2219), "$2a$11$hU/BJYme/jv5IkLrluysjudSxYKy1x.iIQlI6CgQxU4UlC.JDTogC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 10, 13, 54, 63, DateTimeKind.Utc).AddTicks(5433), "$2a$11$PitfAT6Ws9sKxn5Wj3QNw.p0nG4sfSLESqKTU40yPU4FJ/zUcXu12" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 10, 13, 54, 175, DateTimeKind.Utc).AddTicks(5028), "$2a$11$JA9YU5LfRQcZty/HTevht.zFLxSrznCvPN2HtNrfIrWfLI99n.k1i" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 10, 13, 54, 288, DateTimeKind.Utc).AddTicks(1461), "$2a$11$RSOVIWkDoHkHrmy9jtyfLOIIfnV7vusV5yDbApfahLl8D1J7RyRi." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 10, 13, 54, 398, DateTimeKind.Utc).AddTicks(3129), "$2a$11$uhaUFhZS55C3W7lQvq3f3OMM5cuWm9hyv5CeaI7lk58vJ20N8INKS" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "BasePrice", "CategoryID", "Code", "CreatedDate", "Description", "IsActive", "Name", "Unit", "VATRate" },
                values: new object[,]
                {
                    { 1, 23000m, 1, "HH0001", new DateTime(2025, 11, 3, 10, 13, 53, 842, DateTimeKind.Utc).AddTicks(8278), "Xăng RON95 chịu thuế GTGT 10%", true, "Xăng RON95", "Lít", 10m },
                    { 2, 500000m, 2, "DV001", new DateTime(2025, 11, 3, 10, 13, 53, 842, DateTimeKind.Utc).AddTicks(8282), "Dịch vụ hosting thuế suất 8%", true, "Dịch vụ cho thuê máy chủ (Hosting)", "Tháng", 8m },
                    { 3, 10000000m, 3, "SW001", new DateTime(2025, 11, 3, 10, 13, 53, 842, DateTimeKind.Utc).AddTicks(8288), "Phần mềm không chịu thuế GTGT", true, "Phần mềm kế toán bản quyền", "Gói", 0m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 8, 43, 33, 731, DateTimeKind.Utc).AddTicks(9985), "$2a$11$U/LNM4DL8czDLmSx3K.r..98AJWtFR2zHylzBVa/Jb7X2YybyJyLG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 8, 43, 33, 846, DateTimeKind.Utc).AddTicks(6114), "$2a$11$oRwE6gYGqMXXiDyJ.PRvOeic9bycZ5R6La0CgGuoBDT/YJO.uLgc." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 8, 43, 33, 963, DateTimeKind.Utc).AddTicks(5941), "$2a$11$bp/Jw36m/yVUfJGdJEEbx.KHeDoXarUYe33Mt4XHRcsShqyStZQcO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 8, 43, 34, 76, DateTimeKind.Utc).AddTicks(9475), "$2a$11$HtoBsCypVoTh8HPsi0h9jOAzRE3VkRKhHqx2LiZQixKeOXnjuitv6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 8, 43, 34, 191, DateTimeKind.Utc).AddTicks(8146), "$2a$11$M18H7yUlXjOCbwURNraYaeZ3g/872jlAM6/GDmS6pI2ApdNrNlmri" });
        }
    }
}
