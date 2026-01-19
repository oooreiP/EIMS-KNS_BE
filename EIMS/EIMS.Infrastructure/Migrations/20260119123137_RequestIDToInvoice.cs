using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RequestIDToInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequestID",
                table: "Invoices",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 19, 12, 31, 34, 178, DateTimeKind.Utc).AddTicks(9461));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 19, 12, 31, 34, 178, DateTimeKind.Utc).AddTicks(9468));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 19, 12, 31, 34, 178, DateTimeKind.Utc).AddTicks(9470));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 19, 12, 31, 34, 773, DateTimeKind.Utc).AddTicks(1415));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 19, 12, 31, 34, 773, DateTimeKind.Utc).AddTicks(1421));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 19, 12, 31, 34, 773, DateTimeKind.Utc).AddTicks(1423));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 19, 12, 31, 34, 773, DateTimeKind.Utc).AddTicks(1424));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 19, 12, 31, 34, 773, DateTimeKind.Utc).AddTicks(1425));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 19, 12, 31, 34, 773, DateTimeKind.Utc).AddTicks(1427));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 19, 12, 31, 34, 178, DateTimeKind.Utc).AddTicks(9504));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 19, 12, 31, 34, 178, DateTimeKind.Utc).AddTicks(9507));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 19, 12, 31, 34, 178, DateTimeKind.Utc).AddTicks(9509));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 19, 12, 31, 34, 296, DateTimeKind.Utc).AddTicks(8801), "$2a$11$L8lzuKIEsrDNu84yR5FQGO0K6J4eO.GdQB7XNDAg7QnDkGl3p3kki" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 19, 12, 31, 34, 417, DateTimeKind.Utc).AddTicks(5509), "$2a$11$zeUzreDAhfiqQKwLfiSLFugN.XnV1lL2fNsa1MRr9s7ZyEZTrXjWC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 19, 12, 31, 34, 537, DateTimeKind.Utc).AddTicks(2685), "$2a$11$rBhKbPLDnEquQyshqhZDOOU0F6MaLTk4NZhplZH8kOF.LBfXAxrfG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 19, 12, 31, 34, 653, DateTimeKind.Utc).AddTicks(3600), "$2a$11$K0Xz4fu4L/6mqcMcSKd5me4ovF31NqaJuRY4B3hk8Nl2j6qkvNtN6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 19, 12, 31, 34, 770, DateTimeKind.Utc).AddTicks(6453), "$2a$11$MAX3HURBOUfE1OEj.4yIget.1O0h68dXyu2YRzmp95Ap3/6YhsRei" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestID",
                table: "Invoices");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 19, 3, 1, 40, 492, DateTimeKind.Utc).AddTicks(5929));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 19, 3, 1, 40, 492, DateTimeKind.Utc).AddTicks(5934));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 19, 3, 1, 40, 492, DateTimeKind.Utc).AddTicks(5937));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 19, 3, 1, 41, 54, DateTimeKind.Utc).AddTicks(2420));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 19, 3, 1, 41, 54, DateTimeKind.Utc).AddTicks(2425));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 19, 3, 1, 41, 54, DateTimeKind.Utc).AddTicks(2427));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 19, 3, 1, 41, 54, DateTimeKind.Utc).AddTicks(2428));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 19, 3, 1, 41, 54, DateTimeKind.Utc).AddTicks(2463));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 19, 3, 1, 41, 54, DateTimeKind.Utc).AddTicks(2465));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 19, 3, 1, 40, 492, DateTimeKind.Utc).AddTicks(5967));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 19, 3, 1, 40, 492, DateTimeKind.Utc).AddTicks(5971));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 19, 3, 1, 40, 492, DateTimeKind.Utc).AddTicks(5974));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 19, 3, 1, 40, 603, DateTimeKind.Utc).AddTicks(1230), "$2a$11$XJ6UfZcHq7V2ZNI.HXnSsOG6i6PJCTgtTQX6URied3X8fZAasMTV2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 19, 3, 1, 40, 718, DateTimeKind.Utc).AddTicks(6102), "$2a$11$fi9kkkU9NsbY/bUs1T8MguxNY/LpbVKDHIexm.3Lrw/Vl2DOC1KUO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 19, 3, 1, 40, 829, DateTimeKind.Utc).AddTicks(2894), "$2a$11$nZwF4W5PW1nMbm9SCJ9XOO0.AF0u0YfYLo7LOfb0WdQnYaj.H16.W" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 19, 3, 1, 40, 941, DateTimeKind.Utc).AddTicks(7475), "$2a$11$qqha8q37IsuYWa.ogIMONO8nXs/ALEunlV8sQOnR.gbKPODVRs3JS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 19, 3, 1, 41, 52, DateTimeKind.Utc).AddTicks(961), "$2a$11$rQq3seCNWs5XcPe43UtTuuWotByIMwtTJ.sK9eWzCf.LBPcCEvWsO" });
        }
    }
}
