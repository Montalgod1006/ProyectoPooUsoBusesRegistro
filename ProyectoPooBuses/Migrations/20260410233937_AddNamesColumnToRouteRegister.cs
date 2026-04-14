using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoPooBuses.Migrations
{
    /// <inheritdoc />
    public partial class AddNamesColumnToRouteRegister : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Hour",
                table: "Routes",
                newName: "hour");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Routes",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "TotalPassengers",
                table: "Routes",
                newName: "total_passenger");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "hour",
                table: "Routes",
                newName: "Hour");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "Routes",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "total_passenger",
                table: "Routes",
                newName: "TotalPassengers");
        }
    }
}
