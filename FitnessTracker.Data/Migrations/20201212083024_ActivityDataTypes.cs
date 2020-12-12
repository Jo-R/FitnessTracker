using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessTracker.Data.Migrations
{
    public partial class ActivityDataTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AveragePace",
                table: "RunActivity");

            migrationBuilder.DropColumn(
                name: "Calories",
                table: "RunActivity");

            migrationBuilder.RenameColumn(
                name: "Distance",
                table: "RunActivity",
                newName: "DistanceMile");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "RunActivity");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                  table: "RunActivity",
                nullable: false);

            migrationBuilder.AddColumn<double>(
                name: "AveragePaceMile",
                table: "RunActivity",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AveragePaceMile",
                table: "RunActivity");

            migrationBuilder.RenameColumn(
                name: "DistanceMile",
                table: "RunActivity",
                newName: "Distance");

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "RunActivity",
                type: "int",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AddColumn<int>(
                name: "AveragePace",
                table: "RunActivity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Calories",
                table: "RunActivity",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
