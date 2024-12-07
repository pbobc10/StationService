using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StationService.Migrations
{
    /// <inheritdoc />
    public partial class ListToICollection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GasMeters_FuelPipes_FuelPipeId",
                table: "GasMeters");

            migrationBuilder.DropForeignKey(
                name: "FK_Supervisors_GasStations_GasStationId",
                table: "Supervisors");

            migrationBuilder.AlterColumn<int>(
                name: "GasStationId",
                table: "Supervisors",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "FuelPipeId",
                table: "GasMeters",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_GasMeters_FuelPipes_FuelPipeId",
                table: "GasMeters",
                column: "FuelPipeId",
                principalTable: "FuelPipes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Supervisors_GasStations_GasStationId",
                table: "Supervisors",
                column: "GasStationId",
                principalTable: "GasStations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GasMeters_FuelPipes_FuelPipeId",
                table: "GasMeters");

            migrationBuilder.DropForeignKey(
                name: "FK_Supervisors_GasStations_GasStationId",
                table: "Supervisors");

            migrationBuilder.AlterColumn<int>(
                name: "GasStationId",
                table: "Supervisors",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FuelPipeId",
                table: "GasMeters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GasMeters_FuelPipes_FuelPipeId",
                table: "GasMeters",
                column: "FuelPipeId",
                principalTable: "FuelPipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Supervisors_GasStations_GasStationId",
                table: "Supervisors",
                column: "GasStationId",
                principalTable: "GasStations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
