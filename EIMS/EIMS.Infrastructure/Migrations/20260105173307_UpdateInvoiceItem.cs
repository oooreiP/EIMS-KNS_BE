using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInvoiceItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItems_Invoices_OriginalInvoiceItemID",
                table: "InvoiceItems");

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

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItems_InvoiceItems_OriginalInvoiceItemID",
                table: "InvoiceItems",
                column: "OriginalInvoiceItemID",
                principalTable: "InvoiceItems",
                principalColumn: "InvoiceItemID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItems_InvoiceItems_OriginalInvoiceItemID",
                table: "InvoiceItems");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 4, 7, 12, 42, 933, DateTimeKind.Utc).AddTicks(4697));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 4, 7, 12, 42, 933, DateTimeKind.Utc).AddTicks(4748));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 4, 7, 12, 42, 933, DateTimeKind.Utc).AddTicks(4750));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 7, 12, 43, 487, DateTimeKind.Utc).AddTicks(7233));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 7, 12, 43, 487, DateTimeKind.Utc).AddTicks(7237));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 7, 12, 43, 487, DateTimeKind.Utc).AddTicks(7239));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 4, 7, 12, 42, 933, DateTimeKind.Utc).AddTicks(4782));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 4, 7, 12, 42, 933, DateTimeKind.Utc).AddTicks(4785));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 4, 7, 12, 42, 933, DateTimeKind.Utc).AddTicks(4787));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 4, 7, 12, 43, 42, DateTimeKind.Utc).AddTicks(3494), "$2a$11$9WHpZ2BTK961AiB0SxVHhe/acbvK7yY88scz66hxItj47rERls6/q" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 4, 7, 12, 43, 152, DateTimeKind.Utc).AddTicks(7779), "$2a$11$yt6XrG94dv2s.2jVWOF4hOsbtks3Z62oyKJ2tztrhMw8kVD0u.OZC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 4, 7, 12, 43, 265, DateTimeKind.Utc).AddTicks(51), "$2a$11$t4MbDJhT6uSK3Eoqry9OZeuBut8QWDKJXX51Ckm4M9lo9AteHK1m." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 4, 7, 12, 43, 376, DateTimeKind.Utc).AddTicks(6727), "$2a$11$hYriq88V0ZMYv5FberW3ueURUV2KodAQsVf.1lns/k.1OsST2h1oy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 4, 7, 12, 43, 486, DateTimeKind.Utc).AddTicks(1011), "$2a$11$X1tzW5wKS2b8JMkI9D0mLO6zjg7GiziLrHA5/W.nzVZlW6SeDWDUe" });

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItems_Invoices_OriginalInvoiceItemID",
                table: "InvoiceItems",
                column: "OriginalInvoiceItemID",
                principalTable: "Invoices",
                principalColumn: "InvoiceID");
        }
    }
}
