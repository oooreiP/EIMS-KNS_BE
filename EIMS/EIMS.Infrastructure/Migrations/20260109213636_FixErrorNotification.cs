using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixErrorNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NotificationID",
                table: "InvoiceErrorNotifications",
                newName: "InvoiceErrorNotificationID");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 21, 36, 35, 430, DateTimeKind.Utc).AddTicks(5315));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 21, 36, 35, 430, DateTimeKind.Utc).AddTicks(5328));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 21, 36, 35, 430, DateTimeKind.Utc).AddTicks(5331));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 9, 21, 36, 35, 995, DateTimeKind.Utc).AddTicks(5896));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 9, 21, 36, 35, 995, DateTimeKind.Utc).AddTicks(5901));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 9, 21, 36, 35, 995, DateTimeKind.Utc).AddTicks(5903));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 21, 36, 35, 430, DateTimeKind.Utc).AddTicks(5373));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 21, 36, 35, 430, DateTimeKind.Utc).AddTicks(5376));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 21, 36, 35, 430, DateTimeKind.Utc).AddTicks(5434));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 21, 36, 35, 546, DateTimeKind.Utc).AddTicks(8147), "$2a$11$ZW5vpsDxHOQI20Ovuv0.NeHoKMw5sFwxRwkhhSsnB/3Kid/ZSNXAS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 21, 36, 35, 659, DateTimeKind.Utc).AddTicks(9121), "$2a$11$VAAR6ro9zaoTMciYs5oNp.WhsxbRoqW2krlJPsD/dsvat8M4qVlKK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 21, 36, 35, 770, DateTimeKind.Utc).AddTicks(8297), "$2a$11$EbwNJ8WcRODlQZKMexwbhu1HDbPk4Ez.3uUZFyS7cRFEpwFEdspMO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 21, 36, 35, 881, DateTimeKind.Utc).AddTicks(9965), "$2a$11$VSI2hVZK5kjFfJCIS7vkl.vf8qnEs1So/l7n6/M4tH0d.yqJBXxK2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 21, 36, 35, 994, DateTimeKind.Utc).AddTicks(1016), "$2a$11$dU3omGlriSRrQd63PfjjWuMBXgy8uZAFztAcolP8ygrW3kF60oGBG" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InvoiceErrorNotificationID",
                table: "InvoiceErrorNotifications",
                newName: "NotificationID");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 46, 13, 965, DateTimeKind.Utc).AddTicks(5480));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 46, 13, 965, DateTimeKind.Utc).AddTicks(5492));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 46, 13, 965, DateTimeKind.Utc).AddTicks(5494));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 9, 20, 46, 14, 535, DateTimeKind.Utc).AddTicks(7562));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 9, 20, 46, 14, 535, DateTimeKind.Utc).AddTicks(7567));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 9, 20, 46, 14, 535, DateTimeKind.Utc).AddTicks(7568));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 46, 13, 965, DateTimeKind.Utc).AddTicks(5533));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 46, 13, 965, DateTimeKind.Utc).AddTicks(5536));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 9, 20, 46, 13, 965, DateTimeKind.Utc).AddTicks(5539));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 46, 14, 79, DateTimeKind.Utc).AddTicks(2691), "$2a$11$ITK.gan2OpOkzOn5ht0Guuc5rms3zf7egfKNZxdZzf4bb39yJwtLa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 46, 14, 192, DateTimeKind.Utc).AddTicks(1096), "$2a$11$0Vbb4CRRPg59fG7N7BxBXeVHU13KkvGgSvZSeSMac9JySZV0spqlS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 46, 14, 305, DateTimeKind.Utc).AddTicks(7020), "$2a$11$iz2Gpka2/QMMe21nRj.R1urW.mp.bA32/Sm7vxZPKkJfau4sCUoVq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 46, 14, 420, DateTimeKind.Utc).AddTicks(2343), "$2a$11$O81qYsLtYKHR6bYzuBRj3.MmaoMkGvaeLOXkbtIdrXwAVH/7etyVW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 46, 14, 533, DateTimeKind.Utc).AddTicks(1320), "$2a$11$R7CHGo6eVyjoQvQCEns7MuIo9VscpDcQ1nK1F/2/x2c8HBCqSPvS." });
        }
    }
}
