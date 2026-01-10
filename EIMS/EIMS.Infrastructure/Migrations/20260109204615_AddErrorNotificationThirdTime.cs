using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddErrorNotificationThirdTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
