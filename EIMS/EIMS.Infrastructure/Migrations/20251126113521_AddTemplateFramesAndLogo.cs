using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTemplateFramesAndLogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceTemplates_TemplateFrames_TemplateFrameFrameID",
                table: "InvoiceTemplates");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceTemplates_TemplateFrameFrameID",
                table: "InvoiceTemplates");

            migrationBuilder.DropColumn(
                name: "TemplateFrameFrameID",
                table: "InvoiceTemplates");

            migrationBuilder.AddColumn<string>(
                name: "LayoutDefinition",
                table: "InvoiceTemplates",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "InvoiceTemplates",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TemplateFrameID",
                table: "InvoiceTemplates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 11, 35, 19, 462, DateTimeKind.Utc).AddTicks(8894));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 11, 35, 19, 462, DateTimeKind.Utc).AddTicks(8905));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 11, 35, 19, 462, DateTimeKind.Utc).AddTicks(8908));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 11, 35, 19, 462, DateTimeKind.Utc).AddTicks(8970));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 11, 35, 19, 462, DateTimeKind.Utc).AddTicks(8975));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 11, 35, 19, 462, DateTimeKind.Utc).AddTicks(8979));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 11, 35, 19, 616, DateTimeKind.Utc).AddTicks(9062), "$2a$11$JbpB7uHjffVkB.l5B/k.QOiS57YDskVBwJOu505KqHjA9Xr0bRJFu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 11, 35, 19, 770, DateTimeKind.Utc).AddTicks(434), "$2a$11$OE6htJLkwMHvvHSjinTbwOG0hxNUZVmuttaszWlbumhoFLcjRCWfy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 11, 35, 19, 924, DateTimeKind.Utc).AddTicks(4212), "$2a$11$zCppPaBtMf83BVPhMJ6WRO2TPr2lzscM7iPjc1iuiNstDAEgOtvJ." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 11, 35, 20, 77, DateTimeKind.Utc).AddTicks(195), "$2a$11$JvCfN4d/RZiuWNokTCeZMuNVCMxcwzXdhukhgfy64cRP/chHHoWm2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 11, 35, 20, 231, DateTimeKind.Utc).AddTicks(3759), "$2a$11$xt/PXiFI/vFPG/0b5PIVIekab6K80873H64XwnQHoh26St7jrSMm6" });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceTemplates_TemplateFrameID",
                table: "InvoiceTemplates",
                column: "TemplateFrameID");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceTemplates_TemplateFrames_TemplateFrameID",
                table: "InvoiceTemplates",
                column: "TemplateFrameID",
                principalTable: "TemplateFrames",
                principalColumn: "FrameID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceTemplates_TemplateFrames_TemplateFrameID",
                table: "InvoiceTemplates");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceTemplates_TemplateFrameID",
                table: "InvoiceTemplates");

            migrationBuilder.DropColumn(
                name: "LayoutDefinition",
                table: "InvoiceTemplates");

            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "InvoiceTemplates");

            migrationBuilder.DropColumn(
                name: "TemplateFrameID",
                table: "InvoiceTemplates");

            migrationBuilder.AddColumn<int>(
                name: "TemplateFrameFrameID",
                table: "InvoiceTemplates",
                type: "integer",
                nullable: true);

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
    }
}
