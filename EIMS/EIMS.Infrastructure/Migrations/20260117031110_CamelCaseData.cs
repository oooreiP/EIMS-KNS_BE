using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CamelCaseData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 17, 3, 11, 4, 868, DateTimeKind.Utc).AddTicks(7981));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 17, 3, 11, 4, 868, DateTimeKind.Utc).AddTicks(8020));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 17, 3, 11, 4, 868, DateTimeKind.Utc).AddTicks(8027));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 3, 11, 6, 41, DateTimeKind.Utc).AddTicks(8883));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 3, 11, 6, 41, DateTimeKind.Utc).AddTicks(8890));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 3, 11, 6, 41, DateTimeKind.Utc).AddTicks(8892));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 3, 11, 6, 41, DateTimeKind.Utc).AddTicks(8895));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 3, 11, 6, 41, DateTimeKind.Utc).AddTicks(8897));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 3, 11, 6, 41, DateTimeKind.Utc).AddTicks(8899));

            migrationBuilder.UpdateData(
                table: "InvoiceStatuses",
                keyColumn: "InvoiceStatusID",
                keyValue: 10,
                column: "StatusName",
                value: "AdjustmentInProcess");

            migrationBuilder.UpdateData(
                table: "InvoiceStatuses",
                keyColumn: "InvoiceStatusID",
                keyValue: 11,
                column: "StatusName",
                value: "ReplacementInProcess");

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "PaymentStatusID",
                keyValue: 2,
                column: "StatusName",
                value: "PartiallyPaid");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 17, 3, 11, 4, 868, DateTimeKind.Utc).AddTicks(8140));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 17, 3, 11, 4, 868, DateTimeKind.Utc).AddTicks(8147));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 17, 3, 11, 4, 868, DateTimeKind.Utc).AddTicks(8154));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 17, 3, 11, 5, 147, DateTimeKind.Utc).AddTicks(2377), "$2a$11$mdUITCyNSK7fkDJMBXEIsuqh70xFzGQwEj6VKng8Igt2RBOoTFfCi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 17, 3, 11, 5, 388, DateTimeKind.Utc).AddTicks(3055), "$2a$11$45FLJlvnaJlxyUruwLLOruUY/UCI12wufXhGsH0lAUXjM2xDrTXie" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 17, 3, 11, 5, 585, DateTimeKind.Utc).AddTicks(8422), "$2a$11$k96CwOEO8bQq4xlKsSHV2eMJltYxP6Rr4fZj2GHLemLSu7O94qC1C" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 17, 3, 11, 5, 821, DateTimeKind.Utc).AddTicks(2576), "$2a$11$kbGus920GpTojFBichJD7.8wvYrZAHUYw0/4k4PcBBuqbr3zP6JcG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 17, 3, 11, 6, 36, DateTimeKind.Utc).AddTicks(1095), "$2a$11$aZkjbubNCrFMQl2eaIe7hO/mZdWqqsvcgghpCYLFzLXcgpP0QLX7y" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 15, 10, 7, 46, 767, DateTimeKind.Utc).AddTicks(6583));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 15, 10, 7, 46, 767, DateTimeKind.Utc).AddTicks(6589));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 15, 10, 7, 46, 767, DateTimeKind.Utc).AddTicks(6592));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 15, 10, 7, 47, 332, DateTimeKind.Utc).AddTicks(201));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 15, 10, 7, 47, 332, DateTimeKind.Utc).AddTicks(205));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 15, 10, 7, 47, 332, DateTimeKind.Utc).AddTicks(207));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 15, 10, 7, 47, 332, DateTimeKind.Utc).AddTicks(209));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 15, 10, 7, 47, 332, DateTimeKind.Utc).AddTicks(210));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 15, 10, 7, 47, 332, DateTimeKind.Utc).AddTicks(211));

            migrationBuilder.UpdateData(
                table: "InvoiceStatuses",
                keyColumn: "InvoiceStatusID",
                keyValue: 10,
                column: "StatusName",
                value: "Adjustment_in_process");

            migrationBuilder.UpdateData(
                table: "InvoiceStatuses",
                keyColumn: "InvoiceStatusID",
                keyValue: 11,
                column: "StatusName",
                value: "Replacement_in_process");

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "PaymentStatusID",
                keyValue: 2,
                column: "StatusName",
                value: "Partially Paid");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 15, 10, 7, 46, 767, DateTimeKind.Utc).AddTicks(6623));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 15, 10, 7, 46, 767, DateTimeKind.Utc).AddTicks(6626));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 15, 10, 7, 46, 767, DateTimeKind.Utc).AddTicks(6629));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 15, 10, 7, 46, 881, DateTimeKind.Utc).AddTicks(4210), "$2a$11$seABg1EseKKJn6bRe1NGX.grF6LVbV2CfQ5WtkWweIhVQdDAFDFEm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 15, 10, 7, 46, 994, DateTimeKind.Utc).AddTicks(8521), "$2a$11$aVhSnRXAOwSu0EA9RcOkOuDqXEYTpXz7nyzBdgEj/xdavrbWvCcYm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 15, 10, 7, 47, 107, DateTimeKind.Utc).AddTicks(1103), "$2a$11$9iGYtujVBrpH6fJmFpG5ve7NGLMpjpLILBOjLgUo76DLY60bHfrAW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 15, 10, 7, 47, 219, DateTimeKind.Utc).AddTicks(1243), "$2a$11$IPrQmlyt/S5AYGlCF5wfZOofE42MiEfANoXEHmTf8gU/D228WCUfG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 15, 10, 7, 47, 330, DateTimeKind.Utc).AddTicks(2429), "$2a$11$LHBRVVodE25wXsjSFdCsr.TaoIfQMDLgo4x26TqBd8LjxVfCwfy.O" });
        }
    }
}
