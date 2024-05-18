using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonChenil.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIncompatibleTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IncompatibleTypes",
                table: "Pets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            // Update all existing pets to have an empty list of incompatible types
            migrationBuilder.Sql("UPDATE Pets SET IncompatibleTypes = '[]'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncompatibleTypes",
                table: "Pets");
        }
    }
}
