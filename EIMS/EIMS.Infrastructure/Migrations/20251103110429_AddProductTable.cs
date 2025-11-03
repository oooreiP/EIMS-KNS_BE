using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductTable : Migration
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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 11, 4, 28, 514, DateTimeKind.Utc).AddTicks(3057), "$2a$11$b1cEWzZ7kNbM3XChtTEArekniZl3ZeywIxCT/z2gZv.obwG6kq8RC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 11, 4, 28, 682, DateTimeKind.Utc).AddTicks(6404), "$2a$11$RqJ3bhXMeM1nV/h9UDhjIucoaQ0p2tbG/MkQ.dAwcjiYY.KaIhTQW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 11, 4, 28, 844, DateTimeKind.Utc).AddTicks(8329), "$2a$11$4br/8T89v/wZfyKdLSNpkOw4W99MZ.78hkazdjr.VXAhR2mxbYpCq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 11, 4, 29, 3, DateTimeKind.Utc).AddTicks(2539), "$2a$11$ccpCmtWmwpwqJU6rNpI7Bergs8Vu8y.Liwue7D4/mm6eRY9P0p/Bq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 11, 4, 29, 167, DateTimeKind.Utc).AddTicks(9684), "$2a$11$elHI.Rf53mcHoJI/zCSJSuB29j0zOUkRlz/arKljPUE2ESG5WfVEe" });

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
