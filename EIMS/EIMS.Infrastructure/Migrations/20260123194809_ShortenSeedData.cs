using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ShortenSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 48, 7, 920, DateTimeKind.Utc).AddTicks(1868));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 48, 7, 920, DateTimeKind.Utc).AddTicks(1876));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 48, 7, 920, DateTimeKind.Utc).AddTicks(1879));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 48, 8, 691, DateTimeKind.Utc).AddTicks(6826));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 48, 8, 691, DateTimeKind.Utc).AddTicks(6831));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 48, 8, 691, DateTimeKind.Utc).AddTicks(6834));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 48, 8, 691, DateTimeKind.Utc).AddTicks(6836));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 48, 8, 691, DateTimeKind.Utc).AddTicks(6839));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 48, 8, 691, DateTimeKind.Utc).AddTicks(6841));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 48, 8, 691, DateTimeKind.Utc).AddTicks(6844));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 48, 7, 920, DateTimeKind.Utc).AddTicks(1962));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 48, 7, 920, DateTimeKind.Utc).AddTicks(1966));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 48, 7, 920, DateTimeKind.Utc).AddTicks(1969));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 19, 48, 8, 74, DateTimeKind.Utc).AddTicks(2640), "$2a$11$bQsDV79SFDM4lb6lyBoZVukQrja.x1tvR5owPLbpVi11q97nTtwby" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 19, 48, 8, 227, DateTimeKind.Utc).AddTicks(3561), "$2a$11$jhexrJObLtmQVzmVSGvuG.D38fV4s/PAqsI2gGKUQL8iqdWKuM0di" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 19, 48, 8, 381, DateTimeKind.Utc).AddTicks(6144), "$2a$11$OMyR10Ckoxc.whJz03gy1ukQi7NoLLZZk/4p1kOZQf28muwjOawOW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 19, 48, 8, 534, DateTimeKind.Utc).AddTicks(6446), "$2a$11$4egIZNVCEbqDXDQEnYhbSuNc8IQxz8CY4U6gctO9k9uOF1JJ5jtn." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 19, 48, 8, 691, DateTimeKind.Utc).AddTicks(5113), "$2a$11$pQ55II7yll.YVXFQk.ZDA.k8XExjggNqffGnfzMesI0RY3XHwpZP2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 34, 12, 771, DateTimeKind.Utc).AddTicks(8839));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 34, 12, 771, DateTimeKind.Utc).AddTicks(8858));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 34, 12, 771, DateTimeKind.Utc).AddTicks(8861));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 34, 13, 727, DateTimeKind.Utc).AddTicks(4610));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 34, 13, 727, DateTimeKind.Utc).AddTicks(4615));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 34, 13, 727, DateTimeKind.Utc).AddTicks(4622));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 34, 13, 727, DateTimeKind.Utc).AddTicks(4624));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 34, 13, 727, DateTimeKind.Utc).AddTicks(4627));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 34, 13, 727, DateTimeKind.Utc).AddTicks(4629));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 34, 13, 727, DateTimeKind.Utc).AddTicks(4634));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 34, 12, 771, DateTimeKind.Utc).AddTicks(8933));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 34, 12, 771, DateTimeKind.Utc).AddTicks(8938));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 34, 12, 771, DateTimeKind.Utc).AddTicks(8942));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 19, 34, 12, 960, DateTimeKind.Utc).AddTicks(9145), "$2a$11$a8K4f23hJ3PUx3bWzyesZeWFef4.OGpGXTXw.fLBis9c0rbr94mzK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 19, 34, 13, 152, DateTimeKind.Utc).AddTicks(1955), "$2a$11$AyP.l.61ekHIlVbh8v2B9O.lpW5QkA.xvSu5F7/6C6oq77/ro6csS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 19, 34, 13, 334, DateTimeKind.Utc).AddTicks(8916), "$2a$11$7wdeyVciszMICp9kOYO5K.YyVOl/BOOhlyrn6DC877VJtuVjsQ7Xi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 19, 34, 13, 539, DateTimeKind.Utc).AddTicks(558), "$2a$11$0ftWlkPx/xpgslCwDtr6ruckmG.JUwJKzWUcLmk03zQlfaz0K8pWO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 19, 34, 13, 727, DateTimeKind.Utc).AddTicks(2415), "$2a$11$3uvurgZWsz/w63aVUXIhYejqeXtWj7pbzDVPMkz1ZLjErbVSG9LBa" });
        }
    }
}
