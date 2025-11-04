using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSymbolToSerialStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Symbol",
                table: "SerialStatuses",
                type: "character varying(1)",
                maxLength: 1,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 8, 13, 8, 268, DateTimeKind.Utc).AddTicks(5052));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 8, 13, 8, 268, DateTimeKind.Utc).AddTicks(5063));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 8, 13, 8, 268, DateTimeKind.Utc).AddTicks(5068));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 8, 13, 8, 268, DateTimeKind.Utc).AddTicks(5147));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 8, 13, 8, 268, DateTimeKind.Utc).AddTicks(5152));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 8, 13, 8, 268, DateTimeKind.Utc).AddTicks(5156));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 8, 13, 8, 467, DateTimeKind.Utc).AddTicks(1480), "$2a$11$gJ3UTJszrr4kpQGrUAw9auovtz2lPbdk5qdwEgpx6LgTY0R9TlQ9." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 8, 13, 8, 624, DateTimeKind.Utc).AddTicks(2865), "$2a$11$LgLCkTLBcNjJhPGvzncPtu3krOTqrO28wXelp5emJ5/gk/Ip3LkSm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 8, 13, 8, 810, DateTimeKind.Utc).AddTicks(6476), "$2a$11$qm0Sy66sXt26dA2or2hkJuftwBbPQrj8qyUHbp8yvWJIRvIj3s.Hu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 8, 13, 8, 973, DateTimeKind.Utc).AddTicks(4821), "$2a$11$616vfFHiQQsVISer44Jh7OMuQ5vZ2FLLWpO/cbDlFp1nk1qS7pyB." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 8, 13, 9, 149, DateTimeKind.Utc).AddTicks(6220), "$2a$11$gBw9FkyRNuNda6961tW3PuBnmdGmHM5nHHuRYOeSFLfVVtGklFfUe" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Symbol",
                table: "SerialStatuses");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 7, 21, 25, 962, DateTimeKind.Utc).AddTicks(6364));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 7, 21, 25, 962, DateTimeKind.Utc).AddTicks(6377));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 7, 21, 25, 962, DateTimeKind.Utc).AddTicks(6381));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 7, 21, 25, 962, DateTimeKind.Utc).AddTicks(6433));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 7, 21, 25, 962, DateTimeKind.Utc).AddTicks(6437));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 7, 21, 25, 962, DateTimeKind.Utc).AddTicks(6440));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 7, 21, 26, 133, DateTimeKind.Utc).AddTicks(5724), "$2a$11$qsDcOYhIARS2U97XnZbL2eSlnyYXWpor79g8JdguTafPM5r3GCT2m" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 7, 21, 26, 298, DateTimeKind.Utc).AddTicks(9681), "$2a$11$oYh6/Q5bawIPj4VFGm4DWeYPfjKb5gSMST5wyba9sYHkHXkDnqsKG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 7, 21, 26, 465, DateTimeKind.Utc).AddTicks(8303), "$2a$11$zWItrNzWduZUoi.CNmw42..wSWsx3wEkzqYzdItHdf5VKiS6/rloa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 7, 21, 26, 623, DateTimeKind.Utc).AddTicks(5495), "$2a$11$jN1odsuHKie0TVd5wLWmg.nAM8OjgoPZ04Fkf5v4UhSrDKIw1Bhhi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 7, 21, 26, 787, DateTimeKind.Utc).AddTicks(6074), "$2a$11$0aQYC.snXc6H.EQxTSvQieaIXt2I8gR1iXPM724FtgjM8Gt.mZ1Fq" });
        }
    }
}
