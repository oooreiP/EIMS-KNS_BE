using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "PhoneNumber", "RoleID" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 29, 11, 53, 24, 185, DateTimeKind.Utc).AddTicks(863), "admin@eims.local", "Admin User", true, "$2a$11$QIf1jRGnXSdTxLBzKczFX.0YVZ9slcpvM0Ald/OyIp7XUv7bGY0ma", "0101010101", 1 },
                    { 2, new DateTime(2025, 10, 29, 11, 53, 24, 338, DateTimeKind.Utc).AddTicks(2086), "accountant@eims.local", "Accountant User", true, "$2a$11$vQH4TOwkluaVtJQqW5h8R.B/9M0RrVEFVlht1C3S8hgwELgw..woy", "0202020202", 2 },
                    { 3, new DateTime(2025, 10, 29, 11, 53, 24, 491, DateTimeKind.Utc).AddTicks(3899), "sale@eims.local", "Sales User", true, "$2a$11$cAxQQcWX5Or4sBD6gUk4GepekcJ2VDsO7mtGeJEpyivhnJ7q/y3tO", "0303030303", 3 },
                    { 4, new DateTime(2025, 10, 29, 11, 53, 24, 644, DateTimeKind.Utc).AddTicks(336), "hod@eims.local", "Head Dept User", true, "$2a$11$XysmTrukGd0GqP7DEdVGwOaB4fQnJ1YGkh9JU1IG5FLSsuIl//kgm", "0404040404", 4 },
                    { 5, new DateTime(2025, 10, 29, 11, 53, 24, 797, DateTimeKind.Utc).AddTicks(1426), "customer@eims.local", "Customer User", true, "$2a$11$gXUZCHekA/kCEH5I7vu/nep2dSI.XrcCXXcrchwejvM22/ydu6QX.", "0505050505", 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5);
        }
    }
}
