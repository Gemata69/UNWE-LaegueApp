using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UNWE_LaegueApp.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "22180023");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "22180023",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "22180023",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "log_22180023",
                schema: "22180023",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_log_22180023", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                schema: "22180023",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn_22180023 = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "22180023",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "22180023",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "22180023",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "22180023",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "22180023",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "22180023",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "22180023",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "22180023",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "22180023",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "22180023",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "22180023",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Leagues",
                schema: "22180023",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxTeams = table.Column<int>(type: "int", nullable: false),
                    SeasonOneId = table.Column<int>(type: "int", nullable: false),
                    SeasonTwoId = table.Column<int>(type: "int", nullable: false),
                    ModifiedOn_22180023 = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leagues_Seasons_SeasonOneId",
                        column: x => x.SeasonOneId,
                        principalSchema: "22180023",
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Leagues_Seasons_SeasonTwoId",
                        column: x => x.SeasonTwoId,
                        principalSchema: "22180023",
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                schema: "22180023",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamOneId = table.Column<int>(type: "int", nullable: false),
                    TeamTwoId = table.Column<int>(type: "int", nullable: false),
                    TeamOneScore = table.Column<int>(type: "int", nullable: false),
                    TeamTwoScore = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SeasonId = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn_22180023 = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalSchema: "22180023",
                        principalTable: "Seasons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Players",
                schema: "22180023",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Captain = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    ModifiedOn_22180023 = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                schema: "22180023",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaptainId = table.Column<int>(type: "int", nullable: true),
                    LeagueId = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn_22180023 = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalSchema: "22180023",
                        principalTable: "Leagues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Teams_Players_CaptainId",
                        column: x => x.CaptainId,
                        principalSchema: "22180023",
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "22180023",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "22180023",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "22180023",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "22180023",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "22180023",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "22180023",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "22180023",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Games_SeasonId",
                schema: "22180023",
                table: "Games",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_TeamOneId",
                schema: "22180023",
                table: "Games",
                column: "TeamOneId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_TeamTwoId",
                schema: "22180023",
                table: "Games",
                column: "TeamTwoId");

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_SeasonOneId",
                schema: "22180023",
                table: "Leagues",
                column: "SeasonOneId");

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_SeasonTwoId",
                schema: "22180023",
                table: "Leagues",
                column: "SeasonTwoId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                schema: "22180023",
                table: "Players",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CaptainId",
                schema: "22180023",
                table: "Teams",
                column: "CaptainId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_LeagueId",
                schema: "22180023",
                table: "Teams",
                column: "LeagueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Teams_TeamOneId",
                schema: "22180023",
                table: "Games",
                column: "TeamOneId",
                principalSchema: "22180023",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Teams_TeamTwoId",
                schema: "22180023",
                table: "Games",
                column: "TeamTwoId",
                principalSchema: "22180023",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Teams_TeamId",
                schema: "22180023",
                table: "Players",
                column: "TeamId",
                principalSchema: "22180023",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            // =====================================================================
            

            migrationBuilder.Sql(@"
                CREATE TRIGGER [22180023].[TRG_Teams_Audit] ON [22180023].[Teams] AFTER INSERT, UPDATE AS
                BEGIN
                    SET NOCOUNT ON;
                    DECLARE @Action VARCHAR(10);
                    IF EXISTS(SELECT * FROM deleted) SET @Action = 'UPDATE'; ELSE SET @Action = 'INSERT';
                    INSERT INTO [22180023].[log_22180023] (TableName, Action, Time) VALUES ('Teams', @Action, GETDATE());
                END
            ");

            migrationBuilder.Sql(@"
                CREATE TRIGGER [22180023].[TRG_Players_Audit] ON [22180023].[Players] AFTER INSERT, UPDATE AS
                BEGIN
                    SET NOCOUNT ON;
                    DECLARE @Action VARCHAR(10);
                    IF EXISTS(SELECT * FROM deleted) SET @Action = 'UPDATE'; ELSE SET @Action = 'INSERT';
                    INSERT INTO [22180023].[log_22180023] (TableName, Action, Time) VALUES ('Players', @Action, GETDATE());
                END
            ");

            migrationBuilder.Sql(@"
                CREATE TRIGGER [22180023].[TRG_Games_Audit] ON [22180023].[Games] AFTER INSERT, UPDATE AS
                BEGIN
                    SET NOCOUNT ON;
                    DECLARE @Action VARCHAR(10);
                    IF EXISTS(SELECT * FROM deleted) SET @Action = 'UPDATE'; ELSE SET @Action = 'INSERT';
                    INSERT INTO [22180023].[log_22180023] (TableName, Action, Time) VALUES ('Games', @Action, GETDATE());
                END
            ");

            migrationBuilder.Sql(@"
                CREATE TRIGGER [22180023].[TRG_Leagues_Audit] ON [22180023].[Leagues] AFTER INSERT, UPDATE AS
                BEGIN
                    SET NOCOUNT ON;
                    DECLARE @Action VARCHAR(10);
                    IF EXISTS(SELECT * FROM deleted) SET @Action = 'UPDATE'; ELSE SET @Action = 'INSERT';
                    INSERT INTO [22180023].[log_22180023] (TableName, Action, Time) VALUES ('Leagues', @Action, GETDATE());
                END
            ");

            migrationBuilder.Sql(@"
                CREATE TRIGGER [22180023].[TRG_Seasons_Audit] ON [22180023].[Seasons] AFTER INSERT, UPDATE AS
                BEGIN
                    SET NOCOUNT ON;
                    DECLARE @Action VARCHAR(10);
                    IF EXISTS(SELECT * FROM deleted) SET @Action = 'UPDATE'; ELSE SET @Action = 'INSERT';
                    INSERT INTO [22180023].[log_22180023] (TableName, Action, Time) VALUES ('Seasons', @Action, GETDATE());
                END
            ");

            // =====================================================================
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leagues_Seasons_SeasonOneId",
                schema: "22180023",
                table: "Leagues");

            migrationBuilder.DropForeignKey(
                name: "FK_Leagues_Seasons_SeasonTwoId",
                schema: "22180023",
                table: "Leagues");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Teams_TeamId",
                schema: "22180023",
                table: "Players");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "22180023");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "22180023");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "22180023");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "22180023");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "22180023");

            migrationBuilder.DropTable(
                name: "Games",
                schema: "22180023");

            migrationBuilder.DropTable(
                name: "log_22180023",
                schema: "22180023");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "22180023");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "22180023");

            migrationBuilder.DropTable(
                name: "Seasons",
                schema: "22180023");

            migrationBuilder.DropTable(
                name: "Teams",
                schema: "22180023");

            migrationBuilder.DropTable(
                name: "Leagues",
                schema: "22180023");

            migrationBuilder.DropTable(
                name: "Players",
                schema: "22180023");
        }
    }
}