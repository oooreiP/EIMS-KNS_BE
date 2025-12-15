using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixStatementFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 7, 42, 20, 359, DateTimeKind.Utc).AddTicks(3948));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 7, 42, 20, 359, DateTimeKind.Utc).AddTicks(3958));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 7, 42, 20, 359, DateTimeKind.Utc).AddTicks(3961));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 7, 42, 20, 359, DateTimeKind.Utc).AddTicks(4211));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 7, 42, 20, 359, DateTimeKind.Utc).AddTicks(4215));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 7, 42, 20, 359, DateTimeKind.Utc).AddTicks(4219));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 7, 42, 20, 558, DateTimeKind.Utc).AddTicks(3311), "$2a$11$3eoJgjdMeKEzltwOhIE6J.9jK0qiCEqOY71oLGEaZ2DlTvO3DZYDm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 7, 42, 20, 759, DateTimeKind.Utc).AddTicks(9171), "$2a$11$tkGBAOAZ/r39gTEtykU4fuUk/nv9cAHqRFGV3mwzyrB/lZAzm8Wtu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 7, 42, 20, 964, DateTimeKind.Utc).AddTicks(8345), "$2a$11$0f1FTWkTTwXCXJo8gcdcF.WFnOcreLmmGwBJc9G5R0FYsm.awi2Te" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 7, 42, 21, 148, DateTimeKind.Utc).AddTicks(9227), "$2a$11$P3e.Udhyto3x8nvojwOni.d7nl4sC1QuZlvXvwWnq0THl8d7YxW9K" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 7, 42, 21, 341, DateTimeKind.Utc).AddTicks(1515), "$2a$11$ImfUvwGqdJ741xnYHOUlk.83k7F8DgdA.U8m1X3WO4Lcq4vfgCiva" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 5, 47, 56, 696, DateTimeKind.Utc).AddTicks(6105));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 5, 47, 56, 696, DateTimeKind.Utc).AddTicks(6112));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 5, 47, 56, 696, DateTimeKind.Utc).AddTicks(6114));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 5, 47, 56, 696, DateTimeKind.Utc).AddTicks(6168));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 5, 47, 56, 696, DateTimeKind.Utc).AddTicks(6171));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 5, 47, 56, 696, DateTimeKind.Utc).AddTicks(6174));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 5, 47, 56, 813, DateTimeKind.Utc).AddTicks(3992), "$2a$11$oxFrnJF8huMQk3cAMYoM0OwvpqE7Szzc07qRPRUGHjgZSVCUmd11a" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 5, 47, 56, 930, DateTimeKind.Utc).AddTicks(658), "$2a$11$BasiRD0/eGBTDl3mgBmlH.od7IKrTRFxFccvydwKQjsuSJJ.QuJPy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 5, 47, 57, 48, DateTimeKind.Utc).AddTicks(1484), "$2a$11$G6S87Sr/C1LjSmszBOktOeUWbLINwEqcRw1rY5lyct6OpN22hLd5u" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 5, 47, 57, 161, DateTimeKind.Utc).AddTicks(2911), "$2a$11$6vvnybE8bCtKvi5F3ZcDTexbkDvIB0IPdSqX2saVdjL2nfIxnEnGC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 5, 47, 57, 275, DateTimeKind.Utc).AddTicks(4537), "$2a$11$E62QsZqZTqW4pAHGSjgvnurLK.i293ZkMXffU2itYTdR1tYklpUNK" });
        }
    }
}
