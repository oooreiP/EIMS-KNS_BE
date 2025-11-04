using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 3, 10, 44, 55, 901, DateTimeKind.Utc).AddTicks(6815));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 3, 10, 44, 55, 901, DateTimeKind.Utc).AddTicks(6822));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 3, 10, 44, 55, 901, DateTimeKind.Utc).AddTicks(6825));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 3, 10, 44, 55, 901, DateTimeKind.Utc).AddTicks(6874));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 3, 10, 44, 55, 901, DateTimeKind.Utc).AddTicks(6876));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 3, 10, 44, 55, 901, DateTimeKind.Utc).AddTicks(6879));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 10, 44, 56, 78, DateTimeKind.Utc).AddTicks(7887), "$2a$11$DcwzC1AMP02cpSUFIZGiEu47jxNRtrbv9Ih78T0qCEOYuicDixD5W" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 10, 44, 56, 247, DateTimeKind.Utc).AddTicks(5891), "$2a$11$6umhx7XCXyQmlIiJc3CMpudcmYl6doVhTaHoK1MwtymUCN237N2xi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 10, 44, 56, 426, DateTimeKind.Utc).AddTicks(3926), "$2a$11$YbTMiLLSxJHwCEFFt1Snrev0d7SmuxM/nug3xRZPQkLxAZQk1bazi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 10, 44, 56, 602, DateTimeKind.Utc).AddTicks(9630), "$2a$11$SgDERgavvgHt7Vp85DbGLOcizuOXJUqJRdoublqoWcWQox.cIdZh." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 10, 44, 56, 785, DateTimeKind.Utc).AddTicks(6911), "$2a$11$Fi1kbgOZrdRkGjP9nEWGauig8YaaqfG1U1G2vE.sKzwJwvEmihsnS" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 3, 10, 13, 53, 842, DateTimeKind.Utc).AddTicks(8234));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 3, 10, 13, 53, 842, DateTimeKind.Utc).AddTicks(8242));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 3, 10, 13, 53, 842, DateTimeKind.Utc).AddTicks(8245));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 3, 10, 13, 53, 842, DateTimeKind.Utc).AddTicks(8278));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 3, 10, 13, 53, 842, DateTimeKind.Utc).AddTicks(8282));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 3, 10, 13, 53, 842, DateTimeKind.Utc).AddTicks(8288));

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
        }
    }
}
