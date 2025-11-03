using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductTableAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CategoryID = table.Column<int>(type: "integer", nullable: false),
                    Unit = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    BasePrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    VATRate = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "CategoryType", "Code", "CreatedDate", "Description", "IsActive", "IsTaxable", "Name", "VATRate" },
                values: new object[,]
                {
                    { 1, "Goods", "HH", new DateTime(2025, 11, 3, 13, 18, 51, 717, DateTimeKind.Utc).AddTicks(7703), "Mặt hàng vật lý chịu thuế GTGT 10%", true, true, "Hàng hóa chịu thuế 10%", 10m },
                    { 2, "Service", "DV", new DateTime(2025, 11, 3, 13, 18, 51, 717, DateTimeKind.Utc).AddTicks(7712), "Dịch vụ lưu trữ, cho thuê máy chủ", true, true, "Dịch vụ chịu thuế 8%", 8m },
                    { 3, "Software", "SW", new DateTime(2025, 11, 3, 13, 18, 51, 717, DateTimeKind.Utc).AddTicks(7715), "Sản phẩm phần mềm và bản quyền", true, false, "Phần mềm không chịu thuế", 0m }
                });

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

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "BasePrice", "CategoryID", "Code", "CreatedDate", "Description", "IsActive", "Name", "Unit", "VATRate" },
                values: new object[,]
                {
                    { 1, 23000m, 1, "HH0001", new DateTime(2025, 11, 3, 13, 18, 51, 717, DateTimeKind.Utc).AddTicks(7768), "Xăng RON95 chịu thuế GTGT 10%", true, "Xăng RON95", "Lít", 10m },
                    { 2, 500000m, 2, "DV001", new DateTime(2025, 11, 3, 13, 18, 51, 717, DateTimeKind.Utc).AddTicks(7772), "Dịch vụ hosting thuế suất 8%", true, "Dịch vụ cho thuê máy chủ (Hosting)", "Tháng", 8m },
                    { 3, 10000000m, 3, "SW001", new DateTime(2025, 11, 3, 13, 18, 51, 717, DateTimeKind.Utc).AddTicks(7776), "Phần mềm không chịu thuế GTGT", true, "Phần mềm kế toán bản quyền", "Gói", 0m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

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
                values: new object[] { new DateTime(2025, 10, 29, 11, 53, 24, 185, DateTimeKind.Utc).AddTicks(863), "$2a$11$QIf1jRGnXSdTxLBzKczFX.0YVZ9slcpvM0Ald/OyIp7XUv7bGY0ma" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 10, 29, 11, 53, 24, 338, DateTimeKind.Utc).AddTicks(2086), "$2a$11$vQH4TOwkluaVtJQqW5h8R.B/9M0RrVEFVlht1C3S8hgwELgw..woy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 10, 29, 11, 53, 24, 491, DateTimeKind.Utc).AddTicks(3899), "$2a$11$cAxQQcWX5Or4sBD6gUk4GepekcJ2VDsO7mtGeJEpyivhnJ7q/y3tO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 10, 29, 11, 53, 24, 644, DateTimeKind.Utc).AddTicks(336), "$2a$11$XysmTrukGd0GqP7DEdVGwOaB4fQnJ1YGkh9JU1IG5FLSsuIl//kgm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 10, 29, 11, 53, 24, 797, DateTimeKind.Utc).AddTicks(1426), "$2a$11$gXUZCHekA/kCEH5I7vu/nep2dSI.XrcCXXcrchwejvM22/ydu6QX." });
        }
    }
}
