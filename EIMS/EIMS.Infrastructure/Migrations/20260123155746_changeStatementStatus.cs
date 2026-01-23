using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeStatementStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 15, 57, 43, 608, DateTimeKind.Utc).AddTicks(577));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 15, 57, 43, 608, DateTimeKind.Utc).AddTicks(584));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 15, 57, 43, 608, DateTimeKind.Utc).AddTicks(588));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 15, 57, 44, 613, DateTimeKind.Utc).AddTicks(8863));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 15, 57, 44, 613, DateTimeKind.Utc).AddTicks(8874));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 15, 57, 44, 613, DateTimeKind.Utc).AddTicks(8877));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 15, 57, 44, 613, DateTimeKind.Utc).AddTicks(8880));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 15, 57, 44, 613, DateTimeKind.Utc).AddTicks(8882));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 15, 57, 44, 613, DateTimeKind.Utc).AddTicks(8885));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 15, 57, 44, 613, DateTimeKind.Utc).AddTicks(8889));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 15, 57, 43, 608, DateTimeKind.Utc).AddTicks(664));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 15, 57, 43, 608, DateTimeKind.Utc).AddTicks(671));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 15, 57, 43, 608, DateTimeKind.Utc).AddTicks(675));

            migrationBuilder.UpdateData(
                table: "StatementStatuses",
                keyColumn: "StatusID",
                keyValue: 3,
                column: "StatusName",
                value: "Wait for payment");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 15, 57, 43, 813, DateTimeKind.Utc).AddTicks(2982), "$2a$11$gvUcxBA0aM.0eJMTcrhfSePOQYg3BKxSWzUpMvQAimkAl6qRpamR." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 15, 57, 44, 14, DateTimeKind.Utc).AddTicks(7376), "$2a$11$fJCQcTviBMtEW6eLh2mwXOdT6NCVdWm/V5hlt0gctJqEjydNpAgZi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 15, 57, 44, 214, DateTimeKind.Utc).AddTicks(9264), "$2a$11$SaqkVK4SVV4WFe4Ts8QzKuiwr5.n.ZFrytMhnwO4WBn4YdtEBh8zC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 15, 57, 44, 413, DateTimeKind.Utc).AddTicks(6720), "$2a$11$Gay24O4kWCLz4bWM68Zl3OzmmnEB2ZPpcB/ZQIJt6YM6owPixpIPG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 15, 57, 44, 613, DateTimeKind.Utc).AddTicks(6276), "$2a$11$Ptw6LGIi3XipOa72dZ0Rw.myr3hwfQbjq3Gj4lGPkyoZYNu2nqXNq" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 10, 23, 15, 975, DateTimeKind.Utc).AddTicks(721));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 10, 23, 15, 975, DateTimeKind.Utc).AddTicks(728));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 10, 23, 15, 975, DateTimeKind.Utc).AddTicks(730));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 10, 23, 16, 552, DateTimeKind.Utc).AddTicks(9914));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 10, 23, 16, 552, DateTimeKind.Utc).AddTicks(9919));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 10, 23, 16, 552, DateTimeKind.Utc).AddTicks(9921));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 10, 23, 16, 552, DateTimeKind.Utc).AddTicks(9923));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 10, 23, 16, 552, DateTimeKind.Utc).AddTicks(9924));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 10, 23, 16, 552, DateTimeKind.Utc).AddTicks(9925));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 10, 23, 16, 552, DateTimeKind.Utc).AddTicks(9928));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 10, 23, 15, 975, DateTimeKind.Utc).AddTicks(758));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 10, 23, 15, 975, DateTimeKind.Utc).AddTicks(761));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 10, 23, 15, 975, DateTimeKind.Utc).AddTicks(763));

            migrationBuilder.UpdateData(
                table: "StatementStatuses",
                keyColumn: "StatusID",
                keyValue: 3,
                column: "StatusName",
                value: "Sent");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 10, 23, 16, 95, DateTimeKind.Utc).AddTicks(2536), "$2a$11$EGJiADTRQ54RDoJUTxZlmOJ0/ezEjvgHFYGCDL93NEyElexeDliwS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 10, 23, 16, 209, DateTimeKind.Utc).AddTicks(7330), "$2a$11$BeqqsNSAZ2WqV/SSdQ9uOeM4L715hS8PHyqqyIYeq7AmqfBh8psIe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 10, 23, 16, 325, DateTimeKind.Utc).AddTicks(5388), "$2a$11$U/P.13w7AkSbgbHWE.LoD.zrQMlzCWar88galFeogTXnppN2kDZIS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 10, 23, 16, 441, DateTimeKind.Utc).AddTicks(8793), "$2a$11$2/63lsAncRgVY/eH/zyS1u7z9jYqgiuSlbhY8dE3y8mg9cgvEIwnC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 10, 23, 16, 552, DateTimeKind.Utc).AddTicks(8334), "$2a$11$4XQ8O6rOd4nwo2ldpgGRmuU/RPBwr5TE.W8ZrCSGJkSDbq6/NvXOO" });
        }
    }
}
