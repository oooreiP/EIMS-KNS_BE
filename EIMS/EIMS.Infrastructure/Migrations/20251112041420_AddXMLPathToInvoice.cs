using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddXMLPathToInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "XMLPath",
                table: "Invoices",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 12, 4, 14, 17, 554, DateTimeKind.Utc).AddTicks(4203));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 12, 4, 14, 17, 554, DateTimeKind.Utc).AddTicks(4208));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 12, 4, 14, 17, 554, DateTimeKind.Utc).AddTicks(4210));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 12, 4, 14, 17, 554, DateTimeKind.Utc).AddTicks(4249));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 12, 4, 14, 17, 554, DateTimeKind.Utc).AddTicks(4252));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 12, 4, 14, 17, 554, DateTimeKind.Utc).AddTicks(4254));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 12, 4, 14, 17, 667, DateTimeKind.Utc).AddTicks(262), "$2a$11$l50Si39oMYCU63V8JvMXOOZXF1usqtRdNFVghu9tW6HyvurVkgmTu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 12, 4, 14, 17, 779, DateTimeKind.Utc).AddTicks(3492), "$2a$11$9J2ulvdxR60zbydDuKm54uwcxvoD.z2WLWLS0hMAjjrjmCtq7Xyzm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 12, 4, 14, 17, 898, DateTimeKind.Utc).AddTicks(4499), "$2a$11$RfNZWSVXTgtncvkNENGMSebbG5bC4dg108YbisQ6U2m2OxLb.kOIe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 12, 4, 14, 18, 12, DateTimeKind.Utc).AddTicks(5698), "$2a$11$CqiCG7eQqdgeBY5w4Pv7HeY.yVxAQklsq3qhuKpJCF/Z.zQD7VIS2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 12, 4, 14, 18, 122, DateTimeKind.Utc).AddTicks(4468), "$2a$11$VG6sTeb5NYuEcBZA5fVRbe/4gWaBzNMMMt98LSKhSGO54cUhR2Qui" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "XMLPath",
                table: "Invoices");

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
        }
    }
}
