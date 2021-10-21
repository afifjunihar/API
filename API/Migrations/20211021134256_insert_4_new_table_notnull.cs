using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class insert_4_new_table_notnull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_T_Education_Tb_M_University_UniversityId1",
                table: "TB_T_Education");

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId1",
                table: "TB_T_Education",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_T_Education_Tb_M_University_UniversityId1",
                table: "TB_T_Education",
                column: "UniversityId1",
                principalTable: "Tb_M_University",
                principalColumn: "UniversityId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_T_Education_Tb_M_University_UniversityId1",
                table: "TB_T_Education");

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId1",
                table: "TB_T_Education",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_T_Education_Tb_M_University_UniversityId1",
                table: "TB_T_Education",
                column: "UniversityId1",
                principalTable: "Tb_M_University",
                principalColumn: "UniversityId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
