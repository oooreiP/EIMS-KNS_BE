using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AllowNullCompanyInInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 21, 14, 45, 211, DateTimeKind.Utc).AddTicks(8976));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 21, 14, 45, 211, DateTimeKind.Utc).AddTicks(8981));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 21, 14, 45, 211, DateTimeKind.Utc).AddTicks(8984));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 21, 14, 45, 211, DateTimeKind.Utc).AddTicks(9035));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 21, 14, 45, 211, DateTimeKind.Utc).AddTicks(9038));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 21, 14, 45, 211, DateTimeKind.Utc).AddTicks(9040));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 21, 14, 45, 323, DateTimeKind.Utc).AddTicks(6028), "$2a$11$8OnAd5fTKq5K8FUoYdEXS.SKP.7cFSFA.k9mNjmxO8I5A7H14VSN2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 21, 14, 45, 436, DateTimeKind.Utc).AddTicks(6497), "$2a$11$0VeNKilzvqyUHn51meEZren/oNTOo8KiiASbc9VLXatzz4mxBAhEa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 21, 14, 45, 546, DateTimeKind.Utc).AddTicks(6976), "$2a$11$ax5kXFGJrm1wMeNy582p7.P4YHlMEq4HGlt7r2TebP9zACF2m2FkW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 21, 14, 45, 657, DateTimeKind.Utc).AddTicks(6569), "$2a$11$s1S1tT4SiQi61n..sitNcO/g.qmvOCWEWU2pauINLZvC2bj1vAvA." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 21, 14, 45, 770, DateTimeKind.Utc).AddTicks(5167), "$2a$11$t6Aa.Uf5l6o2JBt5yllg4.U6lMm7SfDA7mqOodS7WS0WMm9uF6Bsu" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 20, 27, 26, 551, DateTimeKind.Utc).AddTicks(9067));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 20, 27, 26, 551, DateTimeKind.Utc).AddTicks(9072));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 20, 27, 26, 551, DateTimeKind.Utc).AddTicks(9075));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 20, 27, 26, 551, DateTimeKind.Utc).AddTicks(9125));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 20, 27, 26, 551, DateTimeKind.Utc).AddTicks(9128));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 5, 20, 27, 26, 551, DateTimeKind.Utc).AddTicks(9131));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 20, 27, 26, 667, DateTimeKind.Utc).AddTicks(766), "$2a$11$VoYG6VEbaJlWBug9oPoz3utuak3QUFuVzxcUBQqO4OKhoHajzUwIC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 20, 27, 26, 783, DateTimeKind.Utc).AddTicks(2989), "$2a$11$6sy0d7x.V4mgV.eztfK.1.St9LAPoe4IOB5SFxTo1eXkpfavdALPa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 20, 27, 26, 893, DateTimeKind.Utc).AddTicks(9218), "$2a$11$l4NQY//c8ClpJtrsl2nq8uEEwQ/7XQE5kuH3dZA5EVCwE/lFNahCK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 20, 27, 27, 4, DateTimeKind.Utc).AddTicks(5107), "$2a$11$uhEk8PSqqM3JnWPlWWWkG.HzfwXljRehFdjBfnnoxv5njPL.V4f6W" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 5, 20, 27, 27, 117, DateTimeKind.Utc).AddTicks(9357), "$2a$11$p9ZpnIjHj1pVyPcAHyodeuFxX0ee7PRl7hal9XlJcvlLriGG9w/de" });
        }
    }
}
