using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTaxMessageCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceHistories_Users_PerformedBy",
                table: "InvoiceHistories");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceHistories_PerformedBy",
                table: "InvoiceHistories");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "TaxApiStatuses",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MCCQT",
                table: "TaxApiLogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MTDiep",
                table: "TaxApiLogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoTBao",
                table: "TaxApiLogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MCCQT",
                table: "Invoices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PerformerUserID",
                table: "InvoiceHistories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "tax_message_codes",
                columns: table => new
                {
                    stt = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    message_code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    message_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    flow_type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tax_message_codes", x => x.stt);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 22, 18, 15, 16, 357, DateTimeKind.Utc).AddTicks(4555));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 22, 18, 15, 16, 357, DateTimeKind.Utc).AddTicks(4562));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 22, 18, 15, 16, 357, DateTimeKind.Utc).AddTicks(4564));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 22, 18, 15, 16, 357, DateTimeKind.Utc).AddTicks(4596));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 22, 18, 15, 16, 357, DateTimeKind.Utc).AddTicks(4600));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 22, 18, 15, 16, 357, DateTimeKind.Utc).AddTicks(4602));

            migrationBuilder.InsertData(
                table: "TaxApiStatuses",
                columns: new[] { "TaxApiStatusID", "Code", "StatusName" },
                values: new object[,]
                {
                    { 1, "PENDING", "Đang gửi CQT" },
                    { 2, "RECEIVED", "CQT đã tiếp nhận" },
                    { 3, "REJECTED", "CQT từ chối" },
                    { 4, "APPROVED", "CQT đã cấp mã" },
                    { 5, "FAILED", "Lỗi hệ thống" },
                    { 6, "PROCESSING", "Đang xử lý" },
                    { 7, "NOT_FOUND", "Không tìm thấy hóa đơn" },
                    { 10, "TB01", "Tiếp nhận hợp lệ" },
                    { 11, "TB02", "Từ chối: Sai định dạng XML/XSD" },
                    { 12, "TB03", "Từ chối: Chữ ký số không hợp lệ" },
                    { 13, "TB04", "Từ chối: MST không đúng" },
                    { 14, "TB05", "Từ chối: Thiếu thông tin bắt buộc" },
                    { 15, "TB06", "Từ chối: Sai định dạng dữ liệu" },
                    { 16, "TB07", "Từ chối: Trùng hóa đơn" },
                    { 17, "TB08", "Từ chối: Hóa đơn không được cấp mã" },
                    { 18, "TB09", "Từ chối: Không tìm thấy hóa đơn tham chiếu" },
                    { 19, "TB10", "Từ chối: Thông tin hàng hóa không hợp lệ" },
                    { 20, "TB11", "Từ chối: Bản thể hiện PDF sai cấu trúc" },
                    { 21, "TB12", "Lỗi kỹ thuật hệ thống thuế" },
                    { 30, "KQ01", "Đã cấp mã CQT" },
                    { 31, "KQ02", "Bị từ chối khi cấp mã" },
                    { 32, "KQ03", "Chưa có kết quả xử lý" },
                    { 33, "KQ04", "Không tìm thấy hóa đơn" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 22, 18, 15, 16, 469, DateTimeKind.Utc).AddTicks(497), "$2a$11$uo5aQNwVXfcQzOT47OSGJuFoEPE3dbq2vZMOhyuOaVjc/qYWPboTO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 22, 18, 15, 16, 580, DateTimeKind.Utc).AddTicks(7399), "$2a$11$rVgZaCfX1j9.G8fjG1VtLOPxsrrz7CCgkK.vWDTAIxz.7xixVGmGW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 22, 18, 15, 16, 694, DateTimeKind.Utc).AddTicks(2809), "$2a$11$uxZpXX0Y0BoFCrREPvwB4O1v7hdSVPhGaDK6OqXJtuUhPsb3qtHgC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 22, 18, 15, 16, 806, DateTimeKind.Utc).AddTicks(7035), "$2a$11$.aJZpgGSa2DDbAc3.cn5p.IhRE.zeIjh14QuEfWpKtMW55iIpRXa." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 22, 18, 15, 16, 924, DateTimeKind.Utc).AddTicks(1706), "$2a$11$bHr1kyZA9NSuEQijhqcic.9zbTUoZDgZg7qM5c1SzL3IMUwBUlFAG" });

            migrationBuilder.InsertData(
                table: "tax_message_codes",
                columns: new[] { "stt", "category", "description", "flow_type", "message_code", "message_name" },
                values: new object[,]
                {
                    { 1, "Đăng ký", null, 1, "100", "Thông điệp gửi tờ khai đăng ký/thay đổi thông tin sử dụng hóa đơn điện tử" },
                    { 2, "Đăng ký", null, 1, "101", "Thông điệp gửi tờ khai đăng ký thay đổi thông tin đăng ký sử dụng HĐĐT khi ủy nhiệm/nhận ủy nhiệm lập hóa đơn" },
                    { 3, "Đăng ký", null, 2, "102", "Thông điệp tiếp nhận/không tiếp nhận tờ khai đăng ký HĐĐT" },
                    { 4, "Đăng ký", null, 2, "103", "Thông điệp chấp nhận/không chấp nhận đăng ký HĐĐT" },
                    { 5, "Đăng ký", null, 2, "104", "Thông điệp chấp nhận/không chấp nhận thay đổi thông tin đăng ký HĐĐT khi ủy nhiệm" },
                    { 6, "Đăng ký", null, 2, "105", "Thông điệp hết thời gian sử dụng HĐĐT có mã" },
                    { 7, "Đăng ký", null, 1, "106", "Thông điệp gửi Đơn đề nghị cấp HĐĐT có mã theo từng lần" },
                    { 8, "Đăng ký", null, 2, "107", "Thông điệp yêu cầu giải trình/bổ sung tài liệu" },
                    { 9, "Đăng ký", null, 2, "108", "Thông điệp ngừng sử dụng hóa đơn điện tử" },
                    { 10, "Đăng ký", null, 1, "109", "Thông điệp gửi tờ khai đăng ký/thay đổi thông tin sử dụng chứng từ điện tử" },
                    { 11, "Đăng ký", null, 1, "110", "Thông điệp gửi tờ khai đăng ký thay đổi thông tin CTĐT khi ủy nhiệm" },
                    { 12, "Đăng ký", null, 2, "111", "Thông điệp tiếp nhận/không tiếp nhận tờ khai CTĐT" },
                    { 13, "Đăng ký", null, 2, "112", "Thông điệp chấp nhận/không chấp nhận đăng ký CTĐT" },
                    { 14, "Đăng ký", null, 2, "113", "Thông điệp chấp nhận/không chấp nhận đăng ký CTĐT khi ủy nhiệm" },
                    { 15, "Đăng ký", null, 2, "114", "Thông điệp thông báo về việc NNT hủy tờ khai hoặc thông báo" },
                    { 16, "Tài khoản Cổng TCT", null, 1, "150", "Thông điệp gửi tờ khai đăng ký mới/thay đổi thông tin/Chấm dứt tài khoản Cổng TCT" },
                    { 17, "Tài khoản Cổng TCT", null, 2, "151", "Thông điệp tiếp nhận/không tiếp nhận tài khoản Cổng TCT" },
                    { 18, "Tài khoản Cổng TCT", null, 2, "152", "Thông điệp chấp nhận/không chấp nhận tài khoản Cổng TCT" },
                    { 19, "Hóa đơn Cấp mã", null, 1, "200", "Thông điệp gửi hóa đơn điện tử tới cơ quan thuế để cấp mã" },
                    { 20, "Hóa đơn Cấp mã", null, 1, "201", "Thông điệp gửi hóa đơn điện tử theo từng lần phát sinh" },
                    { 21, "Hóa đơn Cấp mã", null, 2, "202", "Thông điệp kết quả cấp mã hóa đơn điện tử" },
                    { 22, "Hóa đơn Không mã", null, 1, "203", "Thông điệp gửi dữ liệu hóa đơn không mã" },
                    { 23, "Hóa đơn Kiểm tra", null, 2, "204", "Kết quả kiểm tra dữ liệu hóa đơn điện tử (TB01/KTDL)" },
                    { 24, "Hóa đơn Cấp mã", null, 2, "205", "Phản hồi hồ sơ đề nghị cấp mã theo từng lần" },
                    { 25, "HĐ Máy tính tiền", null, 1, "206", "Thông điệp gửi hóa đơn từ máy tính tiền" },
                    { 26, "HĐ Đặc thù", null, 1, "207", "Thông điệp gửi hóa đơn Casino" },
                    { 27, "HĐ Điều chỉnh", null, 1, "208", "Thông điệp gửi hóa đơn điều chỉnh/thay thế nhiều" },
                    { 28, "HĐ Đặc thù", null, 1, "209", "Thông điệp gửi hóa đơn tích hợp biên lai" },
                    { 29, "HĐ Sai sót", null, 1, "300", "Thông điệp thông báo hóa đơn có sai sót" },
                    { 30, "HĐ Sai sót", null, 2, "301", "Tiếp nhận và kết quả xử lý hóa đơn sai sót" },
                    { 31, "HĐ Sai sót", null, 2, "302", "Thông điệp thông báo hóa đơn cần rà soát" },
                    { 32, "HĐ Sai sót", null, 1, "303", "Thông điệp thông báo hóa đơn từ máy tính tiền có sai sót" },
                    { 33, "Bảng Tổng hợp", null, 1, "400", "Thông điệp chuyển bảng tổng hợp dữ liệu hóa đơn điện tử" },
                    { 34, "Bảng Tổng hợp", null, 1, "401", "Chuyển bảng tổng hợp + bảng kê điều chỉnh/thay thế" },
                    { 35, "TCTN Ủy quyền", null, 1, "500", "Chuyển dữ liệu HĐĐT TCTN ủy quyền cấp mã" },
                    { 36, "TCTN Ủy quyền", null, 1, "503", "Chuyển dữ liệu hóa đơn không đủ điều kiện cấp mã" },
                    { 37, "TCTN Ủy quyền", null, 2, "504", "Chuyển dữ liệu TB01/KTDL của TCUQ gửi NNT" },
                    { 38, "TCTN Cung cấp", null, 1, "505", "Cung cấp MST thay đổi thông tin trong ngày" },
                    { 39, "TCTN Cung cấp", null, 1, "506", "Cung cấp quyết định ngừng/tiếp tục sử dụng hóa đơn" },
                    { 40, "TCTN Cung cấp", null, 1, "507", "Cung cấp thông tin đăng ký sử dụng hóa đơn điện tử" },
                    { 41, "Ký số Ủy quyền", null, 1, "600", "Đề nghị ký số hóa đơn cấp mã" },
                    { 42, "Ký số Ủy quyền", null, 2, "601", "Tổng cục Thuế ký số hóa đơn đã cấp mã" },
                    { 43, "Ký số Ủy quyền", null, 1, "602", "Đề nghị ký số lên thông báo" },
                    { 44, "Ký số Ủy quyền", null, 2, "603", "Tổng cục Thuế ký số thông báo" },
                    { 45, "Chứng từ Sai sót", null, 1, "700", "Gửi thông báo chứng từ điện tử sai" },
                    { 46, "Chứng từ Sai sót", null, 2, "702", "Tiếp nhận & xử lý CTĐT sai" },
                    { 47, "Chứng từ Sai sót", null, 2, "703", "Thông báo CTĐT cần rà soát" },
                    { 48, "Chứng từ Sai sót", null, 2, "704", "Thông báo kết quả kiểm tra CTĐT" },
                    { 49, "Phản hồi Khác", null, 2, "999", "Thông điệp phản hồi kỹ thuật" },
                    { 50, "Báo cáo Đối soát", null, 1, "901", "Báo cáo đối soát hàng ngày" },
                    { 51, "Báo cáo Đối soát", null, 1, "902", "Báo cáo đối soát TCTN" },
                    { 52, "Phản hồi Khác", null, 2, "903", "Phản hồi kỹ thuật CTĐT" },
                    { 53, "Phản hồi Khác", null, 2, "-1", "Thông điệp phản hồi sai định dạng XML" },
                    { 54, "Phản hồi Khác", null, 2, "-2", "Dữ liệu đề nghị ký số bị lỗi" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceHistories_PerformerUserID",
                table: "InvoiceHistories",
                column: "PerformerUserID");

            migrationBuilder.CreateIndex(
                name: "IX_tax_message_codes_message_code",
                table: "tax_message_codes",
                column: "message_code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceHistories_Users_PerformerUserID",
                table: "InvoiceHistories",
                column: "PerformerUserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceHistories_Users_PerformerUserID",
                table: "InvoiceHistories");

            migrationBuilder.DropTable(
                name: "tax_message_codes");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceHistories_PerformerUserID",
                table: "InvoiceHistories");

            migrationBuilder.DeleteData(
                table: "TaxApiStatuses",
                keyColumn: "TaxApiStatusID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TaxApiStatuses",
                keyColumn: "TaxApiStatusID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TaxApiStatuses",
                keyColumn: "TaxApiStatusID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TaxApiStatuses",
                keyColumn: "TaxApiStatusID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TaxApiStatuses",
                keyColumn: "TaxApiStatusID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TaxApiStatuses",
                keyColumn: "TaxApiStatusID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TaxApiStatuses",
                keyColumn: "TaxApiStatusID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TaxApiStatuses",
                keyColumn: "TaxApiStatusID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "TaxApiStatuses",
                keyColumn: "TaxApiStatusID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "TaxApiStatuses",
                keyColumn: "TaxApiStatusID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "TaxApiStatuses",
                keyColumn: "TaxApiStatusID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "TaxApiStatuses",
                keyColumn: "TaxApiStatusID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "TaxApiStatuses",
                keyColumn: "TaxApiStatusID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "TaxApiStatuses",
                keyColumn: "TaxApiStatusID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "TaxApiStatuses",
                keyColumn: "TaxApiStatusID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "TaxApiStatuses",
                keyColumn: "TaxApiStatusID",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "TaxApiStatuses",
                keyColumn: "TaxApiStatusID",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "TaxApiStatuses",
                keyColumn: "TaxApiStatusID",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "TaxApiStatuses",
                keyColumn: "TaxApiStatusID",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "TaxApiStatuses",
                keyColumn: "TaxApiStatusID",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "TaxApiStatuses",
                keyColumn: "TaxApiStatusID",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "TaxApiStatuses",
                keyColumn: "TaxApiStatusID",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "TaxApiStatuses",
                keyColumn: "TaxApiStatusID",
                keyValue: 33);

            migrationBuilder.DropColumn(
                name: "Code",
                table: "TaxApiStatuses");

            migrationBuilder.DropColumn(
                name: "MCCQT",
                table: "TaxApiLogs");

            migrationBuilder.DropColumn(
                name: "MTDiep",
                table: "TaxApiLogs");

            migrationBuilder.DropColumn(
                name: "SoTBao",
                table: "TaxApiLogs");

            migrationBuilder.DropColumn(
                name: "MCCQT",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "PerformerUserID",
                table: "InvoiceHistories");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 12, 4, 14, 17, 554, DateTimeKind.Utc).AddTicks(4203));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 12, 4, 14, 17, 554, DateTimeKind.Utc).AddTicks(4208));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 12, 4, 14, 17, 554, DateTimeKind.Utc).AddTicks(4210));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 12, 4, 14, 17, 554, DateTimeKind.Utc).AddTicks(4249));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 12, 4, 14, 17, 554, DateTimeKind.Utc).AddTicks(4252));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 12, 4, 14, 17, 554, DateTimeKind.Utc).AddTicks(4254));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 12, 4, 14, 17, 667, DateTimeKind.Utc).AddTicks(262), "$2a$11$l50Si39oMYCU63V8JvMXOOZXF1usqtRdNFVghu9tW6HyvurVkgmTu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 12, 4, 14, 17, 779, DateTimeKind.Utc).AddTicks(3492), "$2a$11$9J2ulvdxR60zbydDuKm54uwcxvoD.z2WLWLS0hMAjjrjmCtq7Xyzm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 12, 4, 14, 17, 898, DateTimeKind.Utc).AddTicks(4499), "$2a$11$RfNZWSVXTgtncvkNENGMSebbG5bC4dg108YbisQ6U2m2OxLb.kOIe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 12, 4, 14, 18, 12, DateTimeKind.Utc).AddTicks(5698), "$2a$11$CqiCG7eQqdgeBY5w4Pv7HeY.yVxAQklsq3qhuKpJCF/Z.zQD7VIS2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 11, 12, 4, 14, 18, 122, DateTimeKind.Utc).AddTicks(4468), "$2a$11$VG6sTeb5NYuEcBZA5fVRbe/4gWaBzNMMMt98LSKhSGO54cUhR2Qui" });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceHistories_PerformedBy",
                table: "InvoiceHistories",
                column: "PerformedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceHistories_Users_PerformedBy",
                table: "InvoiceHistories",
                column: "PerformedBy",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
