using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 31, 18, 27, 0, 813, DateTimeKind.Utc).AddTicks(1212));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 31, 18, 27, 0, 813, DateTimeKind.Utc).AddTicks(1226));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 31, 18, 27, 0, 813, DateTimeKind.Utc).AddTicks(1229));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 31, 18, 27, 1, 383, DateTimeKind.Utc).AddTicks(6557));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 31, 18, 27, 1, 383, DateTimeKind.Utc).AddTicks(6562));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 31, 18, 27, 1, 383, DateTimeKind.Utc).AddTicks(6564));

            migrationBuilder.InsertData(
                table: "NotificationStatuses",
                columns: new[] { "StatusID", "StatusName" },
                values: new object[,]
                {
                    { 1, "Chưa đọc" },
                    { 2, "Đã đọc" },
                    { 3, "Đã lưu trữ" }
                });

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "TypeID", "TypeName" },
                values: new object[,]
                {
                    { 1, "Hệ thống" },
                    { 2, "Hóa đơn" },
                    { 3, "Thanh toán" },
                    { 4, "Nhắc nhở" },
                    { 5, "Phê duyệt" },
                    { 6, "Báo cáo" },
                    { 7, "Bảo mật" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 31, 18, 27, 0, 813, DateTimeKind.Utc).AddTicks(1263));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 31, 18, 27, 0, 813, DateTimeKind.Utc).AddTicks(1266));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 31, 18, 27, 0, 813, DateTimeKind.Utc).AddTicks(1269));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 31, 18, 27, 0, 930, DateTimeKind.Utc).AddTicks(4581), "$2a$11$aKGez5cU9WCNsA2ep.10h.rmVBJ.6gYLkn/Ovrquejap/L1ShuLQ." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 31, 18, 27, 1, 46, DateTimeKind.Utc).AddTicks(2879), "$2a$11$9tepChIA0gfMgjnf/Lo6cu3FRoRxtFIjrAmHfo608GknObLl9ZYie" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 31, 18, 27, 1, 159, DateTimeKind.Utc).AddTicks(303), "$2a$11$MEIYIpiX06Xh4W2x2Kj6AO/POER08Y71vhxMrmgYEcMvqifZlqCJ6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 31, 18, 27, 1, 271, DateTimeKind.Utc).AddTicks(1173), "$2a$11$iCVB4wKd0/pWKZD1eerEWOLn.ITDK0HPbGLOdcbs0RfPeg2W7.tVe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 31, 18, 27, 1, 381, DateTimeKind.Utc).AddTicks(7331), "$2a$11$m/staNvXBT8RdhGuFub5me8ONwjr0bzQovlVCjU.U9.WWgDywri4O" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NotificationStatuses",
                keyColumn: "StatusID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "NotificationStatuses",
                keyColumn: "StatusID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "NotificationStatuses",
                keyColumn: "StatusID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "NotificationTypes",
                keyColumn: "TypeID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "NotificationTypes",
                keyColumn: "TypeID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "NotificationTypes",
                keyColumn: "TypeID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "NotificationTypes",
                keyColumn: "TypeID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "NotificationTypes",
                keyColumn: "TypeID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "NotificationTypes",
                keyColumn: "TypeID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "NotificationTypes",
                keyColumn: "TypeID",
                keyValue: 7);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 30, 14, 15, 11, 112, DateTimeKind.Utc).AddTicks(9107));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 30, 14, 15, 11, 112, DateTimeKind.Utc).AddTicks(9126));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 30, 14, 15, 11, 112, DateTimeKind.Utc).AddTicks(9129));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 30, 15, 32, 41, 151, DateTimeKind.Utc).AddTicks(7328));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 30, 15, 32, 41, 151, DateTimeKind.Utc).AddTicks(7334));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 30, 15, 32, 41, 151, DateTimeKind.Utc).AddTicks(7336));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 30, 14, 15, 11, 112, DateTimeKind.Utc).AddTicks(9258));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 30, 14, 15, 11, 112, DateTimeKind.Utc).AddTicks(9263));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 30, 14, 15, 11, 112, DateTimeKind.Utc).AddTicks(9266));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 30, 14, 15, 11, 294, DateTimeKind.Utc).AddTicks(160), "$2a$11$evbsCtbp7em2F/P8.IxgYu.b7y43o0zPKOjZSzcI5V8gOmsI/u2M." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 30, 14, 15, 11, 472, DateTimeKind.Utc).AddTicks(6516), "$2a$11$CxUOuTbAVL5UqDr8TmrLUOTBeBzueZDfJC/CyS42ld7mvDgCk7LPG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 30, 14, 15, 11, 663, DateTimeKind.Utc).AddTicks(1481), "$2a$11$yo3GbPBGyLBKUt4b5xp8luMYRArPPeieukCl9jJJtktfdTJZ/Ytyu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 30, 14, 15, 11, 861, DateTimeKind.Utc).AddTicks(7413), "$2a$11$inbNnnWHv69/PoE7in87/uJrpEwNzcAVvY7FUEr7xMIppH34T5Spe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 30, 14, 15, 12, 47, DateTimeKind.Utc).AddTicks(4602), "$2a$11$G14ruG7VqTQKQr8X9H.4IebPSvG4IhBzVPEUgyi10s9GhfI2VK4om" });
        }
    }
}
