using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonChenil.Data.Migrations
{
    /// <inheritdoc />
    public partial class UsePetFromDomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PetEntityTimeSlot");

            migrationBuilder.DropColumn(
                name: "IncompatibleTypes",
                table: "Pets");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Pets",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "TimeSlotId",
                table: "Pets",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pets_TimeSlotId",
                table: "Pets",
                column: "TimeSlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_TimeSlots_TimeSlotId",
                table: "Pets",
                column: "TimeSlotId",
                principalTable: "TimeSlots",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_TimeSlots_TimeSlotId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_TimeSlotId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "TimeSlotId",
                table: "Pets");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Pets",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "IncompatibleTypes",
                table: "Pets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PetEntityTimeSlot",
                columns: table => new
                {
                    PetsId = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeSlotsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetEntityTimeSlot", x => new { x.PetsId, x.TimeSlotsId });
                    table.ForeignKey(
                        name: "FK_PetEntityTimeSlot_Pets_PetsId",
                        column: x => x.PetsId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PetEntityTimeSlot_TimeSlots_TimeSlotsId",
                        column: x => x.TimeSlotsId,
                        principalTable: "TimeSlots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PetEntityTimeSlot_TimeSlotsId",
                table: "PetEntityTimeSlot",
                column: "TimeSlotsId");
        }
    }
}
