using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoPooBuses.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnNameToGender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Buses",
                newName: "gender");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "gender",
                table: "Buses",
                newName: "Gender");
        }
    }
}
