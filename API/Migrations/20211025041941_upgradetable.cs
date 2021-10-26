using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class upgradetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_T_Education_Tb_M_University_UniversityId1",
                table: "TB_T_Education");

            migrationBuilder.DropIndex(
                name: "IX_TB_T_Education_UniversityId1",
                table: "TB_T_Education");

            migrationBuilder.DropColumn(
                name: "UniversityId1",
                table: "TB_T_Education");

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "TB_T_Education",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_Education_UniversityId",
                table: "TB_T_Education",
                column: "UniversityId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_T_Education_Tb_M_University_UniversityId",
                table: "TB_T_Education",
                column: "UniversityId",
                principalTable: "Tb_M_University",
                principalColumn: "UniversityId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_T_Education_Tb_M_University_UniversityId",
                table: "TB_T_Education");

            migrationBuilder.DropIndex(
                name: "IX_TB_T_Education_UniversityId",
                table: "TB_T_Education");

            migrationBuilder.AlterColumn<string>(
                name: "UniversityId",
                table: "TB_T_Education",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UniversityId1",
                table: "TB_T_Education",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_Education_UniversityId1",
                table: "TB_T_Education",
                column: "UniversityId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_T_Education_Tb_M_University_UniversityId1",
                table: "TB_T_Education",
                column: "UniversityId1",
                principalTable: "Tb_M_University",
                principalColumn: "UniversityId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
