using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonChenil.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveReservationsFromPet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetReservation_Reservations_ReservationsId",
                table: "PetReservation");

            migrationBuilder.RenameColumn(
                name: "ReservationsId",
                table: "PetReservation",
                newName: "ReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_PetReservation_ReservationsId",
                table: "PetReservation",
                newName: "IX_PetReservation_ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PetReservation_Reservations_ReservationId",
                table: "PetReservation",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetReservation_Reservations_ReservationId",
                table: "PetReservation");

            migrationBuilder.RenameColumn(
                name: "ReservationId",
                table: "PetReservation",
                newName: "ReservationsId");

            migrationBuilder.RenameIndex(
                name: "IX_PetReservation_ReservationId",
                table: "PetReservation",
                newName: "IX_PetReservation_ReservationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_PetReservation_Reservations_ReservationsId",
                table: "PetReservation",
                column: "ReservationsId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
