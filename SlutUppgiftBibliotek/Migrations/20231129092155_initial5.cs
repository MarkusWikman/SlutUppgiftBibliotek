using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SlutUppgiftBibliotek.Migrations
{
    /// <inheritdoc />
    public partial class initial5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfLoan",
                table: "Borrowers");

            migrationBuilder.DropColumn(
                name: "DateOfReturn",
                table: "Borrowers");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfLoan",
                table: "Books",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfReturn",
                table: "Books",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfLoan",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "DateOfReturn",
                table: "Books");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfLoan",
                table: "Borrowers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfReturn",
                table: "Borrowers",
                type: "datetime2",
                nullable: true);
        }
    }
}
