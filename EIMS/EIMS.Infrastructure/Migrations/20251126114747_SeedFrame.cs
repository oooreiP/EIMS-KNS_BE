using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedFrame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "TemplateFrames",
                columns: new[] { "FrameID", "Description", "FrameName", "ImageUrl", "IsActive" },
                values: new object[,]
                {
                    { 1, null, "Frame 1", "https://res.cloudinary.com/djz86r9zd/image/upload/v1764156291/khunghoadon11_kqjill.png", true },
                    { 2, null, "Frame 2", "https://res.cloudinary.com/djz86r9zd/image/upload/v1764156289/khunghoadon3_utka5u.png", true },
                    { 3, null, "Frame 3", "https://res.cloudinary.com/djz86r9zd/image/upload/v1764156287/khunghoadon10_pjapiv.png", true },
                    { 4, null, "Frame 4", "https://res.cloudinary.com/djz86r9zd/image/upload/v1764156286/khunghoadon7_shsqte.png", true },
                    { 5, null, "Frame 5", "https://res.cloudinary.com/djz86r9zd/image/upload/v1764156286/khunghoadon4_o9xatr.png", true },
                    { 6, null, "Frame 6", "https://res.cloudinary.com/djz86r9zd/image/upload/v1764156286/khunghoadon9_smq1lj.pngg", true },
                    { 7, null, "Frame 7", "https://res.cloudinary.com/djz86r9zd/image/upload/v1764156285/khunghoadon5_tveg16.png", true },
                    { 8, null, "Frame 8", "https://res.cloudinary.com/djz86r9zd/image/upload/v1764156285/khunghoadon6_mp5fh1.png", true },
                    { 9, null, "Frame 9", "https://res.cloudinary.com/djz86r9zd/image/upload/v1764156285/khunghoadon8_d5ho2y.png", true },
                    { 10, null, "Frame 10", "https://res.cloudinary.com/djz86r9zd/image/upload/v1764156284/khunghoadon2_vamivw.png", true },
                    { 11, null, "Frame 11", "https://res.cloudinary.com/djz86r9zd/image/upload/v1764156219/khunghoadon1_urc2b5.png", true }
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TemplateFrames",
                keyColumn: "FrameID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TemplateFrames",
                keyColumn: "FrameID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TemplateFrames",
                keyColumn: "FrameID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TemplateFrames",
                keyColumn: "FrameID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TemplateFrames",
                keyColumn: "FrameID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TemplateFrames",
                keyColumn: "FrameID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TemplateFrames",
                keyColumn: "FrameID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TemplateFrames",
                keyColumn: "FrameID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TemplateFrames",
                keyColumn: "FrameID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TemplateFrames",
                keyColumn: "FrameID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "TemplateFrames",
                keyColumn: "FrameID",
                keyValue: 11);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 11, 35, 19, 462, DateTimeKind.Utc).AddTicks(8894));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 11, 35, 19, 462, DateTimeKind.Utc).AddTicks(8905));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 11, 35, 19, 462, DateTimeKind.Utc).AddTicks(8908));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 11, 35, 19, 462, DateTimeKind.Utc).AddTicks(8970));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 11, 35, 19, 462, DateTimeKind.Utc).AddTicks(8975));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 26, 11, 35, 19, 462, DateTimeKind.Utc).AddTicks(8979));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 11, 35, 19, 616, DateTimeKind.Utc).AddTicks(9062), "$2a$11$JbpB7uHjffVkB.l5B/k.QOiS57YDskVBwJOu505KqHjA9Xr0bRJFu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 11, 35, 19, 770, DateTimeKind.Utc).AddTicks(434), "$2a$11$OE6htJLkwMHvvHSjinTbwOG0hxNUZVmuttaszWlbumhoFLcjRCWfy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 11, 35, 19, 924, DateTimeKind.Utc).AddTicks(4212), "$2a$11$zCppPaBtMf83BVPhMJ6WRO2TPr2lzscM7iPjc1iuiNstDAEgOtvJ." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 11, 35, 20, 77, DateTimeKind.Utc).AddTicks(195), "$2a$11$JvCfN4d/RZiuWNokTCeZMuNVCMxcwzXdhukhgfy64cRP/chHHoWm2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 26, 11, 35, 20, 231, DateTimeKind.Utc).AddTicks(3759), "$2a$11$xt/PXiFI/vFPG/0b5PIVIekab6K80873H64XwnQHoh26St7jrSMm6" });
        }
    }
}
