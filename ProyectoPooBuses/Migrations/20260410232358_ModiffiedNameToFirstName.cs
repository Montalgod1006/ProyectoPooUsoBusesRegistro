using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoPooBuses.Migrations
{
    /// <inheritdoc />
    public partial class ModiffiedNameToFirstName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Buses",
                newName: "first_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "Buses",
                newName: "name");
        }
    }
}
