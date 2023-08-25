using api.Data.Resources;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Infrastructure.Data.Migrations
{
    public partial class setRolesToUsersOnAspNetUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(SqlQueries.SetRolesToUsersOnAspNetUserRoles);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(SqlQueries.DeleteUsersRolesOnAspNetUserRoles);
        }
    }
}
