using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VibeHome.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Workout");

            migrationBuilder.EnsureSchema(
                name: "Kids");

            migrationBuilder.CreateTable(
                name: "CardioSessions",
                schema: "Workout",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    Distance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CaloriesBurned = table.Column<int>(type: "int", nullable: false),
                    CardioTypeId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardioSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardioTypes",
                schema: "Workout",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardioTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChoreItems",
                schema: "Kids",
                columns: table => new
                {
                    ChoreItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChoreName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoreItems", x => x.ChoreItemId);
                });

            migrationBuilder.CreateTable(
                name: "Kids",
                schema: "Kids",
                columns: table => new
                {
                    KidId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kids", x => x.KidId);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                schema: "Kids",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                schema: "Workout",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Kids",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "WeightTrainingSessions",
                schema: "Workout",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reps = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WorkoutTypeId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    Sets = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightTrainingSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutTypes",
                schema: "Workout",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeeklyPaymentStatuses",
                schema: "Kids",
                columns: table => new
                {
                    WeeklyPaymentStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KidId = table.Column<int>(type: "int", nullable: false),
                    WeekStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklyPaymentStatuses", x => x.WeeklyPaymentStatusId);
                    table.ForeignKey(
                        name: "FK_WeeklyPaymentStatuses_Kids_KidId",
                        column: x => x.KidId,
                        principalSchema: "Kids",
                        principalTable: "Kids",
                        principalColumn: "KidId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChoreCompletions",
                schema: "Kids",
                columns: table => new
                {
                    ChoreCompletionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KidId = table.Column<int>(type: "int", nullable: false),
                    ChoreItemId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    CompletionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoreCompletions", x => x.ChoreCompletionId);
                    table.ForeignKey(
                        name: "FK_ChoreCompletions_ChoreItems_ChoreItemId",
                        column: x => x.ChoreItemId,
                        principalSchema: "Kids",
                        principalTable: "ChoreItems",
                        principalColumn: "ChoreItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChoreCompletions_Kids_KidId",
                        column: x => x.KidId,
                        principalSchema: "Kids",
                        principalTable: "Kids",
                        principalColumn: "KidId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChoreCompletions_Locations_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "Kids",
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChoreCompletions_ChoreItemId",
                schema: "Kids",
                table: "ChoreCompletions",
                column: "ChoreItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoreCompletions_KidId",
                schema: "Kids",
                table: "ChoreCompletions",
                column: "KidId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoreCompletions_LocationId",
                schema: "Kids",
                table: "ChoreCompletions",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyPaymentStatuses_KidId",
                schema: "Kids",
                table: "WeeklyPaymentStatuses",
                column: "KidId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardioSessions",
                schema: "Workout");

            migrationBuilder.DropTable(
                name: "CardioTypes",
                schema: "Workout");

            migrationBuilder.DropTable(
                name: "ChoreCompletions",
                schema: "Kids");

            migrationBuilder.DropTable(
                name: "Locations",
                schema: "Workout");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Kids");

            migrationBuilder.DropTable(
                name: "WeeklyPaymentStatuses",
                schema: "Kids");

            migrationBuilder.DropTable(
                name: "WeightTrainingSessions",
                schema: "Workout");

            migrationBuilder.DropTable(
                name: "WorkoutTypes",
                schema: "Workout");

            migrationBuilder.DropTable(
                name: "ChoreItems",
                schema: "Kids");

            migrationBuilder.DropTable(
                name: "Locations",
                schema: "Kids");

            migrationBuilder.DropTable(
                name: "Kids",
                schema: "Kids");
        }
    }
}
