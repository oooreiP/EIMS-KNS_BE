using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedRequestStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 17, 11, 30, 15, 963, DateTimeKind.Utc).AddTicks(6688));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 17, 11, 30, 15, 963, DateTimeKind.Utc).AddTicks(6697));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 17, 11, 30, 15, 963, DateTimeKind.Utc).AddTicks(6699));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 11, 30, 16, 547, DateTimeKind.Utc).AddTicks(8366));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 11, 30, 16, 547, DateTimeKind.Utc).AddTicks(8371));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 11, 30, 16, 547, DateTimeKind.Utc).AddTicks(8374));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 11, 30, 16, 547, DateTimeKind.Utc).AddTicks(8375));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 11, 30, 16, 547, DateTimeKind.Utc).AddTicks(8377));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 11, 30, 16, 547, DateTimeKind.Utc).AddTicks(8378));

            migrationBuilder.InsertData(
                table: "InvoiceRequestStatuses",
                columns: new[] { "StatusID", "Description", "StatusName" },
                values: new object[,]
                {
                    { 1, "Mới tạo, chờ kế toán kiểm tra và duyệt", "Pending" },
                    { 2, "Đã được duyệt và tạo thành hóa đơn chính thức", "Approved" },
                    { 3, "Bị từ chối (cần sửa lại thông tin)", "Rejected" },
                    { 4, "Đã bị hủy bởi người tạo", "Cancelled" },
                    { 5, "Hóa đơn từ yêu cầu đã được phát hành", "Invoice_Issued" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 17, 11, 30, 15, 963, DateTimeKind.Utc).AddTicks(6753));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 17, 11, 30, 15, 963, DateTimeKind.Utc).AddTicks(6756));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 17, 11, 30, 15, 963, DateTimeKind.Utc).AddTicks(6758));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 17, 11, 30, 16, 78, DateTimeKind.Utc).AddTicks(1788), "$2a$11$rJkyPgYsHg.tJFjGdRlyS.Kl41/no4U60Wj5cYyJmv2S9PMc75BvC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 17, 11, 30, 16, 194, DateTimeKind.Utc).AddTicks(1346), "$2a$11$KQoEuHoW6xtd38WFc/RQvunusklgCmjoIVFYF90ioTQmhTRUwtkj2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 17, 11, 30, 16, 313, DateTimeKind.Utc).AddTicks(6275), "$2a$11$6zDvAGFgocKlhU2/fboIoui78H89uvt1Wf3ogMuGYZFvXdpgOEjQ2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 17, 11, 30, 16, 432, DateTimeKind.Utc).AddTicks(6125), "$2a$11$Eb.77P7Ow4oaxuWYQPobhOLhdVNQj4vZRMy9gpUx01plZ7czWsXwK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 17, 11, 30, 16, 545, DateTimeKind.Utc).AddTicks(7511), "$2a$11$q5tU1GS9SQUODQgyg14aPeNB2AG7tXG5vkTnHpUm86GFcKfB3WqMC" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InvoiceRequestStatuses",
                keyColumn: "StatusID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "InvoiceRequestStatuses",
                keyColumn: "StatusID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "InvoiceRequestStatuses",
                keyColumn: "StatusID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "InvoiceRequestStatuses",
                keyColumn: "StatusID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "InvoiceRequestStatuses",
                keyColumn: "StatusID",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 17, 10, 47, 17, 969, DateTimeKind.Utc).AddTicks(7534));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 17, 10, 47, 17, 969, DateTimeKind.Utc).AddTicks(7540));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 17, 10, 47, 17, 969, DateTimeKind.Utc).AddTicks(7543));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 10, 47, 18, 546, DateTimeKind.Utc).AddTicks(223));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 10, 47, 18, 546, DateTimeKind.Utc).AddTicks(228));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 10, 47, 18, 546, DateTimeKind.Utc).AddTicks(230));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 10, 47, 18, 546, DateTimeKind.Utc).AddTicks(231));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 10, 47, 18, 546, DateTimeKind.Utc).AddTicks(233));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 10, 47, 18, 546, DateTimeKind.Utc).AddTicks(234));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 17, 10, 47, 17, 969, DateTimeKind.Utc).AddTicks(7588));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 17, 10, 47, 17, 969, DateTimeKind.Utc).AddTicks(7591));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 17, 10, 47, 17, 969, DateTimeKind.Utc).AddTicks(7594));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 17, 10, 47, 18, 88, DateTimeKind.Utc).AddTicks(3603), "$2a$11$RnswqK0CdYpiEn/utnER1eOyftICyux9/AiAa3dx4vQmSRfRylY2O" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 17, 10, 47, 18, 205, DateTimeKind.Utc).AddTicks(4577), "$2a$11$oi677l/PMWdAd7PNbC4fC.tePV7Hp5Akz9dDMVn05k1XUyfyZR2DO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 17, 10, 47, 18, 318, DateTimeKind.Utc).AddTicks(4136), "$2a$11$kq.On/X4vmwW8rWxgQpRNuKgm/56fl85iZ1EJ5igsz66A3aoqzO.u" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 17, 10, 47, 18, 430, DateTimeKind.Utc).AddTicks(650), "$2a$11$hRoa46yBm0SVzsCqgSxXr.k90obMeowckQjAjViNO8XzjCxqPdmQO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 17, 10, 47, 18, 543, DateTimeKind.Utc).AddTicks(6522), "$2a$11$f3Wnx5j1yvlk.SPBRnOrQe4THx9DLRTUFkVVrd8aQdqpGpDDkL1X." });
        }
    }
}
