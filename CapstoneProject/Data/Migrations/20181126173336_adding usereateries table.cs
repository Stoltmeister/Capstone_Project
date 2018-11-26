using Microsoft.EntityFrameworkCore.Migrations;

namespace CapstoneProject.Data.Migrations
{
    public partial class addingusereateriestable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserEateries",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    StandardUserId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    ZipCode = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsVegan = table.Column<bool>(nullable: false),
                    IsVegetarian = table.Column<bool>(nullable: false),
                    HasVeganOptions = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEateries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserEateries_StandardUsers_StandardUserId",
                        column: x => x.StandardUserId,
                        principalTable: "StandardUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserEateries_StandardUserId",
                table: "UserEateries",
                column: "StandardUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserEateries");
        }
    }
}
