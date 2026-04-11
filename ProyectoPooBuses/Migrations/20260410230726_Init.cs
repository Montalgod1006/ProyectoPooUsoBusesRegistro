using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoPooBuses.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buses",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    route_number = table.Column<string>(type: "TEXT", nullable: false),
                    bus_model = table.Column<string>(type: "TEXT", nullable: true),
                    dni = table.Column<string>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    last_name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Dailies",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    total_passenger_day = table.Column<int>(type: "INTEGER", nullable: false),
                    rush_hour = table.Column<int>(type: "INTEGER", nullable: false),
                    off_peak_hour = table.Column<int>(type: "INTEGER", nullable: false),
                    most_used_route = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dailies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    bus_id = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Hour = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalPassengers = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.id);
                    table.ForeignKey(
                        name: "FK_Routes_Buses_bus_id",
                        column: x => x.bus_id,
                        principalTable: "Buses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Routes_bus_id",
                table: "Routes",
                column: "bus_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dailies");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Buses");
        }
    }
}
