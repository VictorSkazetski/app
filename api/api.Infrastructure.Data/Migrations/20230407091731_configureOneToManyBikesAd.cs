using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Infrastructure.Data.Migrations
{
    public partial class configureOneToManyBikesAd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "BikeAd",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BikeAd_UserProfileId",
                table: "BikeAd",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_BikeAd_UserProfile_UserProfileId",
                table: "BikeAd",
                column: "UserProfileId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BikeAd_UserProfile_UserProfileId",
                table: "BikeAd");

            migrationBuilder.DropIndex(
                name: "IX_BikeAd_UserProfileId",
                table: "BikeAd");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "BikeAd");
        }
    }
}
