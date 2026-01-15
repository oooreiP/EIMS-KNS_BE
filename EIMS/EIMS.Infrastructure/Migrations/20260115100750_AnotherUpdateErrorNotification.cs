using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AnotherUpdateErrorNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationType",
                table: "InvoiceErrorNotifications");

            migrationBuilder.AddColumn<int>(
                name: "NotificationTypeCode",
                table: "InvoiceErrorNotifications",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationTypeCode",
                table: "InvoiceErrorNotifications");

            migrationBuilder.AddColumn<string>(
                name: "NotificationType",
                table: "InvoiceErrorNotifications",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 15, 9, 8, 24, 937, DateTimeKind.Utc).AddTicks(9775));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 15, 9, 8, 24, 937, DateTimeKind.Utc).AddTicks(9782));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 15, 9, 8, 24, 937, DateTimeKind.Utc).AddTicks(9786));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 15, 9, 8, 25, 512, DateTimeKind.Utc).AddTicks(2141));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 15, 9, 8, 25, 512, DateTimeKind.Utc).AddTicks(2146));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 15, 9, 8, 25, 512, DateTimeKind.Utc).AddTicks(2148));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 15, 9, 8, 25, 512, DateTimeKind.Utc).AddTicks(2149));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 15, 9, 8, 25, 512, DateTimeKind.Utc).AddTicks(2151));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 15, 9, 8, 25, 512, DateTimeKind.Utc).AddTicks(2152));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 15, 9, 8, 24, 937, DateTimeKind.Utc).AddTicks(9814));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 15, 9, 8, 24, 937, DateTimeKind.Utc).AddTicks(9817));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 15, 9, 8, 24, 937, DateTimeKind.Utc).AddTicks(9820));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 15, 9, 8, 25, 50, DateTimeKind.Utc).AddTicks(9708), "$2a$11$Lj9t.ifFXf7m8RDgF5mE/uSQI/fW/gU4RoCWVwfc.XxbARcNeruj2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 15, 9, 8, 25, 169, DateTimeKind.Utc).AddTicks(258), "$2a$11$cFyRr9AEWlqtpyFJqtYH2eyfqEtCYwKZ0e0FjSaqIjT5iWchKPZGK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 15, 9, 8, 25, 285, DateTimeKind.Utc).AddTicks(9053), "$2a$11$nn34xUh6R62rlgICo6worunL.cTbwpV65OxhaHt68vbDEXVbSKm9G" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 15, 9, 8, 25, 399, DateTimeKind.Utc).AddTicks(3769), "$2a$11$IhNHgoJkWpJC9PEQ/QR0Se5mUooAHrALOBWiWb2t0fJzlzX6YZGA." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 15, 9, 8, 25, 510, DateTimeKind.Utc).AddTicks(998), "$2a$11$rgleQXO7mdxvOlrh94Aaxu/rZW0iNUGl2hIXkJJZku73In.sH/A56" });
        }
    }
}
