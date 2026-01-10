using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ErrorNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceErrorNotifications",
                columns: table => new
                {
                    InvoiceErrorNotificationID = table.Column<int>(type: "integer", nullable: false)
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
                    table.PrimaryKey("PK_InvoiceErrorNotifications", x => x.InvoiceErrorNotificationID);
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
                        principalColumn: "InvoiceErrorNotificationID",
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
                value: new DateTime(2026, 1, 10, 9, 37, 51, 171, DateTimeKind.Utc).AddTicks(2784));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 9, 37, 51, 171, DateTimeKind.Utc).AddTicks(2794));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 9, 37, 51, 171, DateTimeKind.Utc).AddTicks(2796));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 10, 9, 37, 51, 761, DateTimeKind.Utc).AddTicks(2723));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 10, 9, 37, 51, 761, DateTimeKind.Utc).AddTicks(2729));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 10, 9, 37, 51, 761, DateTimeKind.Utc).AddTicks(2731));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 9, 37, 51, 171, DateTimeKind.Utc).AddTicks(2839));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 9, 37, 51, 171, DateTimeKind.Utc).AddTicks(2842));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 9, 37, 51, 171, DateTimeKind.Utc).AddTicks(2845));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 9, 37, 51, 283, DateTimeKind.Utc).AddTicks(8617), "$2a$11$1yXq4QkAQ.t4XnatZ7Z9n.C8vncYJXBa89fXue8c../TKnQj2dmDa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 9, 37, 51, 407, DateTimeKind.Utc).AddTicks(4891), "$2a$11$6fCdfpro0zU1XHtl.wKeZeFtVnLZ9QS5ULsmvI6odTkk9CWDLV6Wu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 9, 37, 51, 525, DateTimeKind.Utc).AddTicks(1928), "$2a$11$LLYBIWhvraxT50Dsmb1VAO1DvyFb/i.12VI2PMXXImJ8Cqc07JhfK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 9, 37, 51, 643, DateTimeKind.Utc).AddTicks(7818), "$2a$11$IIVmDyRDGU4woC9ttaezbOshF3pcZGuLn4tybvhAuzX.2SpP1Mn/u" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 9, 37, 51, 759, DateTimeKind.Utc).AddTicks(931), "$2a$11$aH6G4AQRB4kiZV0x9RjeG.fCUS3Kxlp3Xsfn6REE.kolKcOXfZy7y" });

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
                value: new DateTime(2026, 1, 10, 9, 17, 38, 351, DateTimeKind.Utc).AddTicks(2657));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 9, 17, 38, 351, DateTimeKind.Utc).AddTicks(2738));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 9, 17, 38, 351, DateTimeKind.Utc).AddTicks(2741));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 10, 9, 17, 38, 911, DateTimeKind.Utc).AddTicks(7817));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 10, 9, 17, 38, 911, DateTimeKind.Utc).AddTicks(7820));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 10, 9, 17, 38, 911, DateTimeKind.Utc).AddTicks(7822));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 9, 17, 38, 351, DateTimeKind.Utc).AddTicks(2777));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 9, 17, 38, 351, DateTimeKind.Utc).AddTicks(2780));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 9, 17, 38, 351, DateTimeKind.Utc).AddTicks(2782));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 9, 17, 38, 464, DateTimeKind.Utc).AddTicks(135), "$2a$11$OW3YJ3d4NCnWtyeisUriT.NDL1z4QzzbpzpK0mh24X6r/omh471VC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 9, 17, 38, 575, DateTimeKind.Utc).AddTicks(4469), "$2a$11$iUnZ9JWpqUctHPrlYY30vuJwWXwxU4u0b8vDBmPqcmS2XwR.tYOl2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 9, 17, 38, 687, DateTimeKind.Utc).AddTicks(7047), "$2a$11$vPXDj6z0BaYkwXJJ7T9npuUFXXQ0XVh57/KASt4zyHWS0aI6UcFOK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 9, 17, 38, 799, DateTimeKind.Utc).AddTicks(1042), "$2a$11$9vWQ5WkLATn5DXTYb74H.u1PI1UFnmc4FaybkOy4L93hGOi/CNUsu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 9, 17, 38, 910, DateTimeKind.Utc).AddTicks(1910), "$2a$11$n3cc6PlUyoqb6evDjDSRo.D.CvYHgL7S2hY1FPYw0LI4o51TWyIQ2" });
        }
    }
}
