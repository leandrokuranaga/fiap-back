using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class update_nullable_promotion_id_in_gamedomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Promotion_PromotionId",
                table: "Games");

            migrationBuilder.AlterColumn<int>(
                name: "PromotionId",
                table: "Games",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Promotion_PromotionId",
                table: "Games",
                column: "PromotionId",
                principalTable: "Promotion",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Promotion_PromotionId",
                table: "Games");

            migrationBuilder.AlterColumn<int>(
                name: "PromotionId",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Promotion_PromotionId",
                table: "Games",
                column: "PromotionId",
                principalTable: "Promotion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
