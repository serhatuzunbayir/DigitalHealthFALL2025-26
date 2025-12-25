using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalHealthTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixTrainerWorkoutProgramRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPrograms_Trainers_TrainerId1",
                table: "WorkoutPrograms");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutPrograms_TrainerId1",
                table: "WorkoutPrograms");

            migrationBuilder.DropColumn(
                name: "TrainerId1",
                table: "WorkoutPrograms");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrainerId1",
                table: "WorkoutPrograms",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPrograms_TrainerId1",
                table: "WorkoutPrograms",
                column: "TrainerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPrograms_Trainers_TrainerId1",
                table: "WorkoutPrograms",
                column: "TrainerId1",
                principalTable: "Trainers",
                principalColumn: "Id");
        }
    }
}
