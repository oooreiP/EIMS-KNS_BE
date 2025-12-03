using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentMethodInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Invoices",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 3, 15, 6, 32, 461, DateTimeKind.Utc).AddTicks(8060));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 3, 15, 6, 32, 461, DateTimeKind.Utc).AddTicks(8085));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 3, 15, 6, 32, 461, DateTimeKind.Utc).AddTicks(8088));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 3, 15, 6, 32, 461, DateTimeKind.Utc).AddTicks(8227));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 3, 15, 6, 32, 461, DateTimeKind.Utc).AddTicks(8232));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 3, 15, 6, 32, 461, DateTimeKind.Utc).AddTicks(8239));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 3, 15, 6, 32, 657, DateTimeKind.Utc).AddTicks(1197), "$2a$11$tlBn9eiCpciICMDBk4P8W.EtZUJd9/I44msItWUUqsTVX05drcoU6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 3, 15, 6, 32, 819, DateTimeKind.Utc).AddTicks(412), "$2a$11$QYor6MZ59LNfFMNwrRi3yu5WbrXFuSWA8pXCDkoFaxgTWUjU2BES6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 3, 15, 6, 32, 992, DateTimeKind.Utc).AddTicks(2431), "$2a$11$Epvt67oisQtjrXHbSiUD7O9Br16A7sm32MJelfwc3DzzImR3f4U0e" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 3, 15, 6, 33, 164, DateTimeKind.Utc).AddTicks(2201), "$2a$11$GldopbS4h8cxB8MrcyxWquW1z9LaLAesmxO.xiy7VsuOeaXBDx7Hq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 3, 15, 6, 33, 368, DateTimeKind.Utc).AddTicks(1586), "$2a$11$GC8ik5vPEFYBIYEy3XRd.uHHGYlEm3g1BEkNFXWdFZ/PyBhgCDZsG" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Invoices");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 2, 13, 24, 54, 518, DateTimeKind.Utc).AddTicks(4358));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 2, 13, 24, 54, 518, DateTimeKind.Utc).AddTicks(4369));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 2, 13, 24, 54, 518, DateTimeKind.Utc).AddTicks(4373));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 2, 13, 24, 54, 518, DateTimeKind.Utc).AddTicks(4454));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 2, 13, 24, 54, 518, DateTimeKind.Utc).AddTicks(4459));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 2, 13, 24, 54, 518, DateTimeKind.Utc).AddTicks(4463));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 2, 13, 24, 54, 761, DateTimeKind.Utc).AddTicks(8127), "$2a$11$LOIzkj/XXmVxICbaovtzPOFozprVUFsqaqonWPCWdy13KcxGT8tkK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 2, 13, 24, 54, 960, DateTimeKind.Utc).AddTicks(4819), "$2a$11$YO.25PrZU39s6L7D3olOrOlEJDGrskzssMELmKj7DhFDfTYfW1H1S" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 2, 13, 24, 55, 168, DateTimeKind.Utc).AddTicks(178), "$2a$11$B.l4D3pOxvrHSi6oN2jNAOjSgfGDw4LNGuV8Qy3tESO.jOwxxmx9G" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 2, 13, 24, 55, 369, DateTimeKind.Utc).AddTicks(7133), "$2a$11$m.8at/CcPShRoYNiJYiB8OkTNRvwqiwUsBcxWXOiOUUCMYs06BNzC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 2, 13, 24, 55, 569, DateTimeKind.Utc).AddTicks(2988), "$2a$11$A0.35mah848HfEAEp7wmYOYFU7BrtnlsrxnFVD1YKoioAJz8WCJHa" });
        }
    }
}
