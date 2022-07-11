using Microsoft.EntityFrameworkCore.Migrations;

namespace Guardian.Infrastructure.Database.Migrations
{
    public partial class addgameseeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Author", "Description", "ImageUrl", "License", "Name" },
                values: new object[,]
                {
                    { 1, "EA Games", "World War II shooter game.", "https://image.ceneostatic.pl/data/products/10116336/i-medal-of-honor-digital.jpg", "-", "Medal of Honor" },
                    { 2, "Ea Games", "Racing game with super fast cars.", "https://store-images.s-microsoft.com/image/apps.50422.14208985329983396.5216b3ae-22f3-400f-ad5d-45a1eb1686ba.6d0f6755-43ce-4902-8fe6-3939b2e29e4d?q=90&w=480&h=270", "-", "Need for Speed" },
                    { 3, "EA Sports", "Football game.", "https://s2.tvp.pl/images2/2/7/8/uid_2787b3f1460aaecdbefc4818c064be541652200255438_width_1280_play_0_pos_0_gs_0_height_720_fifa-22-jest-aktualnie-najnowsza-wersja-gry-fot-ea-sports.jpg", "-", "FIFA" },
                    { 4, "CD Projekt", "Farm simulator.", "https://smartcdkeys.com/image/data/products/farming-simulator-22/cover/farming-simulator-22-smartcdkeys-cheap-cd-key-cover.jpg", "-", "Farming Simulator" }
                });

            migrationBuilder.InsertData(
                table: "GameCategories",
                columns: new[] { "CategoryId", "GameId" },
                values: new object[,]
                {
                    { 5, 1 },
                    { 4, 2 },
                    { 2, 3 },
                    { 3, 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GameCategories",
                keyColumns: new[] { "CategoryId", "GameId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "GameCategories",
                keyColumns: new[] { "CategoryId", "GameId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "GameCategories",
                keyColumns: new[] { "CategoryId", "GameId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "GameCategories",
                keyColumns: new[] { "CategoryId", "GameId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
