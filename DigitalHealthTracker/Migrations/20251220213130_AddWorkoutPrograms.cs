using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalHealthTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkoutPrograms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutProgram_Trainers_TrainerId",
                table: "WorkoutProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutProgramItem_WorkoutProgram_WorkoutProgramId",
                table: "WorkoutProgramItem");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutProgramItem_Workouts_WorkoutId",
                table: "WorkoutProgramItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutProgramItem",
                table: "WorkoutProgramItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutProgram",
                table: "WorkoutProgram");

            migrationBuilder.RenameTable(
                name: "WorkoutProgramItem",
                newName: "WorkoutProgramItems");

            migrationBuilder.RenameTable(
                name: "WorkoutProgram",
                newName: "WorkoutPrograms");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutProgramItem_WorkoutProgramId",
                table: "WorkoutProgramItems",
                newName: "IX_WorkoutProgramItems_WorkoutProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutProgramItem_WorkoutId",
                table: "WorkoutProgramItems",
                newName: "IX_WorkoutProgramItems_WorkoutId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutProgram_TrainerId",
                table: "WorkoutPrograms",
                newName: "IX_WorkoutPrograms_TrainerId");

            migrationBuilder.AddColumn<int>(
                name: "TrainerId1",
                table: "WorkoutPrograms",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutProgramItems",
                table: "WorkoutProgramItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutPrograms",
                table: "WorkoutPrograms",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPrograms_TrainerId1",
                table: "WorkoutPrograms",
                column: "TrainerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutProgramItems_WorkoutPrograms_WorkoutProgramId",
                table: "WorkoutProgramItems",
                column: "WorkoutProgramId",
                principalTable: "WorkoutPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutProgramItems_Workouts_WorkoutId",
                table: "WorkoutProgramItems",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPrograms_Trainers_TrainerId",
                table: "WorkoutPrograms",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPrograms_Trainers_TrainerId1",
                table: "WorkoutPrograms",
                column: "TrainerId1",
                principalTable: "Trainers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutProgramItems_WorkoutPrograms_WorkoutProgramId",
                table: "WorkoutProgramItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutProgramItems_Workouts_WorkoutId",
                table: "WorkoutProgramItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPrograms_Trainers_TrainerId",
                table: "WorkoutPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPrograms_Trainers_TrainerId1",
                table: "WorkoutPrograms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutPrograms",
                table: "WorkoutPrograms");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutPrograms_TrainerId1",
                table: "WorkoutPrograms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutProgramItems",
                table: "WorkoutProgramItems");

            migrationBuilder.DropColumn(
                name: "TrainerId1",
                table: "WorkoutPrograms");

            migrationBuilder.RenameTable(
                name: "WorkoutPrograms",
                newName: "WorkoutProgram");

            migrationBuilder.RenameTable(
                name: "WorkoutProgramItems",
                newName: "WorkoutProgramItem");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutPrograms_TrainerId",
                table: "WorkoutProgram",
                newName: "IX_WorkoutProgram_TrainerId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutProgramItems_WorkoutProgramId",
                table: "WorkoutProgramItem",
                newName: "IX_WorkoutProgramItem_WorkoutProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutProgramItems_WorkoutId",
                table: "WorkoutProgramItem",
                newName: "IX_WorkoutProgramItem_WorkoutId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutProgram",
                table: "WorkoutProgram",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutProgramItem",
                table: "WorkoutProgramItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutProgram_Trainers_TrainerId",
                table: "WorkoutProgram",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutProgramItem_WorkoutProgram_WorkoutProgramId",
                table: "WorkoutProgramItem",
                column: "WorkoutProgramId",
                principalTable: "WorkoutProgram",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutProgramItem_Workouts_WorkoutId",
                table: "WorkoutProgramItem",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
