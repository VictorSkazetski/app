using Microsoft.EntityFrameworkCore.Migrations;
using api.Data.Resources;
#nullable disable

namespace api.Infrastructure.Data.Migrations
{
    public partial class insertedRolesOnAspNetRolesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(SqlQueries.InsertRolesToAspNetRoles);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(SqlQueries.DeleteRolesFromAspNetRoles);
        }
    }
}
