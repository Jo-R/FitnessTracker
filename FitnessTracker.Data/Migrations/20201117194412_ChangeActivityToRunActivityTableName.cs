using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessTracker.Data.Migrations
{
    public partial class ChangeActivityToRunActivityTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_User_UserId",
                table: "Activity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Activity",
                table: "Activity");

            migrationBuilder.RenameTable(
                name: "Activity",
                newName: "RunActivity");

            migrationBuilder.RenameIndex(
                name: "IX_Activity_UserId",
                table: "RunActivity",
                newName: "IX_RunActivity_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RunActivity",
                table: "RunActivity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RunActivity_User_UserId",
                table: "RunActivity",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RunActivity_User_UserId",
                table: "RunActivity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RunActivity",
                table: "RunActivity");

            migrationBuilder.RenameTable(
                name: "RunActivity",
                newName: "Activity");

            migrationBuilder.RenameIndex(
                name: "IX_RunActivity_UserId",
                table: "Activity",
                newName: "IX_Activity_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Activity",
                table: "Activity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_User_UserId",
                table: "Activity",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
