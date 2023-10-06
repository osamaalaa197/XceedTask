using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product__Application.Migrations
{
    public partial class useradmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO `AspNetUsers` (`Id`, `UserName`, `NormalizedUserName`, `Email`, `NormalizedEmail`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `ConcurrencyStamp`, `PhoneNumber`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEnd`, `LockoutEnabled`, `AccessFailedCount`)
            VALUES ('33597162-1b6e-41af-ae65-0bcd74d32a69', 'admin', 'ADMIN', 'admin@gmail.com', 'ADMIN@GMAIL.COM', 0, 'AQAAAAIAAYagAAAAEMmK08nkjWNQQMoAfTroTzBcl45VUFw3u9BarXkDyTKgQRREKYl6YzWka7jwvxEH+Q==', '3ORG5FXBJ7UV64HYN45DFNEABXZM63HX', '7bb43b12-3e11-45d2-9ee3-d30c8f521455', '01121316417', 0, 0, NULL, 1, 0);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM `AspNetUsers` WHERE Id = '33597162-1b6e-41af-ae65-0bcd74d32a69';");
        }
    }
}
