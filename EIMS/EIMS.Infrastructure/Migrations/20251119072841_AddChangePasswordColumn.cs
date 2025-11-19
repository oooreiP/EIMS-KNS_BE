using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddChangePasswordColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPasswordChangeRequired",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 19, 7, 28, 36, 393, DateTimeKind.Utc).AddTicks(9049));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 19, 7, 28, 36, 393, DateTimeKind.Utc).AddTicks(9058));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 19, 7, 28, 36, 393, DateTimeKind.Utc).AddTicks(9061));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 19, 7, 28, 36, 393, DateTimeKind.Utc).AddTicks(9136));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 19, 7, 28, 36, 393, DateTimeKind.Utc).AddTicks(9141));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 19, 7, 28, 36, 393, DateTimeKind.Utc).AddTicks(9145));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "IsPasswordChangeRequired", "PasswordHash", "Status" },
                values: new object[] { new DateTime(2025, 11, 19, 7, 28, 36, 617, DateTimeKind.Utc).AddTicks(380), false, "$2a$11$.0/nXX9HaZ1poXkfzCy8HOW4n55EhWJqEfB48j6EifWTHG8xaSDfa", 2 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "IsPasswordChangeRequired", "PasswordHash", "Status" },
                values: new object[] { new DateTime(2025, 11, 19, 7, 28, 36, 814, DateTimeKind.Utc).AddTicks(7568), false, "$2a$11$Pwt1l/c38qyM2QXOCBPtiOjrdrqS1pv4YzB7xMrjXQI3qBX76uwtm", 2 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "IsPasswordChangeRequired", "PasswordHash", "Status" },
                values: new object[] { new DateTime(2025, 11, 19, 7, 28, 37, 8, DateTimeKind.Utc).AddTicks(2443), false, "$2a$11$5l.nfUGKMyNXoQBcVvwUiurHkLnYQQA1FpqakxlM0nnJH0P9racMS", 2 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "IsPasswordChangeRequired", "PasswordHash", "Status" },
                values: new object[] { new DateTime(2025, 11, 19, 7, 28, 37, 209, DateTimeKind.Utc).AddTicks(4112), false, "$2a$11$VYQ3/yQjBt5YQogDlG2yQe/OCzMstC7U4h52/U8Oq3mO/VbVadu1W", 2 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "IsPasswordChangeRequired", "PasswordHash", "Status" },
                values: new object[] { new DateTime(2025, 11, 19, 7, 28, 37, 415, DateTimeKind.Utc).AddTicks(4562), false, "$2a$11$2E4594PudZRyAlqqRPFGQOReXdMoS.JeK81RW3bGrssUXM3MT0HKi", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_TaxCode",
                table: "Customers",
                column: "TaxCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_TaxCode",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "IsPasswordChangeRequired",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 12, 8, 13, 10, 613, DateTimeKind.Utc).AddTicks(3316));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 12, 8, 13, 10, 613, DateTimeKind.Utc).AddTicks(3325));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 12, 8, 13, 10, 613, DateTimeKind.Utc).AddTicks(3328));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 12, 8, 13, 10, 613, DateTimeKind.Utc).AddTicks(3376));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 12, 8, 13, 10, 613, DateTimeKind.Utc).AddTicks(3380));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 12, 8, 13, 10, 613, DateTimeKind.Utc).AddTicks(3382));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Status" },
                values: new object[] { new DateTime(2025, 11, 12, 8, 13, 10, 766, DateTimeKind.Utc).AddTicks(8841), "$2a$11$r2Dy0weOpDhx200KMQotWOtAZnveRIwrapBXY3YGPcALz6jA8sBgC", 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash", "Status" },
                values: new object[] { new DateTime(2025, 11, 12, 8, 13, 10, 919, DateTimeKind.Utc).AddTicks(5958), "$2a$11$xGrjScEY1BI/H1fa36gWfOguEr50F3PaU6oDtJTwdJSjKabyRPe/W", 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash", "Status" },
                values: new object[] { new DateTime(2025, 11, 12, 8, 13, 11, 72, DateTimeKind.Utc).AddTicks(8468), "$2a$11$1erseGApFC5GllEZhxylXOPyE/Rag3mZO21TGwF0y7ZDoKvzgsn4i", 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash", "Status" },
                values: new object[] { new DateTime(2025, 11, 12, 8, 13, 11, 234, DateTimeKind.Utc).AddTicks(9703), "$2a$11$azCWLGrFhkMr9i9RM9UOaOsOVUA/W5cqjGOrkdwI4AbpofRwwpAZO", 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash", "Status" },
                values: new object[] { new DateTime(2025, 11, 12, 8, 13, 11, 388, DateTimeKind.Utc).AddTicks(3109), "$2a$11$zoF55HwYxLmPbRX6GKCTlenPHAujKFE6WfSSmgwpG3EVRettKu19W", 0 });
        }
    }
}
