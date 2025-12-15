using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixStatementFK1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceStatements_StatementStatuses_StatementStatusStatusID",
                table: "InvoiceStatements");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceStatements_StatementStatusStatusID",
                table: "InvoiceStatements");

            migrationBuilder.DropColumn(
                name: "StatementStatusStatusID",
                table: "InvoiceStatements");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 7, 56, 51, 893, DateTimeKind.Utc).AddTicks(7849));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 7, 56, 51, 893, DateTimeKind.Utc).AddTicks(7855));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 7, 56, 51, 893, DateTimeKind.Utc).AddTicks(7858));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 7, 56, 51, 893, DateTimeKind.Utc).AddTicks(7965));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 7, 56, 51, 893, DateTimeKind.Utc).AddTicks(7968));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 13, 7, 56, 51, 893, DateTimeKind.Utc).AddTicks(7971));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 7, 56, 52, 9, DateTimeKind.Utc).AddTicks(1075), "$2a$11$nvsEd5oXXm6Sa02SKL1zr.M7KFipfAkXwBTNSAN7bYYeqQ3FggIhu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 7, 56, 52, 126, DateTimeKind.Utc).AddTicks(8947), "$2a$11$iQUi3JlHBGnMgBvN/cSfuOTuxmRpjsubxIcPsvkZBvFlJf2on7zqS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 7, 56, 52, 244, DateTimeKind.Utc).AddTicks(2427), "$2a$11$RTJZvZBEk8g9stp6ZnDsBOCl2LqjiEwjgG21eVYimfKc6Ky8AybQG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 7, 56, 52, 364, DateTimeKind.Utc).AddTicks(1868), "$2a$11$.H9Q7kQSLJrA9LX1DLp6beTbao0CtWEWjiYtQIV/yq8cW56P.Ts.O" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 13, 7, 56, 52, 481, DateTimeKind.Utc).AddTicks(1960), "$2a$11$A3LR743mnhxl2ATKu8KNzuCnn4oMA3hRDuCVOLAXBAwsEJaE4ISMO" });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceStatements_StatusID",
                table: "InvoiceStatements",
                column: "StatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceStatements_StatementStatuses_StatusID",
                table: "InvoiceStatements",
                column: "StatusID",
                principalTable: "StatementStatuses",
                principalColumn: "StatusID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceStatements_StatementStatuses_StatusID",
                table: "InvoiceStatements");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceStatements_StatusID",
                table: "InvoiceStatements");

            migrationBuilder.AddColumn<int>(
                name: "StatementStatusStatusID",
                table: "InvoiceStatements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceStatements_StatementStatusStatusID",
                table: "InvoiceStatements",
                column: "StatementStatusStatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceStatements_StatementStatuses_StatementStatusStatusID",
                table: "InvoiceStatements",
                column: "StatementStatusStatusID",
                principalTable: "StatementStatuses",
                principalColumn: "StatusID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
