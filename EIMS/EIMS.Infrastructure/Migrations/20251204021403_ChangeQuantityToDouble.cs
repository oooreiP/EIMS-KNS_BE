using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeQuantityToDouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 3, 18, 5, 21, 680, DateTimeKind.Utc).AddTicks(1762));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 3, 18, 5, 21, 680, DateTimeKind.Utc).AddTicks(1770));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 3, 18, 5, 21, 680, DateTimeKind.Utc).AddTicks(1774));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 3, 18, 5, 21, 680, DateTimeKind.Utc).AddTicks(1832));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 3, 18, 5, 21, 680, DateTimeKind.Utc).AddTicks(1837));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 3, 18, 5, 21, 680, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 3, 18, 5, 21, 835, DateTimeKind.Utc).AddTicks(6483), "$2a$11$WI7cLDSpLfXClaxjaigiH.4sV5Fsjhf310m6.CnmUHcYb1Yn72tZC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 3, 18, 5, 21, 989, DateTimeKind.Utc).AddTicks(8040), "$2a$11$7vppO1ulOcAXfytXsQEczu7dprRXwJ0oUh8YW7T6GKFkCItDDEp.C" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 3, 18, 5, 22, 146, DateTimeKind.Utc).AddTicks(7189), "$2a$11$1xHsMT3yP8tPEInHw4ssw.2oRl0UX7V8kHdrCV0UAz4weAKsWmbOe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 3, 18, 5, 22, 304, DateTimeKind.Utc).AddTicks(9525), "$2a$11$VCLxPG6DX76Z0EQD2SsgruvI5rY7iHYP79gApa4CqWhIlvVKgwkeW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 3, 18, 5, 22, 459, DateTimeKind.Utc).AddTicks(4985), "$2a$11$HflCSavsxtB6Wx/5GK0Y1uzk4hOo88bD5xZNNeHoPYHHnbeP25u2S" });
        }
    }
}
