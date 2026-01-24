using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoiceCustomerTypeForRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvoiceCustomerType",
                table: "InvoiceRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 20, 1, 21, 183, DateTimeKind.Utc).AddTicks(8678));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 20, 1, 21, 183, DateTimeKind.Utc).AddTicks(8687));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 20, 1, 21, 183, DateTimeKind.Utc).AddTicks(8690));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 20, 1, 21, 746, DateTimeKind.Utc).AddTicks(3408));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 20, 1, 21, 746, DateTimeKind.Utc).AddTicks(3412));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 20, 1, 21, 746, DateTimeKind.Utc).AddTicks(3415));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 20, 1, 21, 746, DateTimeKind.Utc).AddTicks(3416));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 20, 1, 21, 746, DateTimeKind.Utc).AddTicks(3418));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 20, 1, 21, 746, DateTimeKind.Utc).AddTicks(3419));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 20, 1, 21, 746, DateTimeKind.Utc).AddTicks(3421));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 20, 1, 21, 183, DateTimeKind.Utc).AddTicks(8734));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 20, 1, 21, 183, DateTimeKind.Utc).AddTicks(8738));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 20, 1, 21, 183, DateTimeKind.Utc).AddTicks(8741));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 24, 20, 1, 21, 300, DateTimeKind.Utc).AddTicks(9717), "$2a$11$RySEHu27CJ3HmHB9xCrl3e7eYat8TIjEYzXW884t5Iz7hm328vYtm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 24, 20, 1, 21, 413, DateTimeKind.Utc).AddTicks(2364), "$2a$11$e08z6i5SBEM2xquozK/doOjcAf38TBXrjPcGkQlwcZ4VbhKtSjfDi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 24, 20, 1, 21, 526, DateTimeKind.Utc).AddTicks(749), "$2a$11$TjkcQY8uhoAG/5l2Exyw/.xmEzEUcYW4rCLz.I6CgvCcky/sGyngS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 24, 20, 1, 21, 636, DateTimeKind.Utc).AddTicks(4163), "$2a$11$OY3mryrsLPUTmRHU70rcTuyonQNnZWdUEeeGiPtoZp69HNZa98J6W" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 24, 20, 1, 21, 746, DateTimeKind.Utc).AddTicks(2078), "$2a$11$Z5SYqMZN92Ka71G0/euFH.GfbDLSMfyDDL691NPL8I6/iY210BSau" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceCustomerType",
                table: "InvoiceRequests");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 18, 37, 23, 993, DateTimeKind.Utc).AddTicks(8755));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 18, 37, 23, 993, DateTimeKind.Utc).AddTicks(8764));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 18, 37, 23, 993, DateTimeKind.Utc).AddTicks(8767));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 18, 37, 24, 561, DateTimeKind.Utc).AddTicks(1897));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 18, 37, 24, 561, DateTimeKind.Utc).AddTicks(1901));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 18, 37, 24, 561, DateTimeKind.Utc).AddTicks(1903));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 18, 37, 24, 561, DateTimeKind.Utc).AddTicks(1905));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 18, 37, 24, 561, DateTimeKind.Utc).AddTicks(1907));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 18, 37, 24, 561, DateTimeKind.Utc).AddTicks(1908));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 18, 37, 24, 561, DateTimeKind.Utc).AddTicks(1912));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 18, 37, 23, 993, DateTimeKind.Utc).AddTicks(8806));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 18, 37, 23, 993, DateTimeKind.Utc).AddTicks(8808));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 18, 37, 23, 993, DateTimeKind.Utc).AddTicks(8811));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 24, 18, 37, 24, 106, DateTimeKind.Utc).AddTicks(8369), "$2a$11$mEy72ZZSdvlpgYr1vkxu9eKp9O5ke8fSrWpRvl6SO9rOp/2Zv1raa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 24, 18, 37, 24, 219, DateTimeKind.Utc).AddTicks(3570), "$2a$11$wF5XYvBR0jk4xokwNspTmu0ky0XWvfj3D8S4cZ59Em9kTOREGzHpa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 24, 18, 37, 24, 336, DateTimeKind.Utc).AddTicks(4494), "$2a$11$BeIFucvrB55Fc8CFcvF4weUs78JRNLWRe8xD6NnY/Lt8qql6shvvG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 24, 18, 37, 24, 449, DateTimeKind.Utc).AddTicks(4165), "$2a$11$WJtq3G5cj26Zlw2hs88l/evhMrKCZQn.bgQ2PSsza2Yy86UlZpgLe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 24, 18, 37, 24, 561, DateTimeKind.Utc).AddTicks(466), "$2a$11$MtI6GiIXPMiT7lOjjitDjOnCQgE1s9jFlJchdQTAoMu8fMxBBOjv6" });
        }
    }
}
