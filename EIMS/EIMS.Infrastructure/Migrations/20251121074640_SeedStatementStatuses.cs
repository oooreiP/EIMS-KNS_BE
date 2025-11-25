using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedStatementStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 21, 7, 46, 35, 174, DateTimeKind.Utc).AddTicks(3850));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 21, 7, 46, 35, 174, DateTimeKind.Utc).AddTicks(3865));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 21, 7, 46, 35, 174, DateTimeKind.Utc).AddTicks(3868));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 21, 7, 46, 35, 174, DateTimeKind.Utc).AddTicks(3940));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 21, 7, 46, 35, 174, DateTimeKind.Utc).AddTicks(3944));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 21, 7, 46, 35, 174, DateTimeKind.Utc).AddTicks(3948));

            migrationBuilder.InsertData(
                table: "StatementStatuses",
                columns: new[] { "StatusID", "StatusName" },
                values: new object[,]
                {
                    { 1, "Draft" },
                    { 2, "Sent" },
                    { 3, "Paid" },
                    { 4, "Overdue" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 21, 7, 46, 35, 378, DateTimeKind.Utc).AddTicks(5341), "$2a$11$VktNYVu0jZ9Rsjb2UdWGmOHviVmor5AEWRE/NI1Ok91OsF1NX9Yfq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 21, 7, 46, 35, 576, DateTimeKind.Utc).AddTicks(8776), "$2a$11$uvY.Wd.1n.H9AVDJBRanOu29qX.ztv6NRI/ho1sM3aCpMiEiXwQqS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 21, 7, 46, 35, 774, DateTimeKind.Utc).AddTicks(8554), "$2a$11$k24TrQGJX/ZGirB9OZ2QPu/OgskmtlFzfqWq6akNLptr9mIknuvU." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 21, 7, 46, 35, 968, DateTimeKind.Utc).AddTicks(7524), "$2a$11$rAczaG8d08kmivmIVM/b8uz1vo5EjrjrQMGcRmeIwfRSqbgjgQCjS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 21, 7, 46, 36, 193, DateTimeKind.Utc).AddTicks(4706), "$2a$11$93L1mhD6fChPdWJWMQmIougxd53dKWAlOUcVGTaZRudwzfz0QWjfa" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StatementStatuses",
                keyColumn: "StatusID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StatementStatuses",
                keyColumn: "StatusID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StatementStatuses",
                keyColumn: "StatusID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "StatementStatuses",
                keyColumn: "StatusID",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 20, 4, 24, 39, 216, DateTimeKind.Utc).AddTicks(5356));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 20, 4, 24, 39, 216, DateTimeKind.Utc).AddTicks(5366));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 20, 4, 24, 39, 216, DateTimeKind.Utc).AddTicks(5370));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 20, 4, 24, 39, 216, DateTimeKind.Utc).AddTicks(5452));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 20, 4, 24, 39, 216, DateTimeKind.Utc).AddTicks(5457));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 20, 4, 24, 39, 216, DateTimeKind.Utc).AddTicks(5461));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 20, 4, 24, 39, 440, DateTimeKind.Utc).AddTicks(1089), "$2a$11$y29PtaszoeesOT.kKjqTXuPO2Gb0q3LtlWD399aIlon8EmEsyCEyS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 20, 4, 24, 39, 634, DateTimeKind.Utc).AddTicks(959), "$2a$11$vBtd5t.FCr9nV8/TIZg0PeFKMWWnS/1OwoseU5mUOAO1e/qoVJnT2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 20, 4, 24, 39, 827, DateTimeKind.Utc).AddTicks(9623), "$2a$11$wLbvDIyMbQXYnAg4IJIBbOFopmdx2Wn7W5wgrfEEk0bH78fGAVrQO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 20, 4, 24, 40, 22, DateTimeKind.Utc).AddTicks(8274), "$2a$11$tQ9QQ0dElvMz9PJjVQtol.ToT1DSy6Zk0T6t11gduBktIcJidFjuW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 20, 4, 24, 40, 217, DateTimeKind.Utc).AddTicks(9690), "$2a$11$.rC/BlHQUeHmBWoIx/V2SuQwleKb161rUUorVw9hD2oQf.3OdT3bq" });
        }
    }
}
