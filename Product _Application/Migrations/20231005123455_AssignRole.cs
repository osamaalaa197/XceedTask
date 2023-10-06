using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product__Application.Migrations
{
    /// <inheritdoc />
    public partial class AssignRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Aspnetuserroles(UserId,RoleId) select '33597162-1b6e-41af-ae65-0bcd74d32a69',Id from productdatabase.aspnetroles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Aspnetuserroles WHERE UserId='33597162-1b6e-41af-ae65-0bcd74d32a69'");
        }
    }
}
