using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixNameTemplateType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceTypeID",
                table: "InvoiceTemplates");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 5, 30, 1, 699, DateTimeKind.Utc).AddTicks(5429));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 5, 30, 1, 699, DateTimeKind.Utc).AddTicks(5441));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 5, 30, 1, 699, DateTimeKind.Utc).AddTicks(5444));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 5, 30, 1, 699, DateTimeKind.Utc).AddTicks(5486));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 5, 30, 1, 699, DateTimeKind.Utc).AddTicks(5490));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 5, 30, 1, 699, DateTimeKind.Utc).AddTicks(5493));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 5, 30, 1, 860, DateTimeKind.Utc).AddTicks(9547), "$2a$11$IdOUIcQXzH8S7i1GjwhdEeVACPIaWV1dXzZxZofa8TCLySKDXIuBS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 5, 30, 2, 17, DateTimeKind.Utc).AddTicks(8884), "$2a$11$uAhTx/vGVfGZoi2CNEUMQ.N99YN4NdxPhLtmFoAFje12PirEBr1Ge" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 5, 30, 2, 172, DateTimeKind.Utc).AddTicks(1658), "$2a$11$pw6Za9/TbxZzeAt7nKlNLuGPCQ2UM5SEMl9hYEQesSlUwyaSmEU3C" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 5, 30, 2, 326, DateTimeKind.Utc).AddTicks(9151), "$2a$11$HWOR6KA6UwljSqBTa96PquH5AOBQXGs98kOqXLTz7.V2VpCjDOLHK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 5, 30, 2, 521, DateTimeKind.Utc).AddTicks(7372), "$2a$11$/5Puo6XI/rJToASlT5zy9.TlCnWG4NDew9j8rA6t49t.6FzwH5wnO" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvoiceTypeID",
                table: "InvoiceTemplates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 4, 22, 50, 0, DateTimeKind.Utc).AddTicks(9952));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 4, 22, 50, 0, DateTimeKind.Utc).AddTicks(9960));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 4, 22, 50, 0, DateTimeKind.Utc).AddTicks(9963));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 4, 22, 50, 1, DateTimeKind.Utc).AddTicks(5));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 4, 22, 50, 1, DateTimeKind.Utc).AddTicks(9));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 4, 22, 50, 1, DateTimeKind.Utc).AddTicks(12));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 4, 22, 50, 155, DateTimeKind.Utc).AddTicks(2320), "$2a$11$PNh5PSCKIb7KTF.hs9az6uvHlU490Hvt8Zm6vIMGMpVd2iKxDyIBC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 4, 22, 50, 308, DateTimeKind.Utc).AddTicks(840), "$2a$11$.MhTmHvDD7VqI1YZzUhx7OTu99dCkprdGJppnI7ZKrnj6HvKazMke" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 4, 22, 50, 461, DateTimeKind.Utc).AddTicks(1313), "$2a$11$t4qyxj7c0w9yjlWDeYb0euLcqMAqkYW.VxUQCMWWjXGGEiosO6MVS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 4, 22, 50, 614, DateTimeKind.Utc).AddTicks(355), "$2a$11$GUkFyw0i4kTGn0GbUUTrg..s2BSP.W0K7UBNuQg0HCVT7KHHthUgK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 4, 22, 50, 766, DateTimeKind.Utc).AddTicks(7781), "$2a$11$9BwhO2tMvk4PszZxD1Ab7uynFTW4jwFyQ6JRdUPfpM0/kagEyHcTC" });
        }
    }
}
