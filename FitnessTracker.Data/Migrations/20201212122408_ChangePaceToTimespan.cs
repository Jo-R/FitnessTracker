using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessTracker.Data.Migrations
{
    public partial class ChangePaceToTimespan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
               name: "AveragePaceMile",
               table: "RunActivity"
               );

            migrationBuilder.AddColumn<TimeSpan>(
               name: "AveragePaceMile",
               table: "RunActivity",
               type: "time",
               nullable: false
              );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "AveragePaceMile",
                table: "RunActivity",
                type: "float",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");
        }
    }
}
