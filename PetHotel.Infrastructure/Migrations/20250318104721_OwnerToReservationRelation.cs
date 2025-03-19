using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHotel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OwnerToReservationRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_OwnerId",
                table: "Reservations",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Owners_OwnerId",
                table: "Reservations",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Owners_OwnerId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_OwnerId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Reservations");
        }
    }
}
