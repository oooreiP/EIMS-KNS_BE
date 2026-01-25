using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceSymbol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MTDiepPhanHoi",
                table: "TaxApiLogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceSymbol",
                table: "Invoices",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 7, 58, 46, 805, DateTimeKind.Utc).AddTicks(5376));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 7, 58, 46, 805, DateTimeKind.Utc).AddTicks(5386));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 7, 58, 46, 805, DateTimeKind.Utc).AddTicks(5392));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 25, 7, 58, 47, 385, DateTimeKind.Utc).AddTicks(9303));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 25, 7, 58, 47, 385, DateTimeKind.Utc).AddTicks(9308));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 25, 7, 58, 47, 385, DateTimeKind.Utc).AddTicks(9310));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 25, 7, 58, 47, 385, DateTimeKind.Utc).AddTicks(9311));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 25, 7, 58, 47, 385, DateTimeKind.Utc).AddTicks(9313));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 25, 7, 58, 47, 385, DateTimeKind.Utc).AddTicks(9314));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 25, 7, 58, 47, 385, DateTimeKind.Utc).AddTicks(9320));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 7, 58, 46, 805, DateTimeKind.Utc).AddTicks(5422));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 7, 58, 46, 805, DateTimeKind.Utc).AddTicks(5426));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 7, 58, 46, 805, DateTimeKind.Utc).AddTicks(5428));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 25, 7, 58, 46, 925, DateTimeKind.Utc).AddTicks(7376), "$2a$11$csX5Q9SXiTBV2QEUXmBr7uNP2b09glSSxW9gYypggq.D0o9gXBsiO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 25, 7, 58, 47, 42, DateTimeKind.Utc).AddTicks(463), "$2a$11$avPz95Gvb14vw83TPNGDa.h.34wJE0m7f60u3kFngiVeT3fWCLfDC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 25, 7, 58, 47, 156, DateTimeKind.Utc).AddTicks(2153), "$2a$11$LpbS3Wj9SC/Y6pWvJTmtPOAaQuzivV2xrpIIslQI0YynkuqWkTgSe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 25, 7, 58, 47, 266, DateTimeKind.Utc).AddTicks(2572), "$2a$11$/1fI5ASa42QVJ1Qm46iPae3bc5xeCqn0w9CaoJNBGrKIxK1bF/LEi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 25, 7, 58, 47, 385, DateTimeKind.Utc).AddTicks(8045), "$2a$11$xafv3JbTy9edNZ1KZhR8g.xV4F4u9mFEhlr25a0lsGVnJi2bb11Eq" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MTDiepPhanHoi",
                table: "TaxApiLogs");

            migrationBuilder.DropColumn(
                name: "InvoiceSymbol",
                table: "Invoices");

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
    }
}
