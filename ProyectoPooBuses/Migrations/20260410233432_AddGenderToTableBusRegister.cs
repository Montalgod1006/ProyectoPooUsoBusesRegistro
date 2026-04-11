using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoPooBuses.Migrations
{
    /// <inheritdoc />
    public partial class AddGenderToTableBusRegister : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Buses",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Buses");
        }
    }
}
