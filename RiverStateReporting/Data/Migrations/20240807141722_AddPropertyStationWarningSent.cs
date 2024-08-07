using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiverStateReporting.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyStationWarningSent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "WarningSent",
                table: "Stations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WarningSent",
                table: "Stations");
        }
    }
}
