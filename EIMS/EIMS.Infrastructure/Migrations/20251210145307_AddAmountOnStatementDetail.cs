using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAmountOnStatementDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "OutstandingAmount",
                table: "InvoiceStatementDetails",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 10, 14, 53, 3, 281, DateTimeKind.Utc).AddTicks(5572));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 10, 14, 53, 3, 281, DateTimeKind.Utc).AddTicks(5579));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 10, 14, 53, 3, 281, DateTimeKind.Utc).AddTicks(5581));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 10, 14, 53, 3, 281, DateTimeKind.Utc).AddTicks(5796));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 10, 14, 53, 3, 281, DateTimeKind.Utc).AddTicks(5799));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 10, 14, 53, 3, 281, DateTimeKind.Utc).AddTicks(5803));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 53, 3, 513, DateTimeKind.Utc).AddTicks(1424), "$2a$11$gnlLYqwf4RDOoaFxrpP20uaRQ5FVlphReIOv2Iwo7a3A7nAK2yeKy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 53, 3, 744, DateTimeKind.Utc).AddTicks(2539), "$2a$11$xKQYCOFsV7WhvdmBhXPEoOKYy51XdAZMPsGnMCm62dB0Pg4RzUfoC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 53, 3, 950, DateTimeKind.Utc).AddTicks(8564), "$2a$11$u/Jlwf86qcU9ZmiYjd2JbeorTIcemrq5fSGJldzMHVnIiz0r59Kny" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 53, 4, 196, DateTimeKind.Utc).AddTicks(2183), "$2a$11$7W8n3W2OCy/wz10jr/GutO3476vH3jm8HwdL6bJA3HSGPO96aBcTK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 10, 14, 53, 4, 403, DateTimeKind.Utc).AddTicks(8000), "$2a$11$Totul.QxNhvOP7tGZeJ3t.EVzpyfmqL6Y0XTlO/L8SO6eR07SdqKy" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OutstandingAmount",
                table: "InvoiceStatementDetails");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 11, 58, 14, 119, DateTimeKind.Utc).AddTicks(3312));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 11, 58, 14, 119, DateTimeKind.Utc).AddTicks(3321));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 11, 58, 14, 119, DateTimeKind.Utc).AddTicks(3324));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 11, 58, 14, 119, DateTimeKind.Utc).AddTicks(3351));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 11, 58, 14, 119, DateTimeKind.Utc).AddTicks(3354));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 11, 58, 14, 119, DateTimeKind.Utc).AddTicks(3356));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 8, 11, 58, 14, 235, DateTimeKind.Utc).AddTicks(5578), "$2a$11$MSE0fDYBkvoSNwoVIe6dvufuG5zNfZ1arVYb69TOKrorM4/7hDBPi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 8, 11, 58, 14, 351, DateTimeKind.Utc).AddTicks(4669), "$2a$11$locYOzUXcpdcg4mjjGh5ROC4Qda7MhdtcdYMPvMf8juKk3RS5tS.W" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 8, 11, 58, 14, 465, DateTimeKind.Utc).AddTicks(2398), "$2a$11$HYRfT8j9Nz.l2bOCaZJdyeKQ7pt07rD5UO42hB2nDgh9osvbSFzeG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 8, 11, 58, 14, 577, DateTimeKind.Utc).AddTicks(2735), "$2a$11$wMuLIj9alYdjzrARjkloounEEPH.PVTbUJiWSIIy0mFUl0TdWTtkG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 8, 11, 58, 14, 686, DateTimeKind.Utc).AddTicks(7945), "$2a$11$ax80jg80k9IXRV.Oaz/Md.iNWuT3OvGfAnflsM2EdKjbsMLN4H8BS" });
        }
    }
}
