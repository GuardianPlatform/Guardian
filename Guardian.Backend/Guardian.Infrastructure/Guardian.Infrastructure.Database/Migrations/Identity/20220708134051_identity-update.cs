using Microsoft.EntityFrameworkCore.Migrations;

namespace Guardian.Infrastructure.Database.Migrations.Identity
{
    public partial class identityupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_User_UserId1",
                schema: "Identity",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_Rating_UserId1",
                schema: "Identity",
                table: "Rating");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6afa4d22-a751-4dff-b017-2da6c8d3b71b", "2900ebf8-5ab4-4061-a551-d382af97c924" });

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "520fcf34-cb46-4bff-a5bc-7c15f463cea8", "b77c40ab-88d5-4b8e-a1b9-16fdeef163dc" });

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6afa4d22-a751-4dff-b017-2da6c8d3b71b", "b77c40ab-88d5-4b8e-a1b9-16fdeef163dc" });

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "71c7459d-551c-4652-bff6-f33fa75aec3d", "b77c40ab-88d5-4b8e-a1b9-16fdeef163dc" });

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "df6ede86-4b03-4deb-abe4-33a3576c2562", "b77c40ab-88d5-4b8e-a1b9-16fdeef163dc" });

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2900ebf8-5ab4-4061-a551-d382af97c924");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b77c40ab-88d5-4b8e-a1b9-16fdeef163dc");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Role",
                keyColumn: "Id",
                keyValue: "520fcf34-cb46-4bff-a5bc-7c15f463cea8");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Role",
                keyColumn: "Id",
                keyValue: "6afa4d22-a751-4dff-b017-2da6c8d3b71b");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Role",
                keyColumn: "Id",
                keyValue: "71c7459d-551c-4652-bff6-f33fa75aec3d");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Role",
                keyColumn: "Id",
                keyValue: "df6ede86-4b03-4deb-abe4-33a3576c2562");

            migrationBuilder.DropColumn(
                name: "UserId1",
                schema: "Identity",
                table: "Rating");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "Identity",
                table: "Rating",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "GameCategories",
                schema: "Identity",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCategories", x => new { x.GameId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_GameCategories_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "Identity",
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameCategories_Game_GameId",
                        column: x => x.GameId,
                        principalSchema: "Identity",
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameUsers",
                schema: "Identity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameUsers", x => new { x.GameId, x.UserId });
                    table.ForeignKey(
                        name: "FK_GameUsers_Game_GameId",
                        column: x => x.GameId,
                        principalSchema: "Identity",
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameUsers_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "7e2eae01-538f-4f49-ac6d-289a0823a2df", 0, "6ea79f53-20f0-4634-808f-f4e4b6072ba7", "superadmin@gmail.com", true, "Super", "Admin", false, null, "SUPERADMIN@GMAIL.COM", "SUPERADMIN", "AQAAAAEAACcQAAAAEBLjouNqaeiVWbN0TbXUS3+ChW3d7aQIk/BQEkWBxlrdRRngp14b0BIH0Rp65qD6mA==", null, true, "94f3250e-a117-4b07-9bc0-bccd6943018a", false, "superadmin" },
                    { "8c01e411-7de0-4aa5-bb5e-b52ba034f1fa", 0, "cabe84ff-8589-4b38-9ccc-a177f3bef1c4", "basicuser@gmail.com", true, "Basic", "User", false, null, "BASICUSER@GMAIL.COM", "BASICUSER", "AQAAAAEAACcQAAAAEBLjouNqaeiVWbN0TbXUS3+ChW3d7aQIk/BQEkWBxlrdRRngp14b0BIH0Rp65qD6mA==", null, true, "d5d52d50-835b-4b8a-a146-5bc04e2d8915", false, "basicuser" }
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "37d1c42d-89a7-45fc-90fe-2c4ce0b40cdb", "0f9d4330-70dd-463a-a197-af29d3ceef6e", "SuperAdmin", "SuperAdmin" },
                    { "d7c451dd-37be-4d67-97fa-00c07cb9f671", "45aa983b-ead8-4caf-9dda-0850a6335ca0", "Admin", "Admin" },
                    { "69c3ba4d-833c-42b0-b730-63473232e493", "4d468748-9f7d-457a-b043-8ff2b95a7c12", "Moderator", "Moderator" },
                    { "578aada2-1a48-442d-b6d7-acfb00c1c55b", "9c618957-a908-4a3e-86e8-d6631ed4e821", "Basic", "Basic" }
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "37d1c42d-89a7-45fc-90fe-2c4ce0b40cdb", "7e2eae01-538f-4f49-ac6d-289a0823a2df" },
                    { "d7c451dd-37be-4d67-97fa-00c07cb9f671", "7e2eae01-538f-4f49-ac6d-289a0823a2df" },
                    { "69c3ba4d-833c-42b0-b730-63473232e493", "7e2eae01-538f-4f49-ac6d-289a0823a2df" },
                    { "578aada2-1a48-442d-b6d7-acfb00c1c55b", "8c01e411-7de0-4aa5-bb5e-b52ba034f1fa" },
                    { "578aada2-1a48-442d-b6d7-acfb00c1c55b", "7e2eae01-538f-4f49-ac6d-289a0823a2df" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rating_UserId",
                schema: "Identity",
                table: "Rating",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameCategories_CategoryId",
                schema: "Identity",
                table: "GameCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GameUsers_UserId",
                schema: "Identity",
                table: "GameUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_User_UserId",
                schema: "Identity",
                table: "Rating",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_User_UserId",
                schema: "Identity",
                table: "Rating");

            migrationBuilder.DropTable(
                name: "GameCategories",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "GameUsers",
                schema: "Identity");

            migrationBuilder.DropIndex(
                name: "IX_Rating_UserId",
                schema: "Identity",
                table: "Rating");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "37d1c42d-89a7-45fc-90fe-2c4ce0b40cdb", "7e2eae01-538f-4f49-ac6d-289a0823a2df" });

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "578aada2-1a48-442d-b6d7-acfb00c1c55b", "7e2eae01-538f-4f49-ac6d-289a0823a2df" });

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "69c3ba4d-833c-42b0-b730-63473232e493", "7e2eae01-538f-4f49-ac6d-289a0823a2df" });

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d7c451dd-37be-4d67-97fa-00c07cb9f671", "7e2eae01-538f-4f49-ac6d-289a0823a2df" });

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "578aada2-1a48-442d-b6d7-acfb00c1c55b", "8c01e411-7de0-4aa5-bb5e-b52ba034f1fa" });

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7e2eae01-538f-4f49-ac6d-289a0823a2df");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8c01e411-7de0-4aa5-bb5e-b52ba034f1fa");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Role",
                keyColumn: "Id",
                keyValue: "37d1c42d-89a7-45fc-90fe-2c4ce0b40cdb");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Role",
                keyColumn: "Id",
                keyValue: "578aada2-1a48-442d-b6d7-acfb00c1c55b");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Role",
                keyColumn: "Id",
                keyValue: "69c3ba4d-833c-42b0-b730-63473232e493");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Role",
                keyColumn: "Id",
                keyValue: "d7c451dd-37be-4d67-97fa-00c07cb9f671");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                schema: "Identity",
                table: "Rating",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                schema: "Identity",
                table: "Rating",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "b77c40ab-88d5-4b8e-a1b9-16fdeef163dc", 0, "b6ea239b-07f9-438e-81d8-7ff7d1f4e051", "superadmin@gmail.com", true, "Super", "Admin", false, null, "SUPERADMIN@GMAIL.COM", "SUPERADMIN", "AQAAAAEAACcQAAAAEBLjouNqaeiVWbN0TbXUS3+ChW3d7aQIk/BQEkWBxlrdRRngp14b0BIH0Rp65qD6mA==", null, true, "5bf03be5-0dfa-4d09-ad56-097bd9c54095", false, "superadmin" },
                    { "2900ebf8-5ab4-4061-a551-d382af97c924", 0, "abaee515-89f7-4c1f-86e5-ff5ad31874d8", "basicuser@gmail.com", true, "Basic", "User", false, null, "BASICUSER@GMAIL.COM", "BASICUSER", "AQAAAAEAACcQAAAAEBLjouNqaeiVWbN0TbXUS3+ChW3d7aQIk/BQEkWBxlrdRRngp14b0BIH0Rp65qD6mA==", null, true, "e77a12db-b184-4a97-9e8e-5559aace08f1", false, "basicuser" }
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "71c7459d-551c-4652-bff6-f33fa75aec3d", "ad980fe0-0948-4220-82bf-26c96d22ecca", "SuperAdmin", "SuperAdmin" },
                    { "520fcf34-cb46-4bff-a5bc-7c15f463cea8", "acd19db0-de75-42ff-a0d6-9c1049c01a3f", "Admin", "Admin" },
                    { "df6ede86-4b03-4deb-abe4-33a3576c2562", "6977cfb1-c835-478f-925f-084e10761438", "Moderator", "Moderator" },
                    { "6afa4d22-a751-4dff-b017-2da6c8d3b71b", "7549d3c6-018b-44fc-b577-31b342a1c50d", "Basic", "Basic" }
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "71c7459d-551c-4652-bff6-f33fa75aec3d", "b77c40ab-88d5-4b8e-a1b9-16fdeef163dc" },
                    { "520fcf34-cb46-4bff-a5bc-7c15f463cea8", "b77c40ab-88d5-4b8e-a1b9-16fdeef163dc" },
                    { "df6ede86-4b03-4deb-abe4-33a3576c2562", "b77c40ab-88d5-4b8e-a1b9-16fdeef163dc" },
                    { "6afa4d22-a751-4dff-b017-2da6c8d3b71b", "2900ebf8-5ab4-4061-a551-d382af97c924" },
                    { "6afa4d22-a751-4dff-b017-2da6c8d3b71b", "b77c40ab-88d5-4b8e-a1b9-16fdeef163dc" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rating_UserId1",
                schema: "Identity",
                table: "Rating",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_User_UserId1",
                schema: "Identity",
                table: "Rating",
                column: "UserId1",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
