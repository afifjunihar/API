using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class updateRoleNameV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountRoleId",
                table: "Tb_T_Employee");

            migrationBuilder.DropColumn(
                name: "NIK",
                table: "TB_T_Education");

            migrationBuilder.DropColumn(
                name: "EducationId",
                table: "Tb_M_University");

            migrationBuilder.DropColumn(
                name: "AccountRoleId",
                table: "Roles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountRoleId",
                table: "Tb_T_Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NIK",
                table: "TB_T_Education",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EducationId",
                table: "Tb_M_University",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccountRoleId",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
