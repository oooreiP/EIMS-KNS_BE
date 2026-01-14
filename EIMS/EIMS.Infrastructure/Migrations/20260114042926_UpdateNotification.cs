using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NotificationNumber",
                table: "InvoiceErrorNotifications",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NotificationType",
                table: "InvoiceErrorNotifications",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TaxAuthorityName",
                table: "InvoiceErrorNotifications",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InvoiceTaxCode",
                table: "InvoiceErrorDetails",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TaxpayerName",
                table: "InvoiceErrorDetails",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 14, 4, 29, 19, 853, DateTimeKind.Utc).AddTicks(7396));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 14, 4, 29, 19, 853, DateTimeKind.Utc).AddTicks(7402));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 14, 4, 29, 19, 853, DateTimeKind.Utc).AddTicks(7404));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 14, 4, 29, 20, 419, DateTimeKind.Utc).AddTicks(8887));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 14, 4, 29, 20, 419, DateTimeKind.Utc).AddTicks(8892));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 14, 4, 29, 20, 419, DateTimeKind.Utc).AddTicks(8894));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 14, 4, 29, 20, 419, DateTimeKind.Utc).AddTicks(8896));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 14, 4, 29, 20, 419, DateTimeKind.Utc).AddTicks(8897));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 14, 4, 29, 20, 419, DateTimeKind.Utc).AddTicks(8899));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 14, 4, 29, 19, 853, DateTimeKind.Utc).AddTicks(7431));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 14, 4, 29, 19, 853, DateTimeKind.Utc).AddTicks(7435));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 14, 4, 29, 19, 853, DateTimeKind.Utc).AddTicks(7473));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 14, 4, 29, 19, 963, DateTimeKind.Utc).AddTicks(8866), "$2a$11$QbWLGVi33T4g8wrND909jO2S.JHasBnCMZqNgdUQ6fBiQB5HRx58K" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 14, 4, 29, 20, 75, DateTimeKind.Utc).AddTicks(7089), "$2a$11$dz3jaQ.rv0f/GMsKO.um6e0xI8swBstgFmBugKtGKSnoBVJLdLu2S" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 14, 4, 29, 20, 187, DateTimeKind.Utc).AddTicks(4850), "$2a$11$o5b00/B34e8WI8sl1esY7ePp83tQH9BEauE8O7ljEnffi0Y4jM6ee" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 14, 4, 29, 20, 299, DateTimeKind.Utc).AddTicks(6147), "$2a$11$EU4TOQ2u2d0t1LX/bwK4o.VVc63Vf.Dt4UtxXeW3dPpAjIqiGRzam" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 14, 4, 29, 20, 417, DateTimeKind.Utc).AddTicks(7353), "$2a$11$cEmZB1/MB/8Md9GheGIcgOP19jLeScskfk4HxUJ4qnn1Z7tnHvWNS" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationNumber",
                table: "InvoiceErrorNotifications");

            migrationBuilder.DropColumn(
                name: "NotificationType",
                table: "InvoiceErrorNotifications");

            migrationBuilder.DropColumn(
                name: "TaxAuthorityName",
                table: "InvoiceErrorNotifications");

            migrationBuilder.DropColumn(
                name: "InvoiceTaxCode",
                table: "InvoiceErrorDetails");

            migrationBuilder.DropColumn(
                name: "TaxpayerName",
                table: "InvoiceErrorDetails");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 12, 34, 19, 828, DateTimeKind.Utc).AddTicks(2944));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 12, 34, 19, 828, DateTimeKind.Utc).AddTicks(2954));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 12, 34, 19, 828, DateTimeKind.Utc).AddTicks(2957));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 12, 34, 20, 413, DateTimeKind.Utc).AddTicks(604));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 12, 34, 20, 413, DateTimeKind.Utc).AddTicks(609));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 12, 34, 20, 413, DateTimeKind.Utc).AddTicks(611));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 12, 34, 20, 413, DateTimeKind.Utc).AddTicks(612));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 12, 34, 20, 413, DateTimeKind.Utc).AddTicks(613));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 12, 34, 20, 413, DateTimeKind.Utc).AddTicks(615));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 12, 34, 19, 828, DateTimeKind.Utc).AddTicks(2984));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 12, 34, 19, 828, DateTimeKind.Utc).AddTicks(2987));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 12, 34, 19, 828, DateTimeKind.Utc).AddTicks(2989));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 11, 12, 34, 19, 940, DateTimeKind.Utc).AddTicks(9169), "$2a$11$Lg5jdAdM4zIwGqGJrNT5FOnm/17STspS8sdWPPLEm6jLPgZ08jfsK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 11, 12, 34, 20, 59, DateTimeKind.Utc).AddTicks(5123), "$2a$11$VzKJod/S23BlWiczEa27DOma04E.hQWY7WGSfnYkuuCh8W5Bsvcvi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 11, 12, 34, 20, 179, DateTimeKind.Utc).AddTicks(9925), "$2a$11$bS26lCzjhwx7KFxDNXZL1e3y5jHx8.E50GRZLNzTDN0XQfj0J1SjS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 11, 12, 34, 20, 298, DateTimeKind.Utc).AddTicks(2436), "$2a$11$rIXW7fXkFSzecMyoRpd.U.ovIhoKrCS5BzUcEmtXMjzGF.Pr3Kr6." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 11, 12, 34, 20, 411, DateTimeKind.Utc).AddTicks(266), "$2a$11$lRc3bBS2vHNj6SJb6JzUdO2rx/yCjL1UgexiYf.6Yeiw0chV0.B3e" });
        }
    }
}
