using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHotel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addNotesToReservationEntitie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Reservations");
        }
    }
}
