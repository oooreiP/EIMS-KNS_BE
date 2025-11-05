using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MoveCurrentNumberToSerial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentInvoiceNumber",
                table: "InvoiceTemplates");

            migrationBuilder.AddColumn<long>(
                name: "CurrentInvoiceNumber",
                table: "Serials",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentInvoiceNumber",
                table: "Serials");

            migrationBuilder.AddColumn<long>(
                name: "CurrentInvoiceNumber",
                table: "InvoiceTemplates",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 5, 30, 1, 699, DateTimeKind.Utc).AddTicks(5429));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 5, 30, 1, 699, DateTimeKind.Utc).AddTicks(5441));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 5, 30, 1, 699, DateTimeKind.Utc).AddTicks(5444));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 5, 30, 1, 699, DateTimeKind.Utc).AddTicks(5486));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 5, 30, 1, 699, DateTimeKind.Utc).AddTicks(5490));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 5, 30, 1, 699, DateTimeKind.Utc).AddTicks(5493));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 5, 30, 1, 860, DateTimeKind.Utc).AddTicks(9547), "$2a$11$IdOUIcQXzH8S7i1GjwhdEeVACPIaWV1dXzZxZofa8TCLySKDXIuBS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 5, 30, 2, 17, DateTimeKind.Utc).AddTicks(8884), "$2a$11$uAhTx/vGVfGZoi2CNEUMQ.N99YN4NdxPhLtmFoAFje12PirEBr1Ge" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 5, 30, 2, 172, DateTimeKind.Utc).AddTicks(1658), "$2a$11$pw6Za9/TbxZzeAt7nKlNLuGPCQ2UM5SEMl9hYEQesSlUwyaSmEU3C" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 5, 30, 2, 326, DateTimeKind.Utc).AddTicks(9151), "$2a$11$HWOR6KA6UwljSqBTa96PquH5AOBQXGs98kOqXLTz7.V2VpCjDOLHK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 5, 30, 2, 521, DateTimeKind.Utc).AddTicks(7372), "$2a$11$/5Puo6XI/rJToASlT5zy9.TlCnWG4NDew9j8rA6t49t.6FzwH5wnO" });
        }
    }
}
