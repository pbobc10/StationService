using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StationService.Migrations
{
    /// <inheritdoc />
    public partial class FistName_FamilyName_Email_to_sup_adm_gasStAtt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Supervisors",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "GasStationAttendants",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Administrators",
                newName: "FirstName");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Supervisors",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FamilyName",
                table: "Supervisors",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "GasStationAttendants",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FamilyName",
                table: "GasStationAttendants",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FamilyName",
                table: "Administrators",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Supervisors");

            migrationBuilder.DropColumn(
                name: "FamilyName",
                table: "Supervisors");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "GasStationAttendants");

            migrationBuilder.DropColumn(
                name: "FamilyName",
                table: "GasStationAttendants");

            migrationBuilder.DropColumn(
                name: "FamilyName",
                table: "Administrators");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Supervisors",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "GasStationAttendants",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Administrators",
                newName: "Name");
        }
    }
}
