using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UNWE_LaegueApp.Migrations
{
    /// <inheritdoc />
    public partial class FinalFixJoinRequestUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "22180023",
                table: "JoinRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "22180023",
                table: "JoinRequests");
        }
    }
}
