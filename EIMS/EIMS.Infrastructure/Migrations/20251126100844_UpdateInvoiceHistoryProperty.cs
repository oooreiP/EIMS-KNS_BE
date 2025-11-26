using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInvoiceHistoryProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceHistories_Users_PerformerUserID",
                table: "InvoiceHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Users_IssuerID",
                table: "Invoices");

            migrationBuilder.AlterColumn<int>(
                name: "IssuerID",
                table: "Invoices",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "PerformerUserID",
                table: "InvoiceHistories",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "PerformedBy",
                table: "InvoiceHistories",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 10, 8, 42, 875, DateTimeKind.Utc).AddTicks(8040));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 10, 8, 42, 875, DateTimeKind.Utc).AddTicks(8059));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 10, 8, 42, 875, DateTimeKind.Utc).AddTicks(8065));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 10, 8, 42, 875, DateTimeKind.Utc).AddTicks(8217));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 10, 8, 42, 875, DateTimeKind.Utc).AddTicks(8224));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 10, 8, 42, 875, DateTimeKind.Utc).AddTicks(8230));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 10, 8, 43, 55, DateTimeKind.Utc).AddTicks(3493), "$2a$11$b/j7DOxd/N3H3cu1A7qJS.KzDDPKNRLfIxe06eQr3DX03dAI9cGwC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 10, 8, 43, 227, DateTimeKind.Utc).AddTicks(3423), "$2a$11$ANSLPs91zsYErxZ.WlZ8iuSFOO.W.2MwBVHVCNpLLO11YBzBEyOXq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 10, 8, 43, 408, DateTimeKind.Utc).AddTicks(7914), "$2a$11$lJMoxiN3aht1oYhgQDRWK.1KqTtLhXzxc.FKgW9V752XpEW2tu9ma" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 10, 8, 43, 590, DateTimeKind.Utc).AddTicks(978), "$2a$11$xwSsHoH0V3oYFuLq8jX0pOrVdoeE.HdUtjrhhDAhRf0ECgk1XdNVy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 10, 8, 43, 768, DateTimeKind.Utc).AddTicks(7722), "$2a$11$wKeTo1IcrcFuTpJ5w08dMOCKjbrQ809wYLUjiW1cKpQNl30alJXam" });

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceHistories_Users_PerformerUserID",
                table: "InvoiceHistories",
                column: "PerformerUserID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Users_IssuerID",
                table: "Invoices",
                column: "IssuerID",
                principalTable: "Users",
                principalColumn: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceHistories_Users_PerformerUserID",
                table: "InvoiceHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Users_IssuerID",
                table: "Invoices");

            migrationBuilder.AlterColumn<int>(
                name: "IssuerID",
                table: "Invoices",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PerformerUserID",
                table: "InvoiceHistories",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PerformedBy",
                table: "InvoiceHistories",
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
                value: new DateTime(2025, 11, 26, 9, 39, 28, 40, DateTimeKind.Utc).AddTicks(6240));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 9, 39, 28, 40, DateTimeKind.Utc).AddTicks(6252));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 9, 39, 28, 40, DateTimeKind.Utc).AddTicks(6255));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 9, 39, 28, 40, DateTimeKind.Utc).AddTicks(6292));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 9, 39, 28, 40, DateTimeKind.Utc).AddTicks(6295));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 9, 39, 28, 40, DateTimeKind.Utc).AddTicks(6299));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 9, 39, 28, 150, DateTimeKind.Utc).AddTicks(8540), "$2a$11$uduRLy5tc.pNRs1qrY6UjuHu5NvgzB0OmlPScVhRGDsPHgLE6WbGO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 9, 39, 28, 260, DateTimeKind.Utc).AddTicks(9453), "$2a$11$uqTA.vOhEMq4uq.Z33I4celjr/NfUyWffFHqfsU.Z2nEdgYLmqx5W" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 9, 39, 28, 370, DateTimeKind.Utc).AddTicks(7447), "$2a$11$9vydIpxxUOB2nz71aWoX8O2Hd.ySr3fgOOOevqkhQgFwEi7QhGUNy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 9, 39, 28, 480, DateTimeKind.Utc).AddTicks(9764), "$2a$11$f/N6KtiEhxYWDsMm./WOyeIoFiZ13fH1GB.3pj5dlOxHrbXSQm.lG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 9, 39, 28, 590, DateTimeKind.Utc).AddTicks(9241), "$2a$11$39XnPd8DlG/eR5HENe5NSu8i13ioZv9UJcd20eQHL8J3Molj/3Uci" });

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceHistories_Users_PerformerUserID",
                table: "InvoiceHistories",
                column: "PerformerUserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Users_IssuerID",
                table: "Invoices",
                column: "IssuerID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
