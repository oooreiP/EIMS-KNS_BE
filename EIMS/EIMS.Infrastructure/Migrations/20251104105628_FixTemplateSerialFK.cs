using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixTemplateSerialFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceTemplates_Serials_SerialStatusID",
                table: "InvoiceTemplates");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceTemplates_SerialStatusID",
                table: "InvoiceTemplates");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 10, 56, 27, 246, DateTimeKind.Utc).AddTicks(3643));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 10, 56, 27, 246, DateTimeKind.Utc).AddTicks(3652));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 10, 56, 27, 246, DateTimeKind.Utc).AddTicks(3655));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 10, 56, 27, 246, DateTimeKind.Utc).AddTicks(3706));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 10, 56, 27, 246, DateTimeKind.Utc).AddTicks(3710));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 10, 56, 27, 246, DateTimeKind.Utc).AddTicks(3713));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 10, 56, 27, 402, DateTimeKind.Utc).AddTicks(8950), "$2a$11$IE5Q1kOFqENen56uo2XoG.IP5OuTlbCFL.nuXPGSxbGA5A03qkhSu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 10, 56, 27, 558, DateTimeKind.Utc).AddTicks(585), "$2a$11$bnbyp9yMHM0s6SVpKEm8r.EL1rTO0CaB26JfqmF0.dWuvkizo6UGm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 10, 56, 27, 713, DateTimeKind.Utc).AddTicks(4751), "$2a$11$dpK4fK/i2FY24mwvyLEdt.dDwkKUW6Isi5lDyjCxvDhOiwH1lhbvS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 10, 56, 27, 867, DateTimeKind.Utc).AddTicks(9310), "$2a$11$Nnw9F5lCMc9IrAg2b30o9eXFKSeZmnyTIbs2XyqLSAUY9aXX9OaUK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 10, 56, 28, 23, DateTimeKind.Utc).AddTicks(1802), "$2a$11$130Van7BXkWIvex2BIeHau.VBnULwR2mN4ekRPd1a2uCpvxVgVwIW" });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceTemplates_SerialID",
                table: "InvoiceTemplates",
                column: "SerialID");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceTemplates_Serials_SerialID",
                table: "InvoiceTemplates",
                column: "SerialID",
                principalTable: "Serials",
                principalColumn: "SerialID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceTemplates_Serials_SerialID",
                table: "InvoiceTemplates");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceTemplates_SerialID",
                table: "InvoiceTemplates");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 10, 33, 52, 535, DateTimeKind.Utc).AddTicks(5971));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 10, 33, 52, 535, DateTimeKind.Utc).AddTicks(5980));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 10, 33, 52, 535, DateTimeKind.Utc).AddTicks(5983));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 10, 33, 52, 535, DateTimeKind.Utc).AddTicks(6034));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 10, 33, 52, 535, DateTimeKind.Utc).AddTicks(6038));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 10, 33, 52, 535, DateTimeKind.Utc).AddTicks(6042));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 10, 33, 52, 692, DateTimeKind.Utc).AddTicks(3507), "$2a$11$Uq3.Eqf0/IY82F5SXicwsuTdTk9qAq5m0x0IGTFXNVSTeaOXmHAXu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 10, 33, 52, 853, DateTimeKind.Utc).AddTicks(2083), "$2a$11$NsKsCViWgyyMGDxkBh8c5.76DZffkxA7EtsUmP517dDWdL2JnTA5u" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 10, 33, 53, 9, DateTimeKind.Utc).AddTicks(238), "$2a$11$de53I21Px.56GsZ16EUll.DKaTbu.C2pVSEod7ADiclSGkIWLXV7G" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 10, 33, 53, 164, DateTimeKind.Utc).AddTicks(6781), "$2a$11$64VI4BtrOzGstEWuLoeynuJXhlpN8Le33VsxJrUdTWB0YCSpuZKuW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 10, 33, 53, 321, DateTimeKind.Utc).AddTicks(184), "$2a$11$eIDNj/6/IdVu70l1XU26Hub6kFRW2yIhaYo2uxvbZVwhh4d3XpKty" });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceTemplates_SerialStatusID",
                table: "InvoiceTemplates",
                column: "SerialStatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceTemplates_Serials_SerialStatusID",
                table: "InvoiceTemplates",
                column: "SerialStatusID",
                principalTable: "Serials",
                principalColumn: "SerialID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
