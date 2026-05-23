using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UNWE_LaegueApp.Migrations
{
    /// <inheritdoc />
    public partial class AddJoinRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JoinRequests",
                schema: "22180023",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JoinRequests_Teams_TeamId",
                        column: x => x.TeamId,
                        principalSchema: "22180023",
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JoinRequests_TeamId",
                schema: "22180023",
                table: "JoinRequests",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JoinRequests",
                schema: "22180023");
        }
    }
}
