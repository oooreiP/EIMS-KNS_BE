using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInvoiceItem_ProductRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    VATRate = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    CategoryType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsTaxable = table.Column<bool>(type: "boolean", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    TaxCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ContactPhone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    AccountNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    BankName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    TaxCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ContactEmail = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ContactPerson = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ContactPhone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceStatuses",
                columns: table => new
                {
                    InvoiceStatusID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceStatuses", x => x.InvoiceStatusID);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceTypes",
                columns: table => new
                {
                    InvoiceTypeID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Symbol = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    TypeName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceTypes", x => x.InvoiceTypeID);
                });

            migrationBuilder.CreateTable(
                name: "NotificationStatuses",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationStatuses", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTypes",
                columns: table => new
                {
                    TypeID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTypes", x => x.TypeID);
                });

            migrationBuilder.CreateTable(
                name: "Prefixes",
                columns: table => new
                {
                    PrefixID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrefixName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prefixes", x => x.PrefixID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "SerialStatuses",
                columns: table => new
                {
                    SerialStatusID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Symbol = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    StatusName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SerialStatuses", x => x.SerialStatusID);
                });

            migrationBuilder.CreateTable(
                name: "StatementStatuses",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatementStatuses", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "TaxApiStatuses",
                columns: table => new
                {
                    TaxApiStatusID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxApiStatuses", x => x.TaxApiStatusID);
                });

            migrationBuilder.CreateTable(
                name: "TemplateTypes",
                columns: table => new
                {
                    TemplateTypeID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TypeCategory = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateTypes", x => x.TemplateTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CategoryID = table.Column<int>(type: "integer", nullable: false),
                    Unit = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    BasePrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    VATRate = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    RoleID = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Serials",
                columns: table => new
                {
                    SerialID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrefixID = table.Column<int>(type: "integer", nullable: false),
                    InvoiceTypeID = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    Tail = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    SerialStatusID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Serials", x => x.SerialID);
                    table.ForeignKey(
                        name: "FK_Serials_InvoiceTypes_InvoiceTypeID",
                        column: x => x.InvoiceTypeID,
                        principalTable: "InvoiceTypes",
                        principalColumn: "InvoiceTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Serials_Prefixes_PrefixID",
                        column: x => x.PrefixID,
                        principalTable: "Prefixes",
                        principalColumn: "PrefixID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Serials_SerialStatuses_SerialStatusID",
                        column: x => x.SerialStatusID,
                        principalTable: "SerialStatuses",
                        principalColumn: "SerialStatusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    AuditID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<int>(type: "integer", nullable: true),
                    Action = table.Column<string>(type: "text", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Details = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.AuditID);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceStatements",
                columns: table => new
                {
                    StatementID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatementCode = table.Column<string>(type: "text", nullable: true),
                    StatementDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CustomerID = table.Column<int>(type: "integer", nullable: true),
                    TotalInvoices = table.Column<int>(type: "integer", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    StatusID = table.Column<int>(type: "integer", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    CreatorUserID = table.Column<int>(type: "integer", nullable: false),
                    StatementStatusStatusID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceStatements", x => x.StatementID);
                    table.ForeignKey(
                        name: "FK_InvoiceStatements_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK_InvoiceStatements_StatementStatuses_StatementStatusStatusID",
                        column: x => x.StatementStatusStatusID,
                        principalTable: "StatementStatuses",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceStatements_Users_CreatorUserID",
                        column: x => x.CreatorUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    NotificationStatusID = table.Column<int>(type: "integer", nullable: false),
                    NotificationTypeID = table.Column<int>(type: "integer", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationID);
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationStatuses_NotificationStatusID",
                        column: x => x.NotificationStatusID,
                        principalTable: "NotificationStatuses",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationTypes_NotificationTypeID",
                        column: x => x.NotificationTypeID,
                        principalTable: "NotificationTypes",
                        principalColumn: "TypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    Expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceTemplates",
                columns: table => new
                {
                    TemplateID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TemplateName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    InvoiceTypeID = table.Column<int>(type: "integer", nullable: false),
                    SerialID = table.Column<int>(type: "integer", nullable: false),
                    CurrentInvoiceNumber = table.Column<long>(type: "bigint", nullable: false),
                    LayoutDefinition = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedByUserID = table.Column<int>(type: "integer", nullable: false),
                    TemplateTypeID = table.Column<int>(type: "integer", nullable: false),
                    SerialStatusID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceTemplates", x => x.TemplateID);
                    table.ForeignKey(
                        name: "FK_InvoiceTemplates_Serials_SerialID",
                        column: x => x.SerialID,
                        principalTable: "Serials",
                        principalColumn: "SerialID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceTemplates_TemplateTypes_TemplateTypeID",
                        column: x => x.TemplateTypeID,
                        principalTable: "TemplateTypes",
                        principalColumn: "TemplateTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceTemplates_Users_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TemplateID = table.Column<int>(type: "integer", nullable: false),
                    InvoiceNumber = table.Column<long>(type: "bigint", nullable: false),
                    InvoiceStatusID = table.Column<int>(type: "integer", nullable: false),
                    CompanyId = table.Column<int>(type: "integer", nullable: false),
                    CustomerID = table.Column<int>(type: "integer", nullable: false),
                    IssuerID = table.Column<int>(type: "integer", nullable: false),
                    SignDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentDueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SubtotalAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    VATRate = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    VATAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TotalAmountInWords = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    DigitalSignature = table.Column<string>(type: "text", nullable: true),
                    TaxAuthorityCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    QRCodeData = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Notes = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    SalesID = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceID);
                    table.ForeignKey(
                        name: "FK_Invoices_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_InvoiceStatuses_InvoiceStatusID",
                        column: x => x.InvoiceStatusID,
                        principalTable: "InvoiceStatuses",
                        principalColumn: "InvoiceStatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_InvoiceTemplates_TemplateID",
                        column: x => x.TemplateID,
                        principalTable: "InvoiceTemplates",
                        principalColumn: "TemplateID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Users_IssuerID",
                        column: x => x.IssuerID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Users_SalesID",
                        column: x => x.SalesID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceHistories",
                columns: table => new
                {
                    HistoryID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvoiceID = table.Column<int>(type: "integer", nullable: false),
                    ActionType = table.Column<string>(type: "text", nullable: true),
                    ReferenceInvoiceID = table.Column<int>(type: "integer", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PerformedBy = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceHistories", x => x.HistoryID);
                    table.ForeignKey(
                        name: "FK_InvoiceHistories_Invoices_InvoiceID",
                        column: x => x.InvoiceID,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceHistories_Invoices_ReferenceInvoiceID",
                        column: x => x.ReferenceInvoiceID,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceID");
                    table.ForeignKey(
                        name: "FK_InvoiceHistories_Users_PerformedBy",
                        column: x => x.PerformedBy,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    InvoiceItemID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvoiceID = table.Column<int>(type: "integer", nullable: false),
                    ProductID = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    VATAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => x.InvoiceItemID);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoices_InvoiceID",
                        column: x => x.InvoiceID,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaxApiLogs",
                columns: table => new
                {
                    TaxLogID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvoiceID = table.Column<int>(type: "integer", nullable: false),
                    RequestPayload = table.Column<string>(type: "text", nullable: false),
                    ResponsePayload = table.Column<string>(type: "text", nullable: true),
                    TaxApiStatusID = table.Column<int>(type: "integer", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxApiLogs", x => x.TaxLogID);
                    table.ForeignKey(
                        name: "FK_TaxApiLogs_Invoices_InvoiceID",
                        column: x => x.InvoiceID,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaxApiLogs_TaxApiStatuses_TaxApiStatusID",
                        column: x => x.TaxApiStatusID,
                        principalTable: "TaxApiStatuses",
                        principalColumn: "TaxApiStatusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "CategoryType", "Code", "CreatedDate", "Description", "IsActive", "IsTaxable", "Name", "VATRate" },
                values: new object[,]
                {
                    { 1, "Goods", "HH", new DateTime(2025, 11, 5, 20, 27, 26, 551, DateTimeKind.Utc).AddTicks(9067), "Mặt hàng vật lý chịu thuế GTGT 10%", true, true, "Hàng hóa chịu thuế 10%", 10m },
                    { 2, "Service", "DV", new DateTime(2025, 11, 5, 20, 27, 26, 551, DateTimeKind.Utc).AddTicks(9072), "Dịch vụ lưu trữ, cho thuê máy chủ", true, true, "Dịch vụ chịu thuế 8%", 8m },
                    { 3, "Software", "SW", new DateTime(2025, 11, 5, 20, 27, 26, 551, DateTimeKind.Utc).AddTicks(9075), "Sản phẩm phần mềm và bản quyền", true, false, "Phần mềm không chịu thuế", 0m }
                });

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

            migrationBuilder.InsertData(
                table: "Prefixes",
                columns: new[] { "PrefixID", "PrefixName" },
                values: new object[,]
                {
                    { 1, "Hóa đơn điện tử giá trị gia tăng" },
                    { 2, "Hóa đơn điện tử bán hàng" },
                    { 3, "Hóa đơn điện tử bán tài sản công" },
                    { 4, "Hóa đơn điện tử bán hàng dự trữ quốc gia" },
                    { 5, "Hóa đơn điện tử khác là tem điện tử, vé điện tử, thẻ điện tử, phiếu thu điện tử hoặc các chứng từ điện tử có tên gọi khác nhưng có nội dung của hóa đơn điện tử theo quy định tại Nghị định số 123/2020/NĐ-CP" },
                    { 6, "Chứng từ điện tử được sử dụng và quản lý như hóa đơn gồm phiếu xuất kho kiêm vận chuyển nội bộ điện tử, phiếu xuất kho hàng gửi bán đại lý điện tử" },
                    { 7, "Hóa đơn thương mại điện tử" },
                    { 8, "Hóa đơn giá trị gia tăng tích hợp biên lai thu thuế, phí, lệ phí" },
                    { 9, "Hóa đơn bán hàng tích hợp biên lai thu thuế, phí, lệ phí" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "RoleName" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Accountant" },
                    { 3, "Sale" },
                    { 4, "HOD" },
                    { 5, "Customer" }
                });

            migrationBuilder.InsertData(
                table: "SerialStatuses",
                columns: new[] { "SerialStatusID", "StatusName", "Symbol" },
                values: new object[,]
                {
                    { 1, "Hóa đơn có mã của cơ quan thuế", "C" },
                    { 2, "Hóa đơn không có mã của cơ quan thuế", "K" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "BasePrice", "CategoryID", "Code", "CreatedDate", "Description", "IsActive", "Name", "Unit", "VATRate" },
                values: new object[,]
                {
                    { 1, 23000m, 1, "HH0001", new DateTime(2025, 11, 5, 20, 27, 26, 551, DateTimeKind.Utc).AddTicks(9125), "Xăng RON95 chịu thuế GTGT 10%", true, "Xăng RON95", "Lít", 10m },
                    { 2, 500000m, 2, "DV001", new DateTime(2025, 11, 5, 20, 27, 26, 551, DateTimeKind.Utc).AddTicks(9128), "Dịch vụ hosting thuế suất 8%", true, "Dịch vụ cho thuê máy chủ (Hosting)", "Tháng", 8m },
                    { 3, 10000000m, 3, "SW001", new DateTime(2025, 11, 5, 20, 27, 26, 551, DateTimeKind.Utc).AddTicks(9131), "Phần mềm không chịu thuế GTGT", true, "Phần mềm kế toán bản quyền", "Gói", 0m }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "PhoneNumber", "RoleID" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 5, 20, 27, 26, 667, DateTimeKind.Utc).AddTicks(766), "admin@eims.local", "Admin User", true, "$2a$11$VoYG6VEbaJlWBug9oPoz3utuak3QUFuVzxcUBQqO4OKhoHajzUwIC", "0101010101", 1 },
                    { 2, new DateTime(2025, 11, 5, 20, 27, 26, 783, DateTimeKind.Utc).AddTicks(2989), "accountant@eims.local", "Accountant User", true, "$2a$11$6sy0d7x.V4mgV.eztfK.1.St9LAPoe4IOB5SFxTo1eXkpfavdALPa", "0202020202", 2 },
                    { 3, new DateTime(2025, 11, 5, 20, 27, 26, 893, DateTimeKind.Utc).AddTicks(9218), "sale@eims.local", "Sales User", true, "$2a$11$l4NQY//c8ClpJtrsl2nq8uEEwQ/7XQE5kuH3dZA5EVCwE/lFNahCK", "0303030303", 3 },
                    { 4, new DateTime(2025, 11, 5, 20, 27, 27, 4, DateTimeKind.Utc).AddTicks(5107), "hod@eims.local", "Head Dept User", true, "$2a$11$uhEk8PSqqM3JnWPlWWWkG.HzfwXljRehFdjBfnnoxv5njPL.V4f6W", "0404040404", 4 },
                    { 5, new DateTime(2025, 11, 5, 20, 27, 27, 117, DateTimeKind.Utc).AddTicks(9357), "customer@eims.local", "Customer User", true, "$2a$11$p9ZpnIjHj1pVyPcAHyodeuFxX0ee7PRl7hal9XlJcvlLriGG9w/de", "0505050505", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserID",
                table: "AuditLogs",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceHistories_InvoiceID",
                table: "InvoiceHistories",
                column: "InvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceHistories_PerformedBy",
                table: "InvoiceHistories",
                column: "PerformedBy");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceHistories_ReferenceInvoiceID",
                table: "InvoiceHistories",
                column: "ReferenceInvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceID",
                table: "InvoiceItems",
                column: "InvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_ProductID",
                table: "InvoiceItems",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CompanyId",
                table: "Invoices",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerID",
                table: "Invoices",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InvoiceStatusID",
                table: "Invoices",
                column: "InvoiceStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_IssuerID",
                table: "Invoices",
                column: "IssuerID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_SalesID",
                table: "Invoices",
                column: "SalesID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_TemplateID",
                table: "Invoices",
                column: "TemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceStatements_CreatorUserID",
                table: "InvoiceStatements",
                column: "CreatorUserID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceStatements_CustomerID",
                table: "InvoiceStatements",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceStatements_StatementStatusStatusID",
                table: "InvoiceStatements",
                column: "StatementStatusStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceTemplates_CreatedByUserID",
                table: "InvoiceTemplates",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceTemplates_SerialID",
                table: "InvoiceTemplates",
                column: "SerialID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceTemplates_TemplateTypeID",
                table: "InvoiceTemplates",
                column: "TemplateTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationStatusID",
                table: "Notifications",
                column: "NotificationStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationTypeID",
                table: "Notifications",
                column: "NotificationTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserID",
                table: "Notifications",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Serials_InvoiceTypeID",
                table: "Serials",
                column: "InvoiceTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Serials_PrefixID",
                table: "Serials",
                column: "PrefixID");

            migrationBuilder.CreateIndex(
                name: "IX_Serials_SerialStatusID",
                table: "Serials",
                column: "SerialStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_TaxApiLogs_InvoiceID",
                table: "TaxApiLogs",
                column: "InvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_TaxApiLogs_TaxApiStatusID",
                table: "TaxApiLogs",
                column: "TaxApiStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "InvoiceHistories");

            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "InvoiceStatements");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "TaxApiLogs");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "StatementStatuses");

            migrationBuilder.DropTable(
                name: "NotificationStatuses");

            migrationBuilder.DropTable(
                name: "NotificationTypes");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "TaxApiStatuses");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "InvoiceStatuses");

            migrationBuilder.DropTable(
                name: "InvoiceTemplates");

            migrationBuilder.DropTable(
                name: "Serials");

            migrationBuilder.DropTable(
                name: "TemplateTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "InvoiceTypes");

            migrationBuilder.DropTable(
                name: "Prefixes");

            migrationBuilder.DropTable(
                name: "SerialStatuses");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
