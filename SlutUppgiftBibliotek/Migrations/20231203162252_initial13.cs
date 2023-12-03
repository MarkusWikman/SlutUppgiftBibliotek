﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SlutUppgiftBibliotek.Migrations
{
    /// <inheritdoc />
    public partial class initial13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfReturn",
                table: "Books",
                newName: "PlannedDateOfReturn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlannedDateOfReturn",
                table: "Books",
                newName: "DateOfReturn");
        }
    }
}
