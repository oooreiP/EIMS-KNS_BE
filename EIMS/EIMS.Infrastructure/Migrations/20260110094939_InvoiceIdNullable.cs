using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "InvoiceID",
                table: "TaxApiLogs",
                type: "integer",
                nullable: true,          
                oldClrType: typeof(int),
                oldType: "integer");
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 9, 49, 32, 390, DateTimeKind.Utc).AddTicks(5929));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 9, 49, 32, 390, DateTimeKind.Utc).AddTicks(5939));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 9, 49, 32, 390, DateTimeKind.Utc).AddTicks(5993));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 10, 9, 49, 32, 987, DateTimeKind.Utc).AddTicks(5734));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 10, 9, 49, 32, 987, DateTimeKind.Utc).AddTicks(5737));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 10, 9, 49, 32, 987, DateTimeKind.Utc).AddTicks(5739));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 9, 49, 32, 390, DateTimeKind.Utc).AddTicks(6029));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 9, 49, 32, 390, DateTimeKind.Utc).AddTicks(6032));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 9, 49, 32, 390, DateTimeKind.Utc).AddTicks(6034));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 9, 49, 32, 508, DateTimeKind.Utc).AddTicks(3939), "$2a$11$vT9hLjUTY6KFe2mcFxoxp.SBARcgzN0bsWE9Xf/K5EpgdyJ/ILEbq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 9, 49, 32, 625, DateTimeKind.Utc).AddTicks(4996), "$2a$11$tkfF46mPZv218A2knvsZU.S5qYkKVvLlBYYHHv7VlWkxruIGbvfvS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 9, 49, 32, 743, DateTimeKind.Utc).AddTicks(2784), "$2a$11$irGtCypCdIPeDCJ7BTUUSuF3IVwsZFOvpM5ckmzYR/kwqRYIzsKWa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 9, 49, 32, 863, DateTimeKind.Utc).AddTicks(8847), "$2a$11$dAneCBVuK4.FWtcZtextxev9Jp2BnXP7NtnXycXpadAgszhi0rMHy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 9, 49, 32, 984, DateTimeKind.Utc).AddTicks(8268), "$2a$11$llzIZciK8MzR.rHbXrqP0uum4W0/FFER0AZ0Jlfe3BzfuYJ3kWByC" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "InvoiceID",
                table: "TaxApiLogs",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 9, 37, 51, 171, DateTimeKind.Utc).AddTicks(2784));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 9, 37, 51, 171, DateTimeKind.Utc).AddTicks(2794));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 9, 37, 51, 171, DateTimeKind.Utc).AddTicks(2796));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 10, 9, 37, 51, 761, DateTimeKind.Utc).AddTicks(2723));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 10, 9, 37, 51, 761, DateTimeKind.Utc).AddTicks(2729));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 10, 9, 37, 51, 761, DateTimeKind.Utc).AddTicks(2731));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 9, 37, 51, 171, DateTimeKind.Utc).AddTicks(2839));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 9, 37, 51, 171, DateTimeKind.Utc).AddTicks(2842));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 9, 37, 51, 171, DateTimeKind.Utc).AddTicks(2845));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 9, 37, 51, 283, DateTimeKind.Utc).AddTicks(8617), "$2a$11$1yXq4QkAQ.t4XnatZ7Z9n.C8vncYJXBa89fXue8c../TKnQj2dmDa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 9, 37, 51, 407, DateTimeKind.Utc).AddTicks(4891), "$2a$11$6fCdfpro0zU1XHtl.wKeZeFtVnLZ9QS5ULsmvI6odTkk9CWDLV6Wu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 9, 37, 51, 525, DateTimeKind.Utc).AddTicks(1928), "$2a$11$LLYBIWhvraxT50Dsmb1VAO1DvyFb/i.12VI2PMXXImJ8Cqc07JhfK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 9, 37, 51, 643, DateTimeKind.Utc).AddTicks(7818), "$2a$11$IIVmDyRDGU4woC9ttaezbOshF3pcZGuLn4tybvhAuzX.2SpP1Mn/u" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 9, 37, 51, 759, DateTimeKind.Utc).AddTicks(931), "$2a$11$aH6G4AQRB4kiZV0x9RjeG.fCUS3Kxlp3Xsfn6REE.kolKcOXfZy7y" });
        }
    }
}
