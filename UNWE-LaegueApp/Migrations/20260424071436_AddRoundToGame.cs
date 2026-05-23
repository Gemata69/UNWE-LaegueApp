using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UNWE_LaegueApp.Migrations
{
    /// <inheritdoc />
    public partial class AddRoundToGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Round",
                schema: "22180023",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Round",
                schema: "22180023",
                table: "Games");
        }
    }
}
