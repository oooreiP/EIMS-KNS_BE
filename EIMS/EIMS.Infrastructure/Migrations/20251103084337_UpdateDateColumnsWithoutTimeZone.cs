using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDateColumnsWithoutTimeZone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Products",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "GETDATE()");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Products",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 8, 31, 4, 845, DateTimeKind.Utc).AddTicks(184), "$2a$11$bh1EdNYC0hCdcZP60.ELieBunoD3YidnkphKJgXHR5c4EIqIszXj6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 8, 31, 4, 959, DateTimeKind.Utc).AddTicks(2009), "$2a$11$34hUw4xMb4a.56SjRCI4cOnOED9w3ApHI53A3DpuB43Hu7oVT1l/y" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 8, 31, 5, 75, DateTimeKind.Utc).AddTicks(5668), "$2a$11$SIjnjvEKikzvNbnZ.KDKqOFzBCqPGxvYoit1jINMAh54iI3wQrfl." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 8, 31, 5, 194, DateTimeKind.Utc).AddTicks(9502), "$2a$11$65UwrrUZ3PbQC0PHq5J9T.aI4/qBZu1ASZcmGiS6tDkn4wtBP4sYW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 3, 8, 31, 5, 309, DateTimeKind.Utc).AddTicks(170), "$2a$11$drJH5abER31xsCPMRZebzOT8lWz20lvt.BSgh6YUIMlYFs/HeSEeC" });
        }
    }
}
