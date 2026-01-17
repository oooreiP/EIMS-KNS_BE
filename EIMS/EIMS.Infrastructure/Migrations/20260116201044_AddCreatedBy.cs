using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Invoices",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 16, 20, 10, 41, 66, DateTimeKind.Utc).AddTicks(2418));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 16, 20, 10, 41, 66, DateTimeKind.Utc).AddTicks(2428));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 16, 20, 10, 41, 66, DateTimeKind.Utc).AddTicks(2430));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 16, 20, 10, 41, 649, DateTimeKind.Utc).AddTicks(9517));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 16, 20, 10, 41, 649, DateTimeKind.Utc).AddTicks(9520));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 16, 20, 10, 41, 649, DateTimeKind.Utc).AddTicks(9522));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 16, 20, 10, 41, 649, DateTimeKind.Utc).AddTicks(9524));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 16, 20, 10, 41, 649, DateTimeKind.Utc).AddTicks(9525));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 16, 20, 10, 41, 649, DateTimeKind.Utc).AddTicks(9526));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 16, 20, 10, 41, 66, DateTimeKind.Utc).AddTicks(2468));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 16, 20, 10, 41, 66, DateTimeKind.Utc).AddTicks(2472));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 16, 20, 10, 41, 66, DateTimeKind.Utc).AddTicks(2476));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 16, 20, 10, 41, 179, DateTimeKind.Utc).AddTicks(2705), "$2a$11$AMv7ExCB/sRW/yb7kfQkyuppuIEcpatH6RW1julGyLekjvygMQssq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 16, 20, 10, 41, 297, DateTimeKind.Utc).AddTicks(5192), "$2a$11$S.OFWXLjS8pOC7OtevNu6.5pdi9hdpoym3kkXPiYAlPpnSl8iNAWK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 16, 20, 10, 41, 418, DateTimeKind.Utc).AddTicks(4282), "$2a$11$nlPcpHwAiHn8E1oc/B4oWecI6VQ8Pq/cYGzGQ.YYjQ2.IbrX2Ussq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 16, 20, 10, 41, 536, DateTimeKind.Utc).AddTicks(1885), "$2a$11$sQi6CF4wEE9Dt7fM/U.W6eZ8QZsewld497iA2xrLizA3Ciee0PlWW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 16, 20, 10, 41, 647, DateTimeKind.Utc).AddTicks(5900), "$2a$11$RDGHJinUYlCQxOBsk1L05eaTyyp1FDECar5yrm1gaBJqjsopEz6d." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Invoices");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 15, 10, 7, 46, 767, DateTimeKind.Utc).AddTicks(6583));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 15, 10, 7, 46, 767, DateTimeKind.Utc).AddTicks(6589));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 15, 10, 7, 46, 767, DateTimeKind.Utc).AddTicks(6592));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 15, 10, 7, 47, 332, DateTimeKind.Utc).AddTicks(201));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 15, 10, 7, 47, 332, DateTimeKind.Utc).AddTicks(205));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 15, 10, 7, 47, 332, DateTimeKind.Utc).AddTicks(207));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 15, 10, 7, 47, 332, DateTimeKind.Utc).AddTicks(209));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 15, 10, 7, 47, 332, DateTimeKind.Utc).AddTicks(210));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 15, 10, 7, 47, 332, DateTimeKind.Utc).AddTicks(211));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 15, 10, 7, 46, 767, DateTimeKind.Utc).AddTicks(6623));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 15, 10, 7, 46, 767, DateTimeKind.Utc).AddTicks(6626));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 15, 10, 7, 46, 767, DateTimeKind.Utc).AddTicks(6629));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 15, 10, 7, 46, 881, DateTimeKind.Utc).AddTicks(4210), "$2a$11$seABg1EseKKJn6bRe1NGX.grF6LVbV2CfQ5WtkWweIhVQdDAFDFEm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 15, 10, 7, 46, 994, DateTimeKind.Utc).AddTicks(8521), "$2a$11$aVhSnRXAOwSu0EA9RcOkOuDqXEYTpXz7nyzBdgEj/xdavrbWvCcYm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 15, 10, 7, 47, 107, DateTimeKind.Utc).AddTicks(1103), "$2a$11$9iGYtujVBrpH6fJmFpG5ve7NGLMpjpLILBOjLgUo76DLY60bHfrAW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 15, 10, 7, 47, 219, DateTimeKind.Utc).AddTicks(1243), "$2a$11$IPrQmlyt/S5AYGlCF5wfZOofE42MiEfANoXEHmTf8gU/D228WCUfG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 15, 10, 7, 47, 330, DateTimeKind.Utc).AddTicks(2429), "$2a$11$LHBRVVodE25wXsjSFdCsr.TaoIfQMDLgo4x26TqBd8LjxVfCwfy.O" });
        }
    }
}
