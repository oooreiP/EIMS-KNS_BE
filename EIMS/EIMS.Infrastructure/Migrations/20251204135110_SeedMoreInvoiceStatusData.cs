using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoreInvoiceStatusData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "InvoiceStatuses",
                columns: new[] { "InvoiceStatusID", "StatusName" },
                values: new object[,]
                {
                    { 6, "Pending Approval" },
                    { 7, "Pending Sign" }
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InvoiceStatuses",
                keyColumn: "InvoiceStatusID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "InvoiceStatuses",
                keyColumn: "InvoiceStatusID",
                keyValue: 7);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 4, 2, 14, 1, 437, DateTimeKind.Utc).AddTicks(7615));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 4, 2, 14, 1, 437, DateTimeKind.Utc).AddTicks(7628));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 4, 2, 14, 1, 437, DateTimeKind.Utc).AddTicks(7632));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 4, 2, 14, 1, 437, DateTimeKind.Utc).AddTicks(7687));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 4, 2, 14, 1, 437, DateTimeKind.Utc).AddTicks(7692));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 4, 2, 14, 1, 437, DateTimeKind.Utc).AddTicks(7697));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 4, 2, 14, 1, 640, DateTimeKind.Utc).AddTicks(3859), "$2a$11$pkZzFM7c7Sb/2vRSddZ1Pe3BPhGycEb/ptdAIwrnQdKz3IhEoLn5S" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 4, 2, 14, 1, 828, DateTimeKind.Utc).AddTicks(6330), "$2a$11$JvixKF.i9W55IdZ/2obiBeX2deVECiMEsutebm/xYrh7ifRxAO1CC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 4, 2, 14, 2, 16, DateTimeKind.Utc).AddTicks(6528), "$2a$11$8mGoi4pUOawEtCBATREdxunJwn9/iQOLzSaNrhNXG2E7OtGVlvBye" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 4, 2, 14, 2, 205, DateTimeKind.Utc).AddTicks(3295), "$2a$11$DNNd.Rfi4y4lGHALUX.hwOhqHuuRBMENmUZ7djrkv2av81luQ0rQW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 4, 2, 14, 2, 393, DateTimeKind.Utc).AddTicks(6436), "$2a$11$pYqYlINMcP7GK5JH6BNYyes3kEU7OQRuugzwsPAYZg6ut9XBImaJy" });
        }
    }
}
