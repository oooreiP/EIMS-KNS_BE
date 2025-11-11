using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFilePathToInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Companies_CompanyId",
                table: "Invoices");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Invoices",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Invoices",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 11, 1, 39, 14, 414, DateTimeKind.Utc).AddTicks(575));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 11, 1, 39, 14, 414, DateTimeKind.Utc).AddTicks(584));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 11, 1, 39, 14, 414, DateTimeKind.Utc).AddTicks(588));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 11, 1, 39, 14, 414, DateTimeKind.Utc).AddTicks(650));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 11, 1, 39, 14, 414, DateTimeKind.Utc).AddTicks(655));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 11, 1, 39, 14, 414, DateTimeKind.Utc).AddTicks(660));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 11, 1, 39, 14, 613, DateTimeKind.Utc).AddTicks(8578), "$2a$11$.ylbHpODOOui8.xGh9skPORb9X6/fNKy9cunJ6UzhjEOtsgSBEIwW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 11, 1, 39, 14, 813, DateTimeKind.Utc).AddTicks(5368), "$2a$11$i6wYzxHJn1X3dAqXkU76weIUk.zpoqyBLsDGVyCKur4YVZLDznOdG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 11, 1, 39, 15, 13, DateTimeKind.Utc).AddTicks(5773), "$2a$11$6qqDfvUCpLuRAPCNbL.pw.nRgkdnGVWNgklPY07XxVe7V5Xd/WDqe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 11, 1, 39, 15, 211, DateTimeKind.Utc).AddTicks(9002), "$2a$11$ZvzezrBWK/mQHUoNw7ssOOodU54rRY3C4XyUrI31SqhkCVxwyDHV6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 11, 1, 39, 15, 408, DateTimeKind.Utc).AddTicks(8857), "$2a$11$YqqaP9baAWa7pyPIEMH2ZenUuBEoD0K3t0M0t6fYbImJYaUqq2lq2" });

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Companies_CompanyId",
                table: "Invoices",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Companies_CompanyId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Invoices");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Invoices",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 21, 14, 45, 211, DateTimeKind.Utc).AddTicks(8976));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 21, 14, 45, 211, DateTimeKind.Utc).AddTicks(8981));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 21, 14, 45, 211, DateTimeKind.Utc).AddTicks(8984));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 21, 14, 45, 211, DateTimeKind.Utc).AddTicks(9035));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 21, 14, 45, 211, DateTimeKind.Utc).AddTicks(9038));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 21, 14, 45, 211, DateTimeKind.Utc).AddTicks(9040));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 21, 14, 45, 323, DateTimeKind.Utc).AddTicks(6028), "$2a$11$8OnAd5fTKq5K8FUoYdEXS.SKP.7cFSFA.k9mNjmxO8I5A7H14VSN2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 21, 14, 45, 436, DateTimeKind.Utc).AddTicks(6497), "$2a$11$0VeNKilzvqyUHn51meEZren/oNTOo8KiiASbc9VLXatzz4mxBAhEa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 21, 14, 45, 546, DateTimeKind.Utc).AddTicks(6976), "$2a$11$ax5kXFGJrm1wMeNy582p7.P4YHlMEq4HGlt7r2TebP9zACF2m2FkW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 21, 14, 45, 657, DateTimeKind.Utc).AddTicks(6569), "$2a$11$s1S1tT4SiQi61n..sitNcO/g.qmvOCWEWU2pauINLZvC2bj1vAvA." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 21, 14, 45, 770, DateTimeKind.Utc).AddTicks(5167), "$2a$11$t6Aa.Uf5l6o2JBt5yllg4.U6lMm7SfDA7mqOodS7WS0WMm9uF6Bsu" });

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Companies_CompanyId",
                table: "Invoices",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
