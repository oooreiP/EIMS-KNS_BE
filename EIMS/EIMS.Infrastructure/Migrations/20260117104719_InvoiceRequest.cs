using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceRequestStatuses",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceRequestStatuses", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceRequests",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RequestStatusID = table.Column<int>(type: "integer", nullable: false),
                    CompanyID = table.Column<int>(type: "integer", nullable: true),
                    CustomerID = table.Column<int>(type: "integer", nullable: false),
                    SaleID = table.Column<int>(type: "integer", nullable: true),
                    CreatedInvoiceID = table.Column<int>(type: "integer", nullable: true),
                    PaymentMethod = table.Column<string>(type: "text", nullable: true),
                    SubtotalAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    VATRate = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    VATAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TotalAmountInWords = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Notes = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    MinRows = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    InvoiceCustomerName = table.Column<string>(type: "text", nullable: false),
                    InvoiceCustomerAddress = table.Column<string>(type: "text", nullable: false),
                    InvoiceCustomerTaxCode = table.Column<string>(type: "text", nullable: false),
                    SalesUserID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceRequests", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK_InvoiceRequests_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                    table.ForeignKey(
                        name: "FK_InvoiceRequests_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceRequests_InvoiceRequestStatuses_RequestStatusID",
                        column: x => x.RequestStatusID,
                        principalTable: "InvoiceRequestStatuses",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceRequests_Invoices_CreatedInvoiceID",
                        column: x => x.CreatedInvoiceID,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceID");
                    table.ForeignKey(
                        name: "FK_InvoiceRequests_Users_SalesUserID",
                        column: x => x.SalesUserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceRequestItems",
                columns: table => new
                {
                    RequestItemID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RequestID = table.Column<int>(type: "integer", nullable: false),
                    ProductID = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(18,4)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    VATAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceRequestItems", x => x.RequestItemID);
                    table.ForeignKey(
                        name: "FK_InvoiceRequestItems_InvoiceRequests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "InvoiceRequests",
                        principalColumn: "RequestID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceRequestItems_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "InvoiceTypes",
                columns: new[] { "InvoiceTypeID", "Symbol", "TypeName" },
                values: new object[] { -1, "0", "Hóa đơn hệ thống không có ký hiệu" });

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

            migrationBuilder.InsertData(
                table: "SerialStatuses",
                columns: new[] { "SerialStatusID", "StatusName", "Symbol" },
                values: new object[] { -1, "Hóa đơn hệ thống không có ký hiệu", "0" });

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

            migrationBuilder.InsertData(
                table: "Serials",
                columns: new[] { "SerialID", "CurrentInvoiceNumber", "InvoiceTypeID", "PrefixID", "SerialStatusID", "Tail", "Year" },
                values: new object[] { -1, 0L, -1, 1, -1, "00", "00" });

            migrationBuilder.InsertData(
                table: "InvoiceTemplates",
                columns: new[] { "TemplateID", "CreatedByUserID", "IsActive", "LayoutDefinition", "LogoUrl", "SerialID", "TemplateFrameID", "TemplateName", "TemplateTypeID" },
                values: new object[] { -1, 1, true, "{\"table\": {\"columns\": [{\"id\": \"code\", \"label\": \"Mã hàng\", \"hasCode\": false, \"visible\": false}, {\"id\": \"name\", \"label\": \"Tên hàng hóa, dịch vụ\", \"hasCode\": false, \"visible\": false}, {\"id\": \"specs\", \"label\": \"Quy cách\", \"hasCode\": false, \"visible\": false}, {\"id\": \"unit\", \"label\": \"Đơn vị tính\", \"hasCode\": false, \"visible\": true}, {\"id\": \"quantity\", \"label\": \"Số lượng\", \"hasCode\": true, \"visible\": true}, {\"id\": \"price\", \"label\": \"Đơn giá\", \"hasCode\": true, \"visible\": true}, {\"id\": \"amount\", \"label\": \"Thành tiền\", \"hasCode\": true, \"visible\": false}, {\"id\": \"note\", \"label\": \"Ghi chú\", \"hasCode\": false, \"visible\": false}, {\"id\": \"warehouse\", \"label\": \"Kho nhập\", \"hasCode\": false, \"visible\": false}], \"rowCount\": 5, \"sttTitle\": \"STT\", \"sttContent\": \"[STT]\"}, \"company\": {\"name\": \"Công ty Cổ phần Giải pháp Tổng thể Kỷ Nguyên Số\", \"phone\": \"(028) 38 995 822\", \"fields\": [{\"id\": \"name\", \"label\": \"Đơn vị bán\", \"value\": \"Công ty Cổ phần Giải pháp Tổng thể Kỷ Nguyên Số\", \"visible\": true}, {\"id\": \"taxCode\", \"label\": \"Mã số thuế\", \"value\": \"0316882091\", \"visible\": false}, {\"id\": \"address\", \"label\": \"Địa chỉ\", \"value\": \"Tòa nhà ABC, 123 Đường XYZ, Phường Tân Định, Quận 1, TP. Hồ Chí Minh, Việt Nam\", \"visible\": true}, {\"id\": \"phone\", \"label\": \"Điện thoại\", \"value\": \"(028) 38 995 822\", \"visible\": true}, {\"id\": \"fax\", \"label\": \"Fax\", \"value\": \"\", \"visible\": false}, {\"id\": \"website\", \"label\": \"Website\", \"value\": \"kns.com.vn\", \"visible\": false}, {\"id\": \"email\", \"label\": \"Email\", \"value\": \"contact@kns.com.vn\", \"visible\": false}, {\"id\": \"bankAccount\", \"label\": \"Số tài khoản\", \"value\": \"245889119 - Ngân hàng TMCP Á Châu - CN Sài Gòn\", \"visible\": true}], \"address\": \"Tòa nhà ABC, 123 Đường XYZ, Phường Tân Định, Quận 1, TP. Hồ Chí Minh, Việt Nam\", \"taxCode\": \"0316882091\", \"bankAccount\": \"245889119 - Ngân hàng TMCP Á Châu - CN Sài Gòn\"}, \"settings\": {\"bilingual\": false, \"numberFont\": \"arial\", \"showQrCode\": true, \"visibility\": {\"showLogo\": true, \"showSignature\": true, \"showCompanyName\": true, \"showCompanyPhone\": true, \"showCompanyAddress\": true, \"showCompanyTaxCode\": false, \"showCompanyBankAccount\": true}, \"customerVisibility\": {\"customerName\": false, \"customerEmail\": false, \"customerPhone\": false, \"paymentMethod\": false, \"customerAddress\": false, \"customerTaxCode\": false}}, \"modelCode\": \"01GTKT\", \"background\": {\"frame\": \"https://res.cloudinary.com/djz86r9zd/image/upload/v1764156289/khunghoadon3_utka5u.png\", \"custom\": null}, \"invoiceDate\": \"2025-11-28T04:56:57.273Z\", \"templateCode\": \"1000000\"}", null, -1, 1, "Hóa hệ thống không có ký hiệu", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRequestItems_ProductID",
                table: "InvoiceRequestItems",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRequestItems_RequestID",
                table: "InvoiceRequestItems",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRequests_CompanyID",
                table: "InvoiceRequests",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRequests_CreatedInvoiceID",
                table: "InvoiceRequests",
                column: "CreatedInvoiceID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRequests_CustomerID",
                table: "InvoiceRequests",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRequests_RequestStatusID",
                table: "InvoiceRequests",
                column: "RequestStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRequests_SalesUserID",
                table: "InvoiceRequests",
                column: "SalesUserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceRequestItems");

            migrationBuilder.DropTable(
                name: "InvoiceRequests");

            migrationBuilder.DropTable(
                name: "InvoiceRequestStatuses");

            migrationBuilder.DeleteData(
                table: "InvoiceTemplates",
                keyColumn: "TemplateID",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "Serials",
                keyColumn: "SerialID",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "InvoiceTypes",
                keyColumn: "InvoiceTypeID",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "SerialStatuses",
                keyColumn: "SerialStatusID",
                keyValue: -1);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 16, 20, 10, 41, 66, DateTimeKind.Utc).AddTicks(2418));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 16, 20, 10, 41, 66, DateTimeKind.Utc).AddTicks(2428));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 16, 20, 10, 41, 66, DateTimeKind.Utc).AddTicks(2430));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 16, 20, 10, 41, 649, DateTimeKind.Utc).AddTicks(9517));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 16, 20, 10, 41, 649, DateTimeKind.Utc).AddTicks(9520));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 16, 20, 10, 41, 649, DateTimeKind.Utc).AddTicks(9522));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 16, 20, 10, 41, 649, DateTimeKind.Utc).AddTicks(9524));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 16, 20, 10, 41, 649, DateTimeKind.Utc).AddTicks(9525));

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "EmailTemplateID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 16, 20, 10, 41, 649, DateTimeKind.Utc).AddTicks(9526));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 16, 20, 10, 41, 66, DateTimeKind.Utc).AddTicks(2468));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 16, 20, 10, 41, 66, DateTimeKind.Utc).AddTicks(2472));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 16, 20, 10, 41, 66, DateTimeKind.Utc).AddTicks(2476));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 16, 20, 10, 41, 179, DateTimeKind.Utc).AddTicks(2705), "$2a$11$AMv7ExCB/sRW/yb7kfQkyuppuIEcpatH6RW1julGyLekjvygMQssq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 16, 20, 10, 41, 297, DateTimeKind.Utc).AddTicks(5192), "$2a$11$S.OFWXLjS8pOC7OtevNu6.5pdi9hdpoym3kkXPiYAlPpnSl8iNAWK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 16, 20, 10, 41, 418, DateTimeKind.Utc).AddTicks(4282), "$2a$11$nlPcpHwAiHn8E1oc/B4oWecI6VQ8Pq/cYGzGQ.YYjQ2.IbrX2Ussq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 16, 20, 10, 41, 536, DateTimeKind.Utc).AddTicks(1885), "$2a$11$sQi6CF4wEE9Dt7fM/U.W6eZ8QZsewld497iA2xrLizA3Ciee0PlWW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 16, 20, 10, 41, 647, DateTimeKind.Utc).AddTicks(5900), "$2a$11$RDGHJinUYlCQxOBsk1L05eaTyyp1FDECar5yrm1gaBJqjsopEz6d." });
        }
    }
}
