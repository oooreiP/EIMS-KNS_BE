using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FilePathForStatement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "InvoiceStatements",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 26, 4, 46, 18, 888, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 26, 4, 46, 18, 888, DateTimeKind.Utc).AddTicks(1846));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 26, 4, 46, 18, 888, DateTimeKind.Utc).AddTicks(1849));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 4, 46, 19, 467, DateTimeKind.Utc).AddTicks(7988));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 4, 46, 19, 467, DateTimeKind.Utc).AddTicks(7993));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 4, 46, 19, 467, DateTimeKind.Utc).AddTicks(7995));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 4, 46, 19, 467, DateTimeKind.Utc).AddTicks(7996));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 4, 46, 19, 467, DateTimeKind.Utc).AddTicks(7998));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 4, 46, 19, 467, DateTimeKind.Utc).AddTicks(7999));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 4, 46, 19, 467, DateTimeKind.Utc).AddTicks(8001));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 26, 4, 46, 18, 888, DateTimeKind.Utc).AddTicks(1985));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 26, 4, 46, 18, 888, DateTimeKind.Utc).AddTicks(1989));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 26, 4, 46, 18, 888, DateTimeKind.Utc).AddTicks(1991));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 26, 4, 46, 19, 2, DateTimeKind.Utc).AddTicks(7933), "$2a$11$DpPQwOGUfYN.lThMkvwbR.9GK1E9.iMqVemTL.d.3XqbJVmYJTmSi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 26, 4, 46, 19, 119, DateTimeKind.Utc).AddTicks(6223), "$2a$11$k2EKODcsXsBvIns6J4rb4u2E6q.we1qouRJcduAtrT8t91957U.Qq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 26, 4, 46, 19, 239, DateTimeKind.Utc).AddTicks(8930), "$2a$11$unSYploM7b5zOsED9gWMF.uTwcNg7HJd64PCciVBaRwK0W7d50ULS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 26, 4, 46, 19, 356, DateTimeKind.Utc).AddTicks(5916), "$2a$11$bHnRfNr91orBsbKhPAe02.Xz5rFuQP2JRWW1DDW4AWq5rzJj7JFLy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 26, 4, 46, 19, 467, DateTimeKind.Utc).AddTicks(6591), "$2a$11$MQnsXA6h/sWqQXfOtMM6ieYDe2t60KxcgcSPFweKlWnXQ1ZxCkNVu" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "InvoiceStatements");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 8, 39, 7, 972, DateTimeKind.Utc).AddTicks(3548));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 8, 39, 7, 972, DateTimeKind.Utc).AddTicks(3558));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 8, 39, 7, 972, DateTimeKind.Utc).AddTicks(3561));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 25, 8, 39, 8, 760, DateTimeKind.Utc).AddTicks(4776));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 25, 8, 39, 8, 760, DateTimeKind.Utc).AddTicks(4789));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 25, 8, 39, 8, 760, DateTimeKind.Utc).AddTicks(4794));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 25, 8, 39, 8, 760, DateTimeKind.Utc).AddTicks(4799));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 25, 8, 39, 8, 760, DateTimeKind.Utc).AddTicks(4804));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 25, 8, 39, 8, 760, DateTimeKind.Utc).AddTicks(4808));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 25, 8, 39, 8, 760, DateTimeKind.Utc).AddTicks(4816));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 8, 39, 7, 972, DateTimeKind.Utc).AddTicks(3615));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 8, 39, 7, 972, DateTimeKind.Utc).AddTicks(3622));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 8, 39, 7, 972, DateTimeKind.Utc).AddTicks(3625));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 25, 8, 39, 8, 131, DateTimeKind.Utc).AddTicks(8176), "$2a$11$OjLTslJY203coLtRBhBTJu2kxxLFn9KzE1kFTmucpJ.ANXgEzjZy6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 25, 8, 39, 8, 289, DateTimeKind.Utc).AddTicks(9897), "$2a$11$Cz6DnNXJQ/fr3xNO9Ojl/u0UcuTN3WRNalLQ3QGPr6XuI3W92FscK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 25, 8, 39, 8, 448, DateTimeKind.Utc).AddTicks(7086), "$2a$11$maBToXuQSZXQpV3YuiTuueilouHEBKI0bRNFI4BbIb43OspESZo2W" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 25, 8, 39, 8, 605, DateTimeKind.Utc).AddTicks(5082), "$2a$11$Mq2d3/unrWM8WbrlgHztW.MMFLjmR9vaa7hkZa2qEapJf8/Oo1wwu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 25, 8, 39, 8, 760, DateTimeKind.Utc).AddTicks(683), "$2a$11$cCYIeThzScfAcF0KEauy7.HNJi43qXrLaqy27Gx3bnOxD6HuX9sve" });
        }
    }
}
