using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Media_Site.Migrations
{
    public partial class MessageTest3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReciverId",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReciverId",
                table: "Messages",
                column: "ReciverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_ReciverId",
                table: "Messages",
                column: "ReciverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_ReciverId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ReciverId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ReciverId",
                table: "Messages");
        }
    }
}
