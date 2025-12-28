using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalHealthTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignedProgramIdToWorkoutLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedProgramId",
                table: "WorkoutLogs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutLogs_AssignedProgramId",
                table: "WorkoutLogs",
                column: "AssignedProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutLogs_AssignedPrograms_AssignedProgramId",
                table: "WorkoutLogs",
                column: "AssignedProgramId",
                principalTable: "AssignedPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutLogs_AssignedPrograms_AssignedProgramId",
                table: "WorkoutLogs");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutLogs_AssignedProgramId",
                table: "WorkoutLogs");

            migrationBuilder.DropColumn(
                name: "AssignedProgramId",
                table: "WorkoutLogs");
        }
    }
}
