using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditLogs_Users_UserId",
                table: "AuditLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemActivityLogs_Users_UserId",
                table: "SystemActivityLogs");

            migrationBuilder.DropIndex(
                name: "IX_SystemActivityLogs_UserId",
                table: "SystemActivityLogs");

            migrationBuilder.DropIndex(
                name: "IX_AuditLogs_UserId",
                table: "AuditLogs");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SystemActivityLogs",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AuditLogs",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 18, 14, 5, 923, DateTimeKind.Utc).AddTicks(66));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 18, 14, 5, 923, DateTimeKind.Utc).AddTicks(72));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 18, 14, 5, 923, DateTimeKind.Utc).AddTicks(75));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 18, 14, 5, 923, DateTimeKind.Utc).AddTicks(104));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 18, 14, 5, 923, DateTimeKind.Utc).AddTicks(107));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 18, 14, 5, 923, DateTimeKind.Utc).AddTicks(110));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 29, 18, 14, 6, 34, DateTimeKind.Utc).AddTicks(715), "$2a$11$vgOqteiGjQZ5fdFtZR6H3.FqNFhr9PV2ELTzBRJDo/qlWnuLimOdW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 29, 18, 14, 6, 146, DateTimeKind.Utc).AddTicks(3220), "$2a$11$b0Ctw3LKv6GyQgzpEX0mYeaTwi/jx.N/yYl/zvsQwiIEaa60WjFNm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 29, 18, 14, 6, 261, DateTimeKind.Utc).AddTicks(7807), "$2a$11$YZV3smnSNRUoW2ml/06aL.rS3ywaQfTWLRw3xqRE.RWbi6GqDHe46" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 29, 18, 14, 6, 375, DateTimeKind.Utc).AddTicks(2549), "$2a$11$GIaqab0FKgjKWMnRy90L6.s9hsKRP2mqZXVMagVV/dVoUC8rpKH4G" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 29, 18, 14, 6, 494, DateTimeKind.Utc).AddTicks(626), "$2a$11$6c5jvxy3ycRf6oEEGCPUJe5rhPeztyCFZYuJ1umgwdb4AlWAu31oi" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "SystemActivityLogs",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AuditLogs",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 17, 47, 44, 448, DateTimeKind.Utc).AddTicks(3755));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 17, 47, 44, 448, DateTimeKind.Utc).AddTicks(3761));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 17, 47, 44, 448, DateTimeKind.Utc).AddTicks(3763));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 17, 47, 44, 448, DateTimeKind.Utc).AddTicks(3801));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 17, 47, 44, 448, DateTimeKind.Utc).AddTicks(3804));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 17, 47, 44, 448, DateTimeKind.Utc).AddTicks(3807));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 29, 17, 47, 44, 559, DateTimeKind.Utc).AddTicks(9571), "$2a$11$Gl1jvSf9c4NdakqxHimBgeVnLy8HpTtG71TRw9OLxbFnDCEVaWmPK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 29, 17, 47, 44, 670, DateTimeKind.Utc).AddTicks(1389), "$2a$11$o0kkN/xigF/0FbAzYPgUL.jmgGX3Jgjbrr64vhfa5m6xMGvnj2lWi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 29, 17, 47, 44, 780, DateTimeKind.Utc).AddTicks(7715), "$2a$11$zk2o9FLuu1pUpBWa0btkpeCLT2KrZRqhkzEs1h0lDgCDoLXdKEKNC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 29, 17, 47, 44, 890, DateTimeKind.Utc).AddTicks(6885), "$2a$11$Q8N019jz0hci1MM.NuA23O4fhYN0OC0VB6W7fgEMYfAkQuba1b9OK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 29, 17, 47, 45, 0, DateTimeKind.Utc).AddTicks(9394), "$2a$11$ZC7yilNvGdUAsYum72ZmqOtSQRdMsmhwgZbZ1yq6xA1VVNBt2MTZe" });

            migrationBuilder.CreateIndex(
                name: "IX_SystemActivityLogs_UserId",
                table: "SystemActivityLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserId",
                table: "AuditLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_Users_UserId",
                table: "AuditLogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemActivityLogs_Users_UserId",
                table: "SystemActivityLogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
