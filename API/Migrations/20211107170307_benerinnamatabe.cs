using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class benerinnamatabe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoles_Tb_T_Employee_NIK",
                table: "AccountRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Tb_T_Employee_Tb_M_Employee_NIK",
                table: "Tb_T_Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Tb_T_Profiling_Tb_T_Employee_NIK",
                table: "Tb_T_Profiling");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tb_T_Employee",
                table: "Tb_T_Employee");

            migrationBuilder.RenameTable(
                name: "Tb_T_Employee",
                newName: "Tb_T_Account");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tb_T_Account",
                table: "Tb_T_Account",
                column: "NIK");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoles_Tb_T_Account_NIK",
                table: "AccountRoles",
                column: "NIK",
                principalTable: "Tb_T_Account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_T_Account_Tb_M_Employee_NIK",
                table: "Tb_T_Account",
                column: "NIK",
                principalTable: "Tb_M_Employee",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_T_Profiling_Tb_T_Account_NIK",
                table: "Tb_T_Profiling",
                column: "NIK",
                principalTable: "Tb_T_Account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoles_Tb_T_Account_NIK",
                table: "AccountRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Tb_T_Account_Tb_M_Employee_NIK",
                table: "Tb_T_Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Tb_T_Profiling_Tb_T_Account_NIK",
                table: "Tb_T_Profiling");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tb_T_Account",
                table: "Tb_T_Account");

            migrationBuilder.RenameTable(
                name: "Tb_T_Account",
                newName: "Tb_T_Employee");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tb_T_Employee",
                table: "Tb_T_Employee",
                column: "NIK");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoles_Tb_T_Employee_NIK",
                table: "AccountRoles",
                column: "NIK",
                principalTable: "Tb_T_Employee",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_T_Employee_Tb_M_Employee_NIK",
                table: "Tb_T_Employee",
                column: "NIK",
                principalTable: "Tb_M_Employee",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_T_Profiling_Tb_T_Employee_NIK",
                table: "Tb_T_Profiling",
                column: "NIK",
                principalTable: "Tb_T_Employee",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
