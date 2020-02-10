using Microsoft.EntityFrameworkCore.Migrations;

namespace Example_Persistance.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Level = table.Column<int>(nullable: false),
                    Gold = table.Column<int>(nullable: false),
                    Gems = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                });

            migrationBuilder.CreateTable(
                name: "PlayerAwards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerId = table.Column<int>(nullable: false),
                    ForLevel = table.Column<int>(nullable: false),
                    Collected = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerAwards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerAwards_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Storages",
                columns: table => new
                {
                    StorageId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerId = table.Column<int>(nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    ItemsCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storages", x => x.StorageId);
                    table.ForeignKey(
                        name: "FK_Storages_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AwardArticles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerAwardId = table.Column<int>(nullable: true),
                    ArticleId = table.Column<int>(nullable: false),
                    ArticleAmount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwardArticles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AwardArticles_PlayerAwards_PlayerAwardId",
                        column: x => x.PlayerAwardId,
                        principalTable: "PlayerAwards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerArticles",
                columns: table => new
                {
                    PlayerArticleId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StorageId = table.Column<int>(nullable: true),
                    ItemId = table.Column<int>(nullable: false),
                    ArticleAmount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerArticles", x => x.PlayerArticleId);
                    table.ForeignKey(
                        name: "FK_PlayerArticles_Storages_StorageId",
                        column: x => x.StorageId,
                        principalTable: "Storages",
                        principalColumn: "StorageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AwardArticles_PlayerAwardId",
                table: "AwardArticles",
                column: "PlayerAwardId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerArticles_StorageId",
                table: "PlayerArticles",
                column: "StorageId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerAwards_PlayerId",
                table: "PlayerAwards",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Storages_PlayerId",
                table: "Storages",
                column: "PlayerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AwardArticles");

            migrationBuilder.DropTable(
                name: "PlayerArticles");

            migrationBuilder.DropTable(
                name: "PlayerAwards");

            migrationBuilder.DropTable(
                name: "Storages");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
