using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MinuteInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MinuteInvoices",
                columns: table => new
                {
                    MinutesInvoiceId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvoiceId = table.Column<int>(type: "integer", nullable: false),
                    MinuteCode = table.Column<string>(type: "text", nullable: false),
                    MinutesType = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    IsSellerSigned = table.Column<bool>(type: "boolean", nullable: false),
                    SellerSignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsBuyerSigned = table.Column<bool>(type: "boolean", nullable: false),
                    BuyerSignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinuteInvoices", x => x.MinutesInvoiceId);
                    table.ForeignKey(
                        name: "FK_MinuteInvoices_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MinuteInvoices_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 6, 28, 49, DateTimeKind.Utc).AddTicks(8437));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 6, 28, 49, DateTimeKind.Utc).AddTicks(8446));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 6, 28, 49, DateTimeKind.Utc).AddTicks(8449));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 6, 28, 638, DateTimeKind.Utc).AddTicks(3990));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 6, 28, 638, DateTimeKind.Utc).AddTicks(3994));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 6, 28, 638, DateTimeKind.Utc).AddTicks(3996));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 6, 28, 638, DateTimeKind.Utc).AddTicks(3998));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 6, 28, 638, DateTimeKind.Utc).AddTicks(3999));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 6, 28, 638, DateTimeKind.Utc).AddTicks(4001));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 6, 28, 638, DateTimeKind.Utc).AddTicks(4003));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 6, 28, 49, DateTimeKind.Utc).AddTicks(8489));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 6, 28, 49, DateTimeKind.Utc).AddTicks(8494));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 6, 28, 49, DateTimeKind.Utc).AddTicks(8496));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 19, 6, 28, 170, DateTimeKind.Utc).AddTicks(4243), "$2a$11$VEHmpGSxv.O1Y61lsvNBTuZzn7xXjugzAF877U.6Uu/xfL6TgGGMq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 19, 6, 28, 289, DateTimeKind.Utc).AddTicks(6272), "$2a$11$KVGwktpp4hkQMN50NIQmsehhOn6NDpka9LE//jh8m0o06YJDsKfo." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 19, 6, 28, 404, DateTimeKind.Utc).AddTicks(5325), "$2a$11$Lcp2C169xUt5C3xqPi398O0DLstBYo.3i7XI5PfysWiLePa0HDRzu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 19, 6, 28, 519, DateTimeKind.Utc).AddTicks(827), "$2a$11$Wr8YQcXVOcXHpQRk19XKaOutPW0jcrokiZMew3UcFvXq0b2MPCuyK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 19, 6, 28, 638, DateTimeKind.Utc).AddTicks(2608), "$2a$11$zGypV4GY0Im8GpUs6pHjluL2FEzq0sPjcn70RqHeLwdrj26Mme6iq" });

            migrationBuilder.CreateIndex(
                name: "IX_MinuteInvoices_CreatedBy",
                table: "MinuteInvoices",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MinuteInvoices_InvoiceId",
                table: "MinuteInvoices",
                column: "InvoiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MinuteInvoices");

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
