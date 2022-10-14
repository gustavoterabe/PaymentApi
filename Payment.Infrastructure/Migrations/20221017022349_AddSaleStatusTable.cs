using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Payment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSaleStatusTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Sales",
                newName: "StatusId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Sales",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 16, 23, 23, 49, 135, DateTimeKind.Local).AddTicks(3018),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 16, 22, 18, 51, 205, DateTimeKind.Local).AddTicks(430));

            migrationBuilder.CreateTable(
                name: "SalesStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesStatus", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "SalesStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Waiting for payment" },
                    { 1, "Payment approved" },
                    { 2, "Sent to shipping company" },
                    { 3, "Delivered" },
                    { 4, "Canceled" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_StatusId",
                table: "Sales",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_SalesStatus_StatusId",
                table: "Sales",
                column: "StatusId",
                principalTable: "SalesStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_SalesStatus_StatusId",
                table: "Sales");

            migrationBuilder.DropTable(
                name: "SalesStatus");

            migrationBuilder.DropIndex(
                name: "IX_Sales_StatusId",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Sales",
                newName: "Status");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Sales",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 16, 22, 18, 51, 205, DateTimeKind.Local).AddTicks(430),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 16, 23, 23, 49, 135, DateTimeKind.Local).AddTicks(3018));
        }
    }
}
