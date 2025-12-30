using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSystemLogAndUpdateAuditLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditLogs_Users_UserID",
                table: "AuditLogs");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "AuditLogs",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Details",
                table: "AuditLogs",
                newName: "TableName");

            migrationBuilder.RenameIndex(
                name: "IX_AuditLogs_UserID",
                table: "AuditLogs",
                newName: "IX_AuditLogs_UserId");

            migrationBuilder.AddColumn<string>(
                name: "NewValues",
                table: "AuditLogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldValues",
                table: "AuditLogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordId",
                table: "AuditLogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TraceId",
                table: "AuditLogs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SystemActivityLogs",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ActionName = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IpAddress = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TraceId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemActivityLogs", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_SystemActivityLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 17, 47, 44, 448, DateTimeKind.Utc).AddTicks(3755));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 17, 47, 44, 448, DateTimeKind.Utc).AddTicks(3761));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 17, 47, 44, 448, DateTimeKind.Utc).AddTicks(3763));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 17, 47, 44, 448, DateTimeKind.Utc).AddTicks(3801));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 17, 47, 44, 448, DateTimeKind.Utc).AddTicks(3804));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 17, 47, 44, 448, DateTimeKind.Utc).AddTicks(3807));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 29, 17, 47, 44, 559, DateTimeKind.Utc).AddTicks(9571), "$2a$11$Gl1jvSf9c4NdakqxHimBgeVnLy8HpTtG71TRw9OLxbFnDCEVaWmPK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 29, 17, 47, 44, 670, DateTimeKind.Utc).AddTicks(1389), "$2a$11$o0kkN/xigF/0FbAzYPgUL.jmgGX3Jgjbrr64vhfa5m6xMGvnj2lWi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 29, 17, 47, 44, 780, DateTimeKind.Utc).AddTicks(7715), "$2a$11$zk2o9FLuu1pUpBWa0btkpeCLT2KrZRqhkzEs1h0lDgCDoLXdKEKNC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 29, 17, 47, 44, 890, DateTimeKind.Utc).AddTicks(6885), "$2a$11$Q8N019jz0hci1MM.NuA23O4fhYN0OC0VB6W7fgEMYfAkQuba1b9OK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 29, 17, 47, 45, 0, DateTimeKind.Utc).AddTicks(9394), "$2a$11$ZC7yilNvGdUAsYum72ZmqOtSQRdMsmhwgZbZ1yq6xA1VVNBt2MTZe" });

            migrationBuilder.CreateIndex(
                name: "IX_SystemActivityLogs_UserId",
                table: "SystemActivityLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_Users_UserId",
                table: "AuditLogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditLogs_Users_UserId",
                table: "AuditLogs");

            migrationBuilder.DropTable(
                name: "SystemActivityLogs");

            migrationBuilder.DropColumn(
                name: "NewValues",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "OldValues",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "RecordId",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "TraceId",
                table: "AuditLogs");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AuditLogs",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "TableName",
                table: "AuditLogs",
                newName: "Details");

            migrationBuilder.RenameIndex(
                name: "IX_AuditLogs_UserId",
                table: "AuditLogs",
                newName: "IX_AuditLogs_UserID");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 25, 4, 40, 15, 267, DateTimeKind.Utc).AddTicks(1540));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 25, 4, 40, 15, 267, DateTimeKind.Utc).AddTicks(1567));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 25, 4, 40, 15, 267, DateTimeKind.Utc).AddTicks(1570));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 25, 4, 40, 15, 267, DateTimeKind.Utc).AddTicks(1680));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 25, 4, 40, 15, 267, DateTimeKind.Utc).AddTicks(1688));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 25, 4, 40, 15, 267, DateTimeKind.Utc).AddTicks(1693));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 25, 4, 40, 15, 394, DateTimeKind.Utc).AddTicks(2640), "$2a$11$V0U1wDJAGvIcudycoznw5OtS6/6Ukwna/XjzCTK1rA3YdzCZdIIVC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 25, 4, 40, 15, 508, DateTimeKind.Utc).AddTicks(8863), "$2a$11$1rSO3nIMoykDA8vSZJnGjekn8gpHPmhQ0y4e79.8wGBH3yAlFzcsi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 25, 4, 40, 15, 627, DateTimeKind.Utc).AddTicks(2295), "$2a$11$8UHNBzm9Fts0XSsvacqaDeKLrNq0LMA5WQVMgWOUplRFi.ZtIbBg6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 25, 4, 40, 15, 746, DateTimeKind.Utc).AddTicks(2277), "$2a$11$elllviozStyYlmbzG4XV5eDg8YBkMmv1dpvjJl9q/XLZ4og7vt4/q" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 25, 4, 40, 15, 865, DateTimeKind.Utc).AddTicks(7185), "$2a$11$hzXGQteggnNW8IZ47DZ3tuw8k//p7zFoEw3gqzv3Svg6TvXESsZqq" });

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_Users_UserID",
                table: "AuditLogs",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");
        }
    }
}
