using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UNWE_LaegueApp.Migrations
{
    /// <inheritdoc />
    public partial class AddUserProfileTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserProfiles",
                schema: "22180023",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserProfiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "22180023",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProfiles",
                schema: "22180023");
        }
    }
}
