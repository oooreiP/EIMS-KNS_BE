using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixFKSerial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerialStatusID",
                table: "InvoiceTemplates");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 7, 9, 36, 40, DateTimeKind.Utc).AddTicks(1142));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 7, 9, 36, 40, DateTimeKind.Utc).AddTicks(1153));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 7, 9, 36, 40, DateTimeKind.Utc).AddTicks(1157));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 7, 9, 36, 40, DateTimeKind.Utc).AddTicks(1221));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 7, 9, 36, 40, DateTimeKind.Utc).AddTicks(1226));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 7, 9, 36, 40, DateTimeKind.Utc).AddTicks(1230));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 7, 9, 36, 213, DateTimeKind.Utc).AddTicks(6260), "$2a$11$RekL3Ti5cP6cOt1QHaC21.JQlXOY77iFYfjYt/R/ipnPtxo/rOxIK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 7, 9, 36, 369, DateTimeKind.Utc).AddTicks(7335), "$2a$11$.WoCtXYOKI0fGk7J7r6wYuZiUdPXlxqMaBrBj1mIjnr5Ijh5r4vFa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 7, 9, 36, 526, DateTimeKind.Utc).AddTicks(8687), "$2a$11$qPdM5tBCS1HR56W1h1p4/elf18Mishy2kmLWSVVm7t.fDjdaEXKqq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 7, 9, 36, 683, DateTimeKind.Utc).AddTicks(6671), "$2a$11$xwIX3EAxwcxPffzfAmRZoePvox6ElKa7zbdHyCPuZHtpQEcRoSEBO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 7, 9, 36, 847, DateTimeKind.Utc).AddTicks(3212), "$2a$11$6mUv/U8XDy7hlR9ExMJTEOPQYB1bvOxiCB.NtmzORVG5pJMcBdzgq" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SerialStatusID",
                table: "InvoiceTemplates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 6, 34, 29, 50, DateTimeKind.Utc).AddTicks(7358));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 6, 34, 29, 50, DateTimeKind.Utc).AddTicks(7367));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 6, 34, 29, 50, DateTimeKind.Utc).AddTicks(7370));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 6, 34, 29, 50, DateTimeKind.Utc).AddTicks(7421));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 6, 34, 29, 50, DateTimeKind.Utc).AddTicks(7463));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 6, 34, 29, 50, DateTimeKind.Utc).AddTicks(7466));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 6, 34, 29, 236, DateTimeKind.Utc).AddTicks(6510), "$2a$11$oOcC9K8GD27MynUJW3kvIeS9AE1o082D4zvEhUypw6mMOXz1..2pO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 6, 34, 29, 392, DateTimeKind.Utc).AddTicks(4167), "$2a$11$.2RsKdbec5DcQvBetmFMZeH6dXh/w6sBtz0BGLh8ZLPeteVhOx2me" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 6, 34, 29, 556, DateTimeKind.Utc).AddTicks(3184), "$2a$11$zs6yUK0eU80/7pLRdIWAbOe7SGZ6mJugKrQSs9RlI9X.M5vEOA2EG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 6, 34, 29, 712, DateTimeKind.Utc).AddTicks(7415), "$2a$11$9df/4IH9W0RLJKxvCBschua.I5uK0cySl5SN/ygvwKHvV1x9/j93m" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 6, 34, 29, 874, DateTimeKind.Utc).AddTicks(9389), "$2a$11$yYgt7ob1H5fM5OqtCmYvW.xHTvgRQ3aQira0YWL.k3Q97MjiJ2iZ." });
        }
    }
}
