using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 6, 10, 6, 285, DateTimeKind.Utc).AddTicks(2998));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 6, 10, 6, 285, DateTimeKind.Utc).AddTicks(3009));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 6, 10, 6, 285, DateTimeKind.Utc).AddTicks(3015));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 6, 10, 6, 285, DateTimeKind.Utc).AddTicks(3065));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 6, 10, 6, 285, DateTimeKind.Utc).AddTicks(3071));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 6, 10, 6, 285, DateTimeKind.Utc).AddTicks(3075));

            migrationBuilder.UpdateData(
                table: "TemplateFrames",
                keyColumn: "FrameID",
                keyValue: 6,
                column: "ImageUrl",
                value: "https://res.cloudinary.com/djz86r9zd/image/upload/v1764156286/khunghoadon9_smq1lj.png");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 29, 6, 10, 6, 520, DateTimeKind.Utc).AddTicks(1429), "$2a$11$DNhZbDW25T9Oiuq6hitR2OPgIYJn.jSknkBskJIzdqSpRW6LJYoa." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 29, 6, 10, 6, 711, DateTimeKind.Utc).AddTicks(2017), "$2a$11$/lzImO4eT7e9Z8Mdi5V3FOvSoW62fHpRnCDVNeCVEpTeztd/./TFG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 29, 6, 10, 6, 910, DateTimeKind.Utc).AddTicks(3069), "$2a$11$YGDA1WeF3yOBBjfyCGXTxOMCmLX3zOkEh.stkcoCQpDc8TDVHCZs6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 29, 6, 10, 7, 150, DateTimeKind.Utc).AddTicks(9801), "$2a$11$81xBzyv1xw5sRPkjkSdE.OFUZ01oK6OH2a1v3Ns8UxJQ1YxoMvuQy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 29, 6, 10, 7, 406, DateTimeKind.Utc).AddTicks(1893), "$2a$11$8GtVrPLsDI/LD.4wzPl4r.K/6TZYqBdmMw93W1qbCb92SmpH9i66O" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 11, 47, 45, 633, DateTimeKind.Utc).AddTicks(5508));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 11, 47, 45, 633, DateTimeKind.Utc).AddTicks(5559));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 11, 47, 45, 633, DateTimeKind.Utc).AddTicks(5563));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 11, 47, 45, 633, DateTimeKind.Utc).AddTicks(5609));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 11, 47, 45, 633, DateTimeKind.Utc).AddTicks(5613));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 11, 47, 45, 633, DateTimeKind.Utc).AddTicks(5616));

            migrationBuilder.UpdateData(
                table: "TemplateFrames",
                keyColumn: "FrameID",
                keyValue: 6,
                column: "ImageUrl",
                value: "https://res.cloudinary.com/djz86r9zd/image/upload/v1764156286/khunghoadon9_smq1lj.pngg");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 11, 47, 45, 788, DateTimeKind.Utc).AddTicks(1471), "$2a$11$1052QeF7p5.7fATd0G2U3eui1f4l4xrAhDd4MgqK1maUqkjZFUTnm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 11, 47, 45, 945, DateTimeKind.Utc).AddTicks(6562), "$2a$11$jGM7VUweX837rEImPq2yGOj/hQDyWrbWq8EeLWVGjaUcGkpo8hFnS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 11, 47, 46, 103, DateTimeKind.Utc).AddTicks(1227), "$2a$11$2b2kaOELIG8A3pnIA03bfudN6SpdD151PHvvkyYm9MEUaehAmvNBS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 11, 47, 46, 261, DateTimeKind.Utc).AddTicks(4851), "$2a$11$xQQYwKtc/70/RFuCH1xjhu8wajVPHYy.j6kUKcWAJ8MgatOxzEmPy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 11, 47, 46, 422, DateTimeKind.Utc).AddTicks(7570), "$2a$11$XCpiZ1H5fFNWpLi2nudfK..mxQLwbRHUrd7q4Yh0QCaPkXJNKL/Ze" });
        }
    }
}
