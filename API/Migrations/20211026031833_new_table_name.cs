using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class new_table_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tb_M_Education_Tb_M_University_UniversityId",
                table: "Tb_M_Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Tb_T_Profiling_Tb_M_Education_EducationId",
                table: "Tb_T_Profiling");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tb_M_Education",
                table: "Tb_M_Education");

            migrationBuilder.RenameTable(
                name: "Tb_M_Education",
                newName: "Tb_T_Education");

            migrationBuilder.RenameIndex(
                name: "IX_Tb_M_Education_UniversityId",
                table: "Tb_T_Education",
                newName: "IX_Tb_T_Education_UniversityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tb_T_Education",
                table: "Tb_T_Education",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_T_Education_Tb_M_University_UniversityId",
                table: "Tb_T_Education",
                column: "UniversityId",
                principalTable: "Tb_M_University",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_T_Profiling_Tb_T_Education_EducationId",
                table: "Tb_T_Profiling",
                column: "EducationId",
                principalTable: "Tb_T_Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tb_T_Education_Tb_M_University_UniversityId",
                table: "Tb_T_Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Tb_T_Profiling_Tb_T_Education_EducationId",
                table: "Tb_T_Profiling");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tb_T_Education",
                table: "Tb_T_Education");

            migrationBuilder.RenameTable(
                name: "Tb_T_Education",
                newName: "Tb_M_Education");

            migrationBuilder.RenameIndex(
                name: "IX_Tb_T_Education_UniversityId",
                table: "Tb_M_Education",
                newName: "IX_Tb_M_Education_UniversityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tb_M_Education",
                table: "Tb_M_Education",
                column: "Id");

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
    }
}
