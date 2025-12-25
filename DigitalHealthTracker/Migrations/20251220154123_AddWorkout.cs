using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalHealthTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPrograms_Trainers_TrainerId",
                table: "WorkoutPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPrograms_Users_UserId",
                table: "WorkoutPrograms");

			/* migrationBuilder.DropTable(
				 name: "WorkoutLogs");*/
			migrationBuilder.Sql(@"DROP TABLE IF EXISTS ""WorkoutLogs"";");

			migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutPrograms",
                table: "WorkoutPrograms");

            /*migrationBuilder.DropIndex(
                name: "IX_WorkoutPrograms_UserId",
                table: "WorkoutPrograms");*/
			migrationBuilder.Sql(@"DROP INDEX IF EXISTS ""IX_WorkoutPrograms_UserId"";");

			migrationBuilder.DropColumn(
                name: "Bmi",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TargetWeightDiffKg",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "WorkoutPrograms");

            migrationBuilder.DropColumn(
                name: "IsApprovedByUser",
                table: "WorkoutPrograms");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "WorkoutPrograms");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WorkoutPrograms");

            migrationBuilder.RenameTable(
                name: "WorkoutPrograms",
                newName: "WorkoutProgram");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Users",
                newName: "BirthYear");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "WorkoutProgram",
                newName: "Title");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutPrograms_TrainerId",
                table: "WorkoutProgram",
                newName: "IX_WorkoutProgram_TrainerId");

            migrationBuilder.AddColumn<int>(
                name: "BirthYear",
                table: "Trainers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutProgram",
                table: "WorkoutProgram",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutProgramItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WorkoutProgramId = table.Column<int>(type: "INTEGER", nullable: false),
                    WorkoutId = table.Column<int>(type: "INTEGER", nullable: false),
                    DayNo = table.Column<int>(type: "INTEGER", nullable: false),
                    Sets = table.Column<int>(type: "INTEGER", nullable: false),
                    Reps = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutProgramItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutProgramItem_WorkoutProgram_WorkoutProgramId",
                        column: x => x.WorkoutProgramId,
                        principalTable: "WorkoutProgram",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutProgramItem_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutProgramItem_WorkoutId",
                table: "WorkoutProgramItem",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutProgramItem_WorkoutProgramId",
                table: "WorkoutProgramItem",
                column: "WorkoutProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutProgram_Trainers_TrainerId",
                table: "WorkoutProgram",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutProgram_Trainers_TrainerId",
                table: "WorkoutProgram");

            migrationBuilder.DropTable(
                name: "WorkoutProgramItem");

            migrationBuilder.DropTable(
                name: "Workouts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutProgram",
                table: "WorkoutProgram");

            migrationBuilder.DropColumn(
                name: "BirthYear",
                table: "Trainers");

            migrationBuilder.RenameTable(
                name: "WorkoutProgram",
                newName: "WorkoutPrograms");

            migrationBuilder.RenameColumn(
                name: "BirthYear",
                table: "Users",
                newName: "Age");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "WorkoutPrograms",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutProgram_TrainerId",
                table: "WorkoutPrograms",
                newName: "IX_WorkoutPrograms_TrainerId");

            migrationBuilder.AddColumn<double>(
                name: "Bmi",
                table: "Users",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TargetWeightDiffKg",
                table: "Users",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "WorkoutPrograms",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsApprovedByUser",
                table: "WorkoutPrograms",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "WorkoutPrograms",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "WorkoutPrograms",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutPrograms",
                table: "WorkoutPrograms",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "WorkoutLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExerciseName = table.Column<string>(type: "TEXT", nullable: false),
                    IsExtraExercise = table.Column<bool>(type: "INTEGER", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPrograms_UserId",
                table: "WorkoutPrograms",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutLogs_UserId",
                table: "WorkoutLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPrograms_Trainers_TrainerId",
                table: "WorkoutPrograms",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPrograms_Users_UserId",
                table: "WorkoutPrograms",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
