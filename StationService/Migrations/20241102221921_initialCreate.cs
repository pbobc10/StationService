using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StationService.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GasStations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    SupervisorId = table.Column<int>(type: "INTEGER", nullable: false),
                    AdministratorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GasStations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GasStations_Administrators_AdministratorId",
                        column: x => x.AdministratorId,
                        principalTable: "Administrators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DispensingUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UnitNumber = table.Column<string>(type: "TEXT", nullable: false),
                    GasStationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DispensingUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DispensingUnits_GasStations_GasStationId",
                        column: x => x.GasStationId,
                        principalTable: "GasStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GasStationAttendants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Shift = table.Column<int>(type: "INTEGER", nullable: false),
                    GasStationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GasStationAttendants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GasStationAttendants_GasStations_GasStationId",
                        column: x => x.GasStationId,
                        principalTable: "GasStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Supervisors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    GasStationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supervisors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supervisors_GasStations_GasStationId",
                        column: x => x.GasStationId,
                        principalTable: "GasStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FuelPipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    DispensingUnitId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelPipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuelPipes_DispensingUnits_DispensingUnitId",
                        column: x => x.DispensingUnitId,
                        principalTable: "DispensingUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GasStationAttendantId = table.Column<int>(type: "INTEGER", nullable: false),
                    DispenserUnitId = table.Column<int>(type: "INTEGER", nullable: false),
                    DispensingUnitId = table.Column<int>(type: "INTEGER", nullable: false),
                    SupervisorId = table.Column<int>(type: "INTEGER", nullable: false),
                    ShiftType = table.Column<int>(type: "INTEGER", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignments_DispensingUnits_DispensingUnitId",
                        column: x => x.DispensingUnitId,
                        principalTable: "DispensingUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assignments_GasStationAttendants_GasStationAttendantId",
                        column: x => x.GasStationAttendantId,
                        principalTable: "GasStationAttendants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assignments_Supervisors_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Supervisors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GasMeters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MeterReading = table.Column<double>(type: "REAL", nullable: false),
                    FuelPipeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GasMeters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GasMeters_FuelPipes_FuelPipeId",
                        column: x => x.FuelPipeId,
                        principalTable: "FuelPipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FuelQuantities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FuelType = table.Column<int>(type: "INTEGER", nullable: false),
                    OpeningQuantity = table.Column<double>(type: "REAL", nullable: false),
                    ClosingQuantity = table.Column<double>(type: "REAL", nullable: false),
                    AssignmentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelQuantities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuelQuantities_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_DispensingUnitId",
                table: "Assignments",
                column: "DispensingUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_GasStationAttendantId",
                table: "Assignments",
                column: "GasStationAttendantId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_SupervisorId",
                table: "Assignments",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_DispensingUnits_GasStationId",
                table: "DispensingUnits",
                column: "GasStationId");

            migrationBuilder.CreateIndex(
                name: "IX_FuelPipes_DispensingUnitId",
                table: "FuelPipes",
                column: "DispensingUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_FuelQuantities_AssignmentId",
                table: "FuelQuantities",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_GasMeters_FuelPipeId",
                table: "GasMeters",
                column: "FuelPipeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GasStationAttendants_GasStationId",
                table: "GasStationAttendants",
                column: "GasStationId");

            migrationBuilder.CreateIndex(
                name: "IX_GasStations_AdministratorId",
                table: "GasStations",
                column: "AdministratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisors_GasStationId",
                table: "Supervisors",
                column: "GasStationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuelQuantities");

            migrationBuilder.DropTable(
                name: "GasMeters");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "FuelPipes");

            migrationBuilder.DropTable(
                name: "GasStationAttendants");

            migrationBuilder.DropTable(
                name: "Supervisors");

            migrationBuilder.DropTable(
                name: "DispensingUnits");

            migrationBuilder.DropTable(
                name: "GasStations");

            migrationBuilder.DropTable(
                name: "Administrators");
        }
    }
}
