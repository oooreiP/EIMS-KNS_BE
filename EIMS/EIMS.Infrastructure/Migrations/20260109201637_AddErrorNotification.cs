using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddErrorNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 16, 33, 975, DateTimeKind.Utc).AddTicks(8749));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 16, 33, 975, DateTimeKind.Utc).AddTicks(8755));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 16, 33, 975, DateTimeKind.Utc).AddTicks(8758));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 9, 20, 16, 34, 535, DateTimeKind.Utc).AddTicks(9520));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 9, 20, 16, 34, 535, DateTimeKind.Utc).AddTicks(9524));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 9, 20, 16, 34, 535, DateTimeKind.Utc).AddTicks(9526));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 16, 33, 975, DateTimeKind.Utc).AddTicks(8785));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 16, 33, 975, DateTimeKind.Utc).AddTicks(8788));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 16, 33, 975, DateTimeKind.Utc).AddTicks(8790));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 16, 34, 93, DateTimeKind.Utc).AddTicks(183), "$2a$11$jjVjCbg/78TbIOXxdnfDpO1CpZijPuNR05Hjlt41yJMCUBN9xjiQS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 16, 34, 204, DateTimeKind.Utc).AddTicks(1808), "$2a$11$/N4icTjbl7aHC70/Luqu2urLDvqb.tzrtT7z6CM7gvOnAGTtyPImq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 16, 34, 313, DateTimeKind.Utc).AddTicks(3092), "$2a$11$H5ynqrmdt78u2n6q.nMh8uXnYeJfYkdpAZrTNGx2XKMY4HGxYS9di" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 16, 34, 424, DateTimeKind.Utc).AddTicks(5422), "$2a$11$Z98n67qv6UPny9cs8s.UcO/1Jae8xudXlZN90XmLFWrwKnHeWIVgm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 16, 34, 534, DateTimeKind.Utc).AddTicks(1852), "$2a$11$qDD7rnQ28GZtALvO7fEDUu0nkIUSrzeWwFNgytgrhjNxKkLJ5P5KC" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 8, 15, 56, 27, 274, DateTimeKind.Utc).AddTicks(694));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 8, 15, 56, 27, 274, DateTimeKind.Utc).AddTicks(700));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 8, 15, 56, 27, 274, DateTimeKind.Utc).AddTicks(704));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 8, 15, 56, 27, 855, DateTimeKind.Utc).AddTicks(3919));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 8, 15, 56, 27, 855, DateTimeKind.Utc).AddTicks(3923));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 8, 15, 56, 27, 855, DateTimeKind.Utc).AddTicks(3925));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 8, 15, 56, 27, 274, DateTimeKind.Utc).AddTicks(752));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 8, 15, 56, 27, 274, DateTimeKind.Utc).AddTicks(755));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 8, 15, 56, 27, 274, DateTimeKind.Utc).AddTicks(758));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 8, 15, 56, 27, 395, DateTimeKind.Utc).AddTicks(327), "$2a$11$tRi6dKvXJqtoIYyWMw2UOu45YxvjOVGRED4acGP3s1zR.XJN51r3C" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 8, 15, 56, 27, 513, DateTimeKind.Utc).AddTicks(7063), "$2a$11$LI0zNwHgAOrJsKlZKVMFf.D2YU.xm5Tg7tl/Fvocu2dYXFco9A9Ni" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 8, 15, 56, 27, 629, DateTimeKind.Utc).AddTicks(79), "$2a$11$iT63MINGeZTSEOUv0J2IVOZumLEuTxNrMJj/CsJdJnt2btwhIBr8i" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 8, 15, 56, 27, 741, DateTimeKind.Utc).AddTicks(4280), "$2a$11$q3U9foz/qE07AchcvCFxEO4/Puz7nVxaNfIaNtmlXdm.9ynqtxReG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 8, 15, 56, 27, 852, DateTimeKind.Utc).AddTicks(9841), "$2a$11$SyPc9JRpsGZLryDvOM0N.uT5xp75E0r34ul/AgZ74QsSeJprfo14K" });
        }
    }
}
