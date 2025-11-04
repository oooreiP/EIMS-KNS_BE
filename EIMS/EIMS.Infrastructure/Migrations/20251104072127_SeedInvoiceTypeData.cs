using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedInvoiceTypeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "InvoiceTypes",
                columns: new[] { "InvoiceTypeID", "Symbol", "TypeName" },
                values: new object[,]
                {
                    { 1, "T", "Hóa đơn Doanh nghiệp, tổ chức, hộ, cá nhân kinh doanh đăng ký sử dụng" },
                    { 2, "D", "Hóa đơn tài sản công và hóa đơn bán hàng dự trữ quốc gia hoặc hóa đơn điện tử đặc thù không nhất thiết phải có một số tiêu thức do các doanh nghiệp, tổ chức đăng ký sử dụng" },
                    { 3, "L", "Hóa đơn Cơ quan thuế cấp theo từng lần phát sinh" },
                    { 4, "M", "Hóa đơn khởi tạo từ máy tính tiền" },
                    { 5, "N", "Phiếu xuất kho kiêm vận chuyển nội bộ" },
                    { 6, "B", "Phiếu xuất kho gửi bán đại lý điện" },
                    { 7, "G", "Tem, vé, thẻ điện tử là hóa đơn GTGT" },
                    { 8, "H", "Tem, vé, thẻ điện tử là hóa đơn bán hàng" },
                    { 9, "X", "Hóa đơn thương mại điện tử" }
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InvoiceTypes",
                keyColumn: "InvoiceTypeID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "InvoiceTypes",
                keyColumn: "InvoiceTypeID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "InvoiceTypes",
                keyColumn: "InvoiceTypeID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "InvoiceTypes",
                keyColumn: "InvoiceTypeID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "InvoiceTypes",
                keyColumn: "InvoiceTypeID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "InvoiceTypes",
                keyColumn: "InvoiceTypeID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "InvoiceTypes",
                keyColumn: "InvoiceTypeID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "InvoiceTypes",
                keyColumn: "InvoiceTypeID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "InvoiceTypes",
                keyColumn: "InvoiceTypeID",
                keyValue: 9);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 7, 15, 24, 703, DateTimeKind.Utc).AddTicks(3241));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 7, 15, 24, 703, DateTimeKind.Utc).AddTicks(3251));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 7, 15, 24, 703, DateTimeKind.Utc).AddTicks(3254));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 7, 15, 24, 703, DateTimeKind.Utc).AddTicks(3300));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 7, 15, 24, 703, DateTimeKind.Utc).AddTicks(3304));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 4, 7, 15, 24, 703, DateTimeKind.Utc).AddTicks(3307));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 7, 15, 24, 861, DateTimeKind.Utc).AddTicks(4196), "$2a$11$R.MvLRchvT8l4vpG6.S.uuaTEoWPDHL1FAAumi8ITZjGjHBtgFnpm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 7, 15, 25, 16, DateTimeKind.Utc).AddTicks(6730), "$2a$11$hq01q1KpTKHfpcbBVd4b3ekNVLErBu8yXGHmYqnf46gD7nSXsS9Qq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 7, 15, 25, 172, DateTimeKind.Utc).AddTicks(4934), "$2a$11$7sY8rQnZL5BhxXKW3cxZzOIwkJw9FnLUMr7KYN/8RNXNLJf46MP7G" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 7, 15, 25, 335, DateTimeKind.Utc).AddTicks(8006), "$2a$11$aQfhrt8w6z1xvcoVspVh8uT6W0alH3NZOXXH3RfX2ZD2oEkDKz9V." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 4, 7, 15, 25, 496, DateTimeKind.Utc).AddTicks(4345), "$2a$11$WmdeeRX.5TB921UIQKj11.uastXkUIbjMHEjsnSPlBAWA27lee/Tm" });
        }
    }
}
