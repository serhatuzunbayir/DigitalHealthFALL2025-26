using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalHealthTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignedPrograms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssignedPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    TrainerId = table.Column<int>(type: "INTEGER", nullable: false),
                    WorkoutProgramId = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ApprovedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedPrograms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignedPrograms_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignedPrograms_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignedPrograms_WorkoutPrograms_WorkoutProgramId",
                        column: x => x.WorkoutProgramId,
                        principalTable: "WorkoutPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignedPrograms_TrainerId",
                table: "AssignedPrograms",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedPrograms_UserId",
                table: "AssignedPrograms",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedPrograms_WorkoutProgramId",
                table: "AssignedPrograms",
                column: "WorkoutProgramId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignedPrograms");
        }
    }
}
