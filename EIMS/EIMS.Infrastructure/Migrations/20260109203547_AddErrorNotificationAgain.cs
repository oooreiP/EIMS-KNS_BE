using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddErrorNotificationAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceErrorNotifications",
                columns: table => new
                {
                    NotificationID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaxAuthorityCode = table.Column<string>(type: "text", nullable: true),
                    Place = table.Column<string>(type: "text", nullable: false),
                    ReportDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    XMLPath = table.Column<string>(type: "text", nullable: true),
                    SignedData = table.Column<string>(type: "text", nullable: true),
                    MTDiep = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceErrorNotifications", x => x.NotificationID);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceErrorDetails",
                columns: table => new
                {
                    DetailID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NotificationID = table.Column<int>(type: "integer", nullable: false),
                    InvoiceID = table.Column<int>(type: "integer", nullable: true),
                    InvoiceSerial = table.Column<string>(type: "text", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "text", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TaxCode = table.Column<string>(type: "text", nullable: false),
                    ErrorType = table.Column<int>(type: "integer", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceErrorDetails", x => x.DetailID);
                    table.ForeignKey(
                        name: "FK_InvoiceErrorDetails_InvoiceErrorNotifications_NotificationID",
                        column: x => x.NotificationID,
                        principalTable: "InvoiceErrorNotifications",
                        principalColumn: "NotificationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceErrorDetails_Invoices_InvoiceID",
                        column: x => x.InvoiceID,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceID");
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 35, 46, 247, DateTimeKind.Utc).AddTicks(7506));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 35, 46, 247, DateTimeKind.Utc).AddTicks(7514));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 35, 46, 247, DateTimeKind.Utc).AddTicks(7517));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 9, 20, 35, 46, 824, DateTimeKind.Utc).AddTicks(4060));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 9, 20, 35, 46, 824, DateTimeKind.Utc).AddTicks(4065));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 9, 20, 35, 46, 824, DateTimeKind.Utc).AddTicks(4067));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 35, 46, 247, DateTimeKind.Utc).AddTicks(7559));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 35, 46, 247, DateTimeKind.Utc).AddTicks(7562));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 35, 46, 247, DateTimeKind.Utc).AddTicks(7565));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 35, 46, 361, DateTimeKind.Utc).AddTicks(7805), "$2a$11$fSK2OdDWqfDr53URNSpDiO9g7cu5qfZUkgoAuk570RjOJ3EheV2Bm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 35, 46, 475, DateTimeKind.Utc).AddTicks(4362), "$2a$11$FVsTgQ3vDr/tnJOiuz2i/e7s4ho7lipiD0zHOMZ70JD.9kEPKl74C" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 35, 46, 593, DateTimeKind.Utc).AddTicks(2746), "$2a$11$E0jtvtkWy4JkV1nFV5HSCeHQXA9Y2rYNUV325zHHSatTBly17Rwz6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 35, 46, 711, DateTimeKind.Utc).AddTicks(2173), "$2a$11$YWVhq1yeT7Mwwjc1MOtM1eEpqEWFF7OostbVlbWz.9zOlbeGx/CLS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 35, 46, 822, DateTimeKind.Utc).AddTicks(1347), "$2a$11$ZYhBQ5TSb1dv.wfJLdRExOE6n/oQlfPM9YtaehGSCCJXByAOTUH7K" });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceErrorDetails_InvoiceID",
                table: "InvoiceErrorDetails",
                column: "InvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceErrorDetails_NotificationID",
                table: "InvoiceErrorDetails",
                column: "NotificationID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceErrorDetails");

            migrationBuilder.DropTable(
                name: "InvoiceErrorNotifications");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 16, 33, 975, DateTimeKind.Utc).AddTicks(8749));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 16, 33, 975, DateTimeKind.Utc).AddTicks(8755));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 16, 33, 975, DateTimeKind.Utc).AddTicks(8758));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 9, 20, 16, 34, 535, DateTimeKind.Utc).AddTicks(9520));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 9, 20, 16, 34, 535, DateTimeKind.Utc).AddTicks(9524));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 9, 20, 16, 34, 535, DateTimeKind.Utc).AddTicks(9526));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 16, 33, 975, DateTimeKind.Utc).AddTicks(8785));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 16, 33, 975, DateTimeKind.Utc).AddTicks(8788));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 16, 33, 975, DateTimeKind.Utc).AddTicks(8790));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 16, 34, 93, DateTimeKind.Utc).AddTicks(183), "$2a$11$jjVjCbg/78TbIOXxdnfDpO1CpZijPuNR05Hjlt41yJMCUBN9xjiQS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 16, 34, 204, DateTimeKind.Utc).AddTicks(1808), "$2a$11$/N4icTjbl7aHC70/Luqu2urLDvqb.tzrtT7z6CM7gvOnAGTtyPImq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 16, 34, 313, DateTimeKind.Utc).AddTicks(3092), "$2a$11$H5ynqrmdt78u2n6q.nMh8uXnYeJfYkdpAZrTNGx2XKMY4HGxYS9di" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 16, 34, 424, DateTimeKind.Utc).AddTicks(5422), "$2a$11$Z98n67qv6UPny9cs8s.UcO/1Jae8xudXlZN90XmLFWrwKnHeWIVgm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 16, 34, 534, DateTimeKind.Utc).AddTicks(1852), "$2a$11$qDD7rnQ28GZtALvO7fEDUu0nkIUSrzeWwFNgytgrhjNxKkLJ5P5KC" });
        }
    }
}
