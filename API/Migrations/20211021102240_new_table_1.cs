using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class new_table_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tb_M_Education_Tb_M_University_UniversityId",
                table: "Tb_M_Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Tb_T_Profiling_Tb_M_Education_EducationId",
                table: "Tb_T_Profiling");

            migrationBuilder.AlterColumn<int>(
                name: "EducationId",
                table: "Tb_T_Profiling",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "Tb_M_Education",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_M_Education_Tb_M_University_UniversityId",
                table: "Tb_M_Education",
                column: "UniversityId",
                principalTable: "Tb_M_University",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_T_Profiling_Tb_M_Education_EducationId",
                table: "Tb_T_Profiling",
                column: "EducationId",
                principalTable: "Tb_M_Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tb_M_Education_Tb_M_University_UniversityId",
                table: "Tb_M_Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Tb_T_Profiling_Tb_M_Education_EducationId",
                table: "Tb_T_Profiling");

            migrationBuilder.AlterColumn<int>(
                name: "EducationId",
                table: "Tb_T_Profiling",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "Tb_M_Education",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_M_Education_Tb_M_University_UniversityId",
                table: "Tb_M_Education",
                column: "UniversityId",
                principalTable: "Tb_M_University",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_T_Profiling_Tb_M_Education_EducationId",
                table: "Tb_T_Profiling",
                column: "EducationId",
                principalTable: "Tb_M_Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
