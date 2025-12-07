using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeQuantityProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 4, 9, 48, 230, DateTimeKind.Utc).AddTicks(2022));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 4, 9, 48, 230, DateTimeKind.Utc).AddTicks(2028));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 4, 9, 48, 230, DateTimeKind.Utc).AddTicks(2030));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 4, 9, 48, 230, DateTimeKind.Utc).AddTicks(2110));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 4, 9, 48, 230, DateTimeKind.Utc).AddTicks(2113));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 4, 9, 48, 230, DateTimeKind.Utc).AddTicks(2116));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 7, 4, 9, 48, 339, DateTimeKind.Utc).AddTicks(2121), "$2a$11$moq6PS6oJNW7s8iXDhzhlucBCKTQdsAtO5m26F/vr6Ra9LjBS/LDO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 7, 4, 9, 48, 448, DateTimeKind.Utc).AddTicks(8224), "$2a$11$G8HQSz5AKEM1PDsh0sP0UOrdTU3AYPUhjwVGHQyq4.EoeOccwgcCG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 7, 4, 9, 48, 558, DateTimeKind.Utc).AddTicks(3094), "$2a$11$M28zCvl1ZccdI91GqjlFUueQqpZZKt98neCMgNBtU9bIGgD5.48dC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 7, 4, 9, 48, 667, DateTimeKind.Utc).AddTicks(4001), "$2a$11$4z65Jg/I/i3VXBBWnILykukdhtocEX6hEXH.TeL/7yyavuksFk1IC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 7, 4, 9, 48, 777, DateTimeKind.Utc).AddTicks(1190), "$2a$11$AvbyRJADnDXsYS.Dlz1RAec8Z7gk9W0tZ6rG1VJva0cfbj2wt17tC" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 4, 2, 12, 345, DateTimeKind.Utc).AddTicks(4567));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 4, 2, 12, 345, DateTimeKind.Utc).AddTicks(4572));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 4, 2, 12, 345, DateTimeKind.Utc).AddTicks(4575));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 4, 2, 12, 345, DateTimeKind.Utc).AddTicks(4612));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 4, 2, 12, 345, DateTimeKind.Utc).AddTicks(4615));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 4, 2, 12, 345, DateTimeKind.Utc).AddTicks(4618));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 7, 4, 2, 12, 456, DateTimeKind.Utc).AddTicks(4856), "$2a$11$s8rGT9cRTyzQUeZNPlPXxOFP.4d8WOT7CcI2yNSsLmZ9z.Z4x5wkG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 7, 4, 2, 12, 570, DateTimeKind.Utc).AddTicks(9771), "$2a$11$69yJ.Fc5N/ThsCM79Trd3eyfpE81sUrfrb6/nzrq4TmqK9tTfCXQ6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 7, 4, 2, 12, 685, DateTimeKind.Utc).AddTicks(196), "$2a$11$6j1RZuPznkGc7YMuSQwh1OdhJ6Hi7HnrJhRCLx1R4uaXIbiQd39zG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 7, 4, 2, 12, 799, DateTimeKind.Utc).AddTicks(1714), "$2a$11$p8rBk6/6U8lF5FkV15CiKetAsM1RsGv0arPiYKtQOV0ofq/rs1d7m" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 7, 4, 2, 12, 912, DateTimeKind.Utc).AddTicks(5257), "$2a$11$xMWkwsE71QvC3uSVg09i2.g1b76fCs8xznGEJMQLabsSuYxCc4bRS" });
        }
    }
}
