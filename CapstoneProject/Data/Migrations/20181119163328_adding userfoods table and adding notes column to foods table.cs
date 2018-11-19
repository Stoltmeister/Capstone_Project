using Microsoft.EntityFrameworkCore.Migrations;

namespace CapstoneProject.Data.Migrations
{
    public partial class addinguserfoodstableandaddingnotescolumntofoodstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Food",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserFoods",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    StandardUserId = table.Column<string>(nullable: true),
                    FoodId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFoods_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserFoods_StandardUsers_StandardUserId",
                        column: x => x.StandardUserId,
                        principalTable: "StandardUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFoods_FoodId",
                table: "UserFoods",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFoods_StandardUserId",
                table: "UserFoods",
                column: "StandardUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFoods");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Food");
        }
    }
}
