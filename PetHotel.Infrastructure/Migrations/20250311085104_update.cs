using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHotel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationServices_Reservations_ServiceId",
                table: "ReservationServices");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationServices_ReservationId",
                table: "ReservationServices",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationServices_Reservations_ReservationId",
                table: "ReservationServices",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "ReservationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationServices_Reservations_ReservationId",
                table: "ReservationServices");

            migrationBuilder.DropIndex(
                name: "IX_ReservationServices_ReservationId",
                table: "ReservationServices");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationServices_Reservations_ServiceId",
                table: "ReservationServices",
                column: "ServiceId",
                principalTable: "Reservations",
                principalColumn: "ReservationId");
        }
    }
}
