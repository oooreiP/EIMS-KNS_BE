using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixAuditLogForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 8, 10, 29, 23, 947, DateTimeKind.Utc).AddTicks(8978));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 8, 10, 29, 23, 947, DateTimeKind.Utc).AddTicks(8985));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 8, 10, 29, 23, 947, DateTimeKind.Utc).AddTicks(8987));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 8, 10, 29, 24, 519, DateTimeKind.Utc).AddTicks(8856));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 8, 10, 29, 24, 519, DateTimeKind.Utc).AddTicks(8860));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 8, 10, 29, 24, 519, DateTimeKind.Utc).AddTicks(8862));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 8, 10, 29, 23, 947, DateTimeKind.Utc).AddTicks(9021));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 8, 10, 29, 23, 947, DateTimeKind.Utc).AddTicks(9024));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 8, 10, 29, 23, 947, DateTimeKind.Utc).AddTicks(9026));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 8, 10, 29, 24, 61, DateTimeKind.Utc).AddTicks(7616), "$2a$11$UqaSY7voYBxQh7Xn5G.yl.xIOTy.zfQyxRoU..MhsCOOgNWvdK9tS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 8, 10, 29, 24, 174, DateTimeKind.Utc).AddTicks(2179), "$2a$11$BuSUDIWleGVNr.1bS/6uPu4K4DvAX/gel4y3Hp5RbD2Lu6N15uhkm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 8, 10, 29, 24, 286, DateTimeKind.Utc).AddTicks(7637), "$2a$11$IMre0tLew.nyxIDrLSiJiexqeyM9S.GnN91EWKcvV0V68zuL0gFCy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 8, 10, 29, 24, 405, DateTimeKind.Utc).AddTicks(1151), "$2a$11$Hpl2Grke05q0UFK/9ywI9.6YEgXtHujIJ3I7gFdGjhNWLkLxiXtFa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 8, 10, 29, 24, 518, DateTimeKind.Utc).AddTicks(196), "$2a$11$r//pV6K1Gnw25C2zp.SaION9ed1tfMuRGnE2jTb5eQ93iRAm7pfZy" });

            // migrationBuilder.CreateIndex(
            //     name: "IX_AuditLogs_UserID",
            //     table: "AuditLogs",
            //     column: "UserID");

            // migrationBuilder.AddForeignKey(
            //     name: "FK_AuditLogs_Users_UserID",
            //     table: "AuditLogs",
            //     column: "UserID",
            //     principalTable: "Users",
            //     principalColumn: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditLogs_Users_UserID",
                table: "AuditLogs");

            migrationBuilder.DropIndex(
                name: "IX_AuditLogs_UserID",
                table: "AuditLogs");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "AuditLogs",
                newName: "UserId");

            migrationBuilder.Sql(@"ALTER TABLE ""AuditLogs"" ALTER COLUMN ""UserID"" TYPE text USING ""UserID""::text");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 5, 17, 33, 4, 641, DateTimeKind.Utc).AddTicks(6861));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 5, 17, 33, 4, 641, DateTimeKind.Utc).AddTicks(6867));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 5, 17, 33, 4, 641, DateTimeKind.Utc).AddTicks(6869));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 5, 17, 33, 5, 214, DateTimeKind.Utc).AddTicks(9382));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 5, 17, 33, 5, 214, DateTimeKind.Utc).AddTicks(9386));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 5, 17, 33, 5, 214, DateTimeKind.Utc).AddTicks(9387));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 5, 17, 33, 4, 641, DateTimeKind.Utc).AddTicks(6901));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 5, 17, 33, 4, 641, DateTimeKind.Utc).AddTicks(6904));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 5, 17, 33, 4, 641, DateTimeKind.Utc).AddTicks(6906));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 5, 17, 33, 4, 753, DateTimeKind.Utc).AddTicks(539), "$2a$11$OokvQnGr2Ld4HmCbIgkFqOe42MCUNxSuzWk1OMBePt63dz3tZkWba" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 5, 17, 33, 4, 864, DateTimeKind.Utc).AddTicks(4154), "$2a$11$BPxFBXsKOWMnpUjlhGCbCuROPj4O8eRFhGYc/862UPVVKeYmplEj6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 5, 17, 33, 4, 978, DateTimeKind.Utc).AddTicks(6080), "$2a$11$7vL.qtz6iSQSWjf80LzKvuUcDUNb/5CAZu6NtzzUADUxmOW3HgeH." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 5, 17, 33, 5, 98, DateTimeKind.Utc).AddTicks(7038), "$2a$11$ne3oucb9pQBGzLUJaWCvaueV.uNuj5ulZ/PvGSPO1ruOW/QWd/1/2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 5, 17, 33, 5, 213, DateTimeKind.Utc).AddTicks(590), "$2a$11$YT46BFlhUdTKqolPlyvlieJZh344EN3lXd1ly.T/mAFxb9Z.vDMSy" });
        }
    }
}
