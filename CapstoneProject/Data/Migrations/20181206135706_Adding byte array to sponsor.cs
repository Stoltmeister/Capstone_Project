using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CapstoneProject.Data.Migrations
{
    public partial class Addingbytearraytosponsor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Sponsors");

            migrationBuilder.AddColumn<byte[]>(
                name: "ContentImage",
                table: "Sponsors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentImage",
                table: "Sponsors");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Sponsors",
                nullable: true);
        }
    }
}
