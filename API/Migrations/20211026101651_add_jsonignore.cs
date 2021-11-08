using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class add_jsonignore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "salary",
                table: "Tb_M_Employee",
                newName: "Salary");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Tb_M_Employee",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "gender",
                table: "Tb_M_Employee",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "Tb_M_Employee",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Tb_M_Employee",
                newName: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Salary",
                table: "Tb_M_Employee",
                newName: "salary");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Tb_M_Employee",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Tb_M_Employee",
                newName: "gender");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Tb_M_Employee",
                newName: "firstName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Tb_M_Employee",
                newName: "email");
        }
    }
}
