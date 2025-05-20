using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fiap.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DiscountValue = table.Column<decimal>(type: "numeric", nullable: false),
                    DiscountCurrency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    PasswordSalt = table.Column<string>(type: "text", nullable: false),
                    TypeUser = table.Column<string>(type: "text", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Genre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    PriceCurrency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    PromotionId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Promotions_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LibraryGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PricePaid = table.Column<decimal>(type: "numeric", nullable: false),
                    PriceCurrency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibraryGames_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LibraryGames_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Genre", "Name", "PromotionId", "PriceCurrency", "Price" },
                values: new object[,]
                {
                    { 1, "Action RPG", "The Legend of Zelda: Breath of the Wild", null, "USD", 299.00m },
                    { 2, "Action RPG", "The Witcher 3: Wild Hunt", null, "BRL", 39.99m },
                    { 3, "Action-adventure", "Red Dead Redemption 2", null, "BRL", 49.99m },
                    { 4, "Action RPG", "Dark Souls III", null, "BRL", 29.99m },
                    { 5, "Action-adventure", "God of War", null, "BRL", 39.99m },
                    { 6, "Sandbox", "Minecraft", null, "BRL", 26.95m },
                    { 7, "First-person shooter", "Overwatch", null, "BRL", 39.99m },
                    { 8, "Action-adventure", "The Last of Us Part II", null, "BRL", 49.99m }
                });

            migrationBuilder.InsertData(
                table: "Promotions",
                columns: new[] { "Id", "EndDate", "StartDate", "DiscountCurrency", "DiscountValue" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), "USD", 10.15m },
                    { 2, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "USD", 15.98m },
                    { 3, new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "USD", 20.97m }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "Name", "TypeUser", "Email", "PasswordHash", "PasswordSalt" },
                values: new object[,]
                {
                    { 1, true, "Admin", "Admin", "admin@domain.com", "10000.zC3asvLZv6BQ2X3zcYdnhw==.d4+u5b59o6giayRcfePNPyF4I6nekoRp9bAMUEy3iHc=", "WQ1YZ/OiCeVcusBQ94njtQ==" },
                    { 2, true, "User", "User", "user@domain.com", "10000.SFG18S5pQd5QgxPtSxwPaw==.MPaE5q4K+T8EROdyuCeTz8SmJv+8q+h+yeN0d9pGUOg=", "J3CYQ56LlZ/D7NXAZBirlA==" }
                });

            migrationBuilder.InsertData(
                table: "LibraryGames",
                columns: new[] { "Id", "GameId", "PurchaseDate", "UserId", "PriceCurrency", "PricePaid" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "USD", 10.00m },
                    { 2, 2, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "USD", 15.00m },
                    { 3, 3, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "USD", 20.00m },
                    { 4, 4, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "USD", 28.99m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_Name",
                table: "Games",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_PromotionId",
                table: "Games",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryGames_GameId",
                table: "LibraryGames",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryGames_UserId",
                table: "LibraryGames",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LibraryGames");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Promotions");
        }
    }
}
