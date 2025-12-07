using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "InvoiceStatuses",
                columns: new[] { "InvoiceStatusID", "StatusName" },
                values: new object[,]
                {
                    { 8, "Signed" },
                    { 9, "Sent" }
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InvoiceStatuses",
                keyColumn: "InvoiceStatusID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "InvoiceStatuses",
                keyColumn: "InvoiceStatusID",
                keyValue: 9);

            migrationBuilder.AddColumn<int>(
                name: "SalesID",
                table: "Invoices",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 4, 13, 51, 7, 130, DateTimeKind.Utc).AddTicks(3445));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 4, 13, 51, 7, 130, DateTimeKind.Utc).AddTicks(3453));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 4, 13, 51, 7, 130, DateTimeKind.Utc).AddTicks(3455));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 4, 13, 51, 7, 130, DateTimeKind.Utc).AddTicks(3488));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 4, 13, 51, 7, 130, DateTimeKind.Utc).AddTicks(3492));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 4, 13, 51, 7, 130, DateTimeKind.Utc).AddTicks(3495));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 4, 13, 51, 7, 247, DateTimeKind.Utc).AddTicks(2738), "$2a$11$B4tLAXkgPuazc.uzwnADLOhpPB/BepnWK9zby4ANjJ/C63ZUKIx0G" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 4, 13, 51, 7, 365, DateTimeKind.Utc).AddTicks(1922), "$2a$11$/8vsaKdaHcKlKTBkts2.T.RIk4YbS37pfiaOTqaNVLJ0UTkbUQvGu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 4, 13, 51, 7, 481, DateTimeKind.Utc).AddTicks(8770), "$2a$11$dcB6a/dP/C3jsBTWPcTTXOG/m930HbeY7hoD.lm1j7VpyV6B82/RG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 4, 13, 51, 7, 598, DateTimeKind.Utc).AddTicks(4786), "$2a$11$7hCAyD.20em6hZHGXBlqtudRZlR20YWRH9F57NX86xwNTV0fqp9ny" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 4, 13, 51, 7, 715, DateTimeKind.Utc).AddTicks(3331), "$2a$11$L5toq4ujzrMoAmUuN0cdbOTgsJbFtuTwuHQbwFsNkCdqqapIScfDe" });

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Users_SalesID",
                table: "Invoices",
                column: "SalesID",
                principalTable: "Users",
                principalColumn: "UserID");
        }
    }
}
