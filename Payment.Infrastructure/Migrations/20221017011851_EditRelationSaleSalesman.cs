using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditRelationSaleSalesman : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sales_SalesmanId",
                table: "Sales");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Sales",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 16, 22, 18, 51, 205, DateTimeKind.Local).AddTicks(430),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 15, 16, 3, 39, 106, DateTimeKind.Local).AddTicks(3624));

            migrationBuilder.CreateIndex(
                name: "IX_Sales_SalesmanId",
                table: "Sales",
                column: "SalesmanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sales_SalesmanId",
                table: "Sales");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Sales",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 15, 16, 3, 39, 106, DateTimeKind.Local).AddTicks(3624),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 16, 22, 18, 51, 205, DateTimeKind.Local).AddTicks(430));

            migrationBuilder.CreateIndex(
                name: "IX_Sales_SalesmanId",
                table: "Sales",
                column: "SalesmanId",
                unique: true);
        }
    }
}
