using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEvidencePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EvidenceFilePath",
                table: "InvoiceRequests",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 13, 9, 50, 537, DateTimeKind.Utc).AddTicks(1163));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 13, 9, 50, 537, DateTimeKind.Utc).AddTicks(1171));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 13, 9, 50, 537, DateTimeKind.Utc).AddTicks(1173));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 13, 9, 51, 121, DateTimeKind.Utc).AddTicks(1779));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 13, 9, 51, 121, DateTimeKind.Utc).AddTicks(1783));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 13, 9, 51, 121, DateTimeKind.Utc).AddTicks(1785));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 13, 9, 51, 121, DateTimeKind.Utc).AddTicks(1787));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 13, 9, 51, 121, DateTimeKind.Utc).AddTicks(1788));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 13, 9, 51, 121, DateTimeKind.Utc).AddTicks(1790));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 13, 9, 51, 121, DateTimeKind.Utc).AddTicks(1792));

            migrationBuilder.UpdateData(
                table: "InvoiceTypes",
                keyColumn: "InvoiceTypeID",
                keyValue: 1,
                column: "TypeName",
                value: "Hóa đơn Doanh nghiệp, tổ chức, hộ, cá nhân");

            migrationBuilder.UpdateData(
                table: "InvoiceTypes",
                keyColumn: "InvoiceTypeID",
                keyValue: 2,
                column: "TypeName",
                value: "Hóa đơn tài sản công và hóa đơn bán hàng dự trữ quốc gia");

            migrationBuilder.UpdateData(
                table: "Prefixes",
                keyColumn: "PrefixID",
                keyValue: 5,
                column: "PrefixName",
                value: "Hóa đơn điện tử khác là tem điện tử, vé điện tử");

            migrationBuilder.UpdateData(
                table: "Prefixes",
                keyColumn: "PrefixID",
                keyValue: 6,
                column: "PrefixName",
                value: "Chứng từ điện tử được sử dụng và quản lý");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 13, 9, 50, 537, DateTimeKind.Utc).AddTicks(1211));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 13, 9, 50, 537, DateTimeKind.Utc).AddTicks(1247));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 24, 13, 9, 50, 537, DateTimeKind.Utc).AddTicks(1250));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 24, 13, 9, 50, 653, DateTimeKind.Utc).AddTicks(7942), "$2a$11$khVzUf078IQU7xOnrpGWn.xao47RFwY/.NZIPRTslcSbQvkt3KKra" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 24, 13, 9, 50, 767, DateTimeKind.Utc).AddTicks(9723), "$2a$11$FgJ6SNtba6DTdBnhxSpMH.fGuFtnlXLSULZmXVXNaRZVYpNDArQSC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 24, 13, 9, 50, 888, DateTimeKind.Utc).AddTicks(5954), "$2a$11$gOAL4HZd2wh0lKxdPVPQpupYu/QmIks7eRLI3JRkBeJNgKjYY/v3y" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 24, 13, 9, 51, 6, DateTimeKind.Utc).AddTicks(2900), "$2a$11$7oiwaDQkZqxP5gZJdFxY4.wjTWXzZT.FBQtZZf3MiqvCKPFLgFllC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 24, 13, 9, 51, 121, DateTimeKind.Utc).AddTicks(523), "$2a$11$tD9Rpp8pnE9wzDusuY0Q1eGF88ZI9J0aSV0HSFQLiGAW7uCLxCq3W" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EvidenceFilePath",
                table: "InvoiceRequests");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 48, 7, 920, DateTimeKind.Utc).AddTicks(1868));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 48, 7, 920, DateTimeKind.Utc).AddTicks(1876));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 48, 7, 920, DateTimeKind.Utc).AddTicks(1879));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 48, 8, 691, DateTimeKind.Utc).AddTicks(6826));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 48, 8, 691, DateTimeKind.Utc).AddTicks(6831));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 48, 8, 691, DateTimeKind.Utc).AddTicks(6834));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 48, 8, 691, DateTimeKind.Utc).AddTicks(6836));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 48, 8, 691, DateTimeKind.Utc).AddTicks(6839));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 48, 8, 691, DateTimeKind.Utc).AddTicks(6841));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 19, 48, 8, 691, DateTimeKind.Utc).AddTicks(6844));

            migrationBuilder.UpdateData(
                table: "InvoiceTypes",
                keyColumn: "InvoiceTypeID",
                keyValue: 1,
                column: "TypeName",
                value: "Hóa đơn Doanh nghiệp, tổ chức, hộ, cá nhân kinh doanh đăng ký sử dụng");

            migrationBuilder.UpdateData(
                table: "InvoiceTypes",
                keyColumn: "InvoiceTypeID",
                keyValue: 2,
                column: "TypeName",
                value: "Hóa đơn tài sản công và hóa đơn bán hàng dự trữ quốc gia hoặc hóa đơn điện tử đặc thù không nhất thiết phải có một số tiêu thức do các doanh nghiệp, tổ chức đăng ký sử dụng");

            migrationBuilder.UpdateData(
                table: "Prefixes",
                keyColumn: "PrefixID",
                keyValue: 5,
                column: "PrefixName",
                value: "Hóa đơn điện tử khác là tem điện tử, vé điện tử, thẻ điện tử, phiếu thu điện tử hoặc các chứng từ điện tử có tên gọi khác nhưng có nội dung của hóa đơn điện tử theo quy định tại Nghị định số 123/2020/NĐ-CP");

            migrationBuilder.UpdateData(
                table: "Prefixes",
                keyColumn: "PrefixID",
                keyValue: 6,
                column: "PrefixName",
                value: "Chứng từ điện tử được sử dụng và quản lý như hóa đơn gồm phiếu xuất kho kiêm vận chuyển nội bộ điện tử, phiếu xuất kho hàng gửi bán đại lý điện tử");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 48, 7, 920, DateTimeKind.Utc).AddTicks(1962));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 48, 7, 920, DateTimeKind.Utc).AddTicks(1966));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 23, 19, 48, 7, 920, DateTimeKind.Utc).AddTicks(1969));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 19, 48, 8, 74, DateTimeKind.Utc).AddTicks(2640), "$2a$11$bQsDV79SFDM4lb6lyBoZVukQrja.x1tvR5owPLbpVi11q97nTtwby" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 19, 48, 8, 227, DateTimeKind.Utc).AddTicks(3561), "$2a$11$jhexrJObLtmQVzmVSGvuG.D38fV4s/PAqsI2gGKUQL8iqdWKuM0di" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 19, 48, 8, 381, DateTimeKind.Utc).AddTicks(6144), "$2a$11$OMyR10Ckoxc.whJz03gy1ukQi7NoLLZZk/4p1kOZQf28muwjOawOW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 19, 48, 8, 534, DateTimeKind.Utc).AddTicks(6446), "$2a$11$4egIZNVCEbqDXDQEnYhbSuNc8IQxz8CY4U6gctO9k9uOF1JJ5jtn." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 23, 19, 48, 8, 691, DateTimeKind.Utc).AddTicks(5113), "$2a$11$pQ55II7yll.YVXFQk.ZDA.k8XExjggNqffGnfzMesI0RY3XHwpZP2" });
        }
    }
}
