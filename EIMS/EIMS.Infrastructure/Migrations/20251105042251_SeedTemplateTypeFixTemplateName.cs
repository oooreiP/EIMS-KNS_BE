using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedTemplateTypeFixTemplateName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "TemplateTypes",
                columns: new[] { "TemplateTypeID", "TypeCategory", "TypeName" },
                values: new object[,]
                {
                    { 1, "New", "Hóa đơn mới" },
                    { 2, "Adjustment", "Hóa đơn điều chỉnh" },
                    { 3, "Replacement", "Hóa đơn thay thế" }
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TemplateTypes",
                keyColumn: "TemplateTypeID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TemplateTypes",
                keyColumn: "TemplateTypeID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TemplateTypes",
                keyColumn: "TemplateTypeID",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 10, 56, 27, 246, DateTimeKind.Utc).AddTicks(3643));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 10, 56, 27, 246, DateTimeKind.Utc).AddTicks(3652));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 10, 56, 27, 246, DateTimeKind.Utc).AddTicks(3655));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 10, 56, 27, 246, DateTimeKind.Utc).AddTicks(3706));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 10, 56, 27, 246, DateTimeKind.Utc).AddTicks(3710));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 10, 56, 27, 246, DateTimeKind.Utc).AddTicks(3713));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 10, 56, 27, 402, DateTimeKind.Utc).AddTicks(8950), "$2a$11$IE5Q1kOFqENen56uo2XoG.IP5OuTlbCFL.nuXPGSxbGA5A03qkhSu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 10, 56, 27, 558, DateTimeKind.Utc).AddTicks(585), "$2a$11$bnbyp9yMHM0s6SVpKEm8r.EL1rTO0CaB26JfqmF0.dWuvkizo6UGm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 10, 56, 27, 713, DateTimeKind.Utc).AddTicks(4751), "$2a$11$dpK4fK/i2FY24mwvyLEdt.dDwkKUW6Isi5lDyjCxvDhOiwH1lhbvS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 10, 56, 27, 867, DateTimeKind.Utc).AddTicks(9310), "$2a$11$Nnw9F5lCMc9IrAg2b30o9eXFKSeZmnyTIbs2XyqLSAUY9aXX9OaUK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 10, 56, 28, 23, DateTimeKind.Utc).AddTicks(1802), "$2a$11$130Van7BXkWIvex2BIeHau.VBnULwR2mN4ekRPd1a2uCpvxVgVwIW" });
        }
    }
}
