using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class new_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Education_Id",
                table: "Tb_T_Profiling");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Tb_M_Education");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Education_Id",
                table: "Tb_T_Profiling",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Tb_M_Education",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
