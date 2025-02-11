using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api1.Migrations
{
    /// <inheritdoc />
    public partial class AgregarColumnaActivo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Activo",
                table: "Clientes",
                nullable: false,
                defaultValue: 1); 
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Clientes");
        }
    }
}
