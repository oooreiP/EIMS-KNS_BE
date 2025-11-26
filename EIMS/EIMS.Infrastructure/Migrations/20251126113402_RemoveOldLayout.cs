using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOldLayout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LayoutDefinition",
                table: "InvoiceTemplates");

            migrationBuilder.AddColumn<int>(
                name: "TemplateFrameFrameID",
                table: "InvoiceTemplates",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TemplateFrames",
                columns: table => new
                {
                    FrameID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FrameName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateFrames", x => x.FrameID);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 11, 34, 0, 897, DateTimeKind.Utc).AddTicks(4182));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 11, 34, 0, 897, DateTimeKind.Utc).AddTicks(4193));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 11, 34, 0, 897, DateTimeKind.Utc).AddTicks(4196));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 11, 34, 0, 897, DateTimeKind.Utc).AddTicks(4256));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 11, 34, 0, 897, DateTimeKind.Utc).AddTicks(4260));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 11, 34, 0, 897, DateTimeKind.Utc).AddTicks(4263));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 11, 34, 1, 54, DateTimeKind.Utc).AddTicks(2458), "$2a$11$yd1LbmjAbxwRmoBgU92q2eXV9Jjms/yWlazzC2dMTRLEE5qsEB5Ya" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 11, 34, 1, 208, DateTimeKind.Utc).AddTicks(3182), "$2a$11$W1dTIwU1ZUp2MyHZ0AZCzeFDARCW/aQLMn3OU.nPKg4ToaBylLlhm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 11, 34, 1, 364, DateTimeKind.Utc).AddTicks(7234), "$2a$11$MP4S2259QtjlNLPDQ/6K1./.Yuxu4eIl3qYVSDRXjkwlU3kP5UqXy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 11, 34, 1, 524, DateTimeKind.Utc).AddTicks(2775), "$2a$11$7m5jxs8H3SYqU4LnWWgY6eThuN6g5WfcZ.ayJ4qzOHYQC00xdDSw6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 11, 34, 1, 681, DateTimeKind.Utc).AddTicks(9407), "$2a$11$Zpb.LwqGsgonJB2IZOkY4eOhhDY8TPKRhfiT3GE3SQ00D3Un3VEZC" });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceTemplates_TemplateFrameFrameID",
                table: "InvoiceTemplates",
                column: "TemplateFrameFrameID");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceTemplates_TemplateFrames_TemplateFrameFrameID",
                table: "InvoiceTemplates",
                column: "TemplateFrameFrameID",
                principalTable: "TemplateFrames",
                principalColumn: "FrameID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceTemplates_TemplateFrames_TemplateFrameFrameID",
                table: "InvoiceTemplates");

            migrationBuilder.DropTable(
                name: "TemplateFrames");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceTemplates_TemplateFrameFrameID",
                table: "InvoiceTemplates");

            migrationBuilder.DropColumn(
                name: "TemplateFrameFrameID",
                table: "InvoiceTemplates");

            migrationBuilder.AddColumn<string>(
                name: "LayoutDefinition",
                table: "InvoiceTemplates",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 21, 7, 46, 35, 174, DateTimeKind.Utc).AddTicks(3850));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 21, 7, 46, 35, 174, DateTimeKind.Utc).AddTicks(3865));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 21, 7, 46, 35, 174, DateTimeKind.Utc).AddTicks(3868));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 21, 7, 46, 35, 174, DateTimeKind.Utc).AddTicks(3940));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 21, 7, 46, 35, 174, DateTimeKind.Utc).AddTicks(3944));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 21, 7, 46, 35, 174, DateTimeKind.Utc).AddTicks(3948));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 21, 7, 46, 35, 378, DateTimeKind.Utc).AddTicks(5341), "$2a$11$VktNYVu0jZ9Rsjb2UdWGmOHviVmor5AEWRE/NI1Ok91OsF1NX9Yfq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 21, 7, 46, 35, 576, DateTimeKind.Utc).AddTicks(8776), "$2a$11$uvY.Wd.1n.H9AVDJBRanOu29qX.ztv6NRI/ho1sM3aCpMiEiXwQqS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 21, 7, 46, 35, 774, DateTimeKind.Utc).AddTicks(8554), "$2a$11$k24TrQGJX/ZGirB9OZ2QPu/OgskmtlFzfqWq6akNLptr9mIknuvU." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 21, 7, 46, 35, 968, DateTimeKind.Utc).AddTicks(7524), "$2a$11$rAczaG8d08kmivmIVM/b8uz1vo5EjrjrQMGcRmeIwfRSqbgjgQCjS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 21, 7, 46, 36, 193, DateTimeKind.Utc).AddTicks(4706), "$2a$11$93L1mhD6fChPdWJWMQmIougxd53dKWAlOUcVGTaZRudwzfz0QWjfa" });
        }
    }
}
