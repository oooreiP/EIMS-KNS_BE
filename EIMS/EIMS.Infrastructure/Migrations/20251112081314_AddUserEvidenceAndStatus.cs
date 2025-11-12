using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserEvidenceAndStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EvidenceStoragePath",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 12, 8, 13, 10, 613, DateTimeKind.Utc).AddTicks(3316));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 12, 8, 13, 10, 613, DateTimeKind.Utc).AddTicks(3325));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 12, 8, 13, 10, 613, DateTimeKind.Utc).AddTicks(3328));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 12, 8, 13, 10, 613, DateTimeKind.Utc).AddTicks(3376));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 12, 8, 13, 10, 613, DateTimeKind.Utc).AddTicks(3380));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 12, 8, 13, 10, 613, DateTimeKind.Utc).AddTicks(3382));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "EvidenceStoragePath", "PasswordHash", "Status" },
                values: new object[] { new DateTime(2025, 11, 12, 8, 13, 10, 766, DateTimeKind.Utc).AddTicks(8841), null, "$2a$11$r2Dy0weOpDhx200KMQotWOtAZnveRIwrapBXY3YGPcALz6jA8sBgC", 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "EvidenceStoragePath", "PasswordHash", "Status" },
                values: new object[] { new DateTime(2025, 11, 12, 8, 13, 10, 919, DateTimeKind.Utc).AddTicks(5958), null, "$2a$11$xGrjScEY1BI/H1fa36gWfOguEr50F3PaU6oDtJTwdJSjKabyRPe/W", 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "EvidenceStoragePath", "PasswordHash", "Status" },
                values: new object[] { new DateTime(2025, 11, 12, 8, 13, 11, 72, DateTimeKind.Utc).AddTicks(8468), null, "$2a$11$1erseGApFC5GllEZhxylXOPyE/Rag3mZO21TGwF0y7ZDoKvzgsn4i", 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "EvidenceStoragePath", "PasswordHash", "Status" },
                values: new object[] { new DateTime(2025, 11, 12, 8, 13, 11, 234, DateTimeKind.Utc).AddTicks(9703), null, "$2a$11$azCWLGrFhkMr9i9RM9UOaOsOVUA/W5cqjGOrkdwI4AbpofRwwpAZO", 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "EvidenceStoragePath", "PasswordHash", "Status" },
                values: new object[] { new DateTime(2025, 11, 12, 8, 13, 11, 388, DateTimeKind.Utc).AddTicks(3109), null, "$2a$11$zoF55HwYxLmPbRX6GKCTlenPHAujKFE6WfSSmgwpG3EVRettKu19W", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EvidenceStoragePath",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 11, 16, 21, 21, 649, DateTimeKind.Utc).AddTicks(8423));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 11, 16, 21, 21, 649, DateTimeKind.Utc).AddTicks(8434));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 11, 16, 21, 21, 649, DateTimeKind.Utc).AddTicks(8438));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 11, 16, 21, 21, 649, DateTimeKind.Utc).AddTicks(8494));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 11, 16, 21, 21, 649, DateTimeKind.Utc).AddTicks(8499));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 11, 16, 21, 21, 649, DateTimeKind.Utc).AddTicks(8507));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 11, 16, 21, 21, 833, DateTimeKind.Utc).AddTicks(5561), "$2a$11$qoBm9IAkEJ170.TDILcfmOGQ24.FZVTp2dSJsM1Wqn8ELjCC3gF7G" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 11, 16, 21, 22, 9, DateTimeKind.Utc).AddTicks(1768), "$2a$11$Zi4X58fJb1AzuTxsDilRd.rszQ8n7wN35ScDo1k.h3ztGW1pwgk9q" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 11, 16, 21, 22, 181, DateTimeKind.Utc).AddTicks(3897), "$2a$11$MuVfv6jknBFkqwe6W.a49OVEnypyltwjZGrUBZB/mnVsAnTGptDZe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 11, 16, 21, 22, 348, DateTimeKind.Utc).AddTicks(5162), "$2a$11$lcwOqiRuLntAqESgYGcuGeLhT.t39OBrhH5Ub5s9LG50bEOGMMXJG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 11, 16, 21, 22, 516, DateTimeKind.Utc).AddTicks(5598), "$2a$11$/v/cblDed11BVBMKHRoDYOOohyL01YLwl5SA44kLrVQHWxd5h9ne." });
        }
    }
}
