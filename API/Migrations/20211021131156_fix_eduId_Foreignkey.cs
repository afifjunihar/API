using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class fix_eduId_Foreignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profilings_Education_EducationId",
                table: "Profilings");

            migrationBuilder.DropIndex(
                name: "IX_Profilings_EducationId",
                table: "Profilings");

            migrationBuilder.DropColumn(
                name: "EducationId",
                table: "Profilings");

            migrationBuilder.CreateIndex(
                name: "IX_Profilings_Education_Id",
                table: "Profilings",
                column: "Education_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Profilings_Education_Education_Id",
                table: "Profilings",
                column: "Education_Id",
                principalTable: "Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profilings_Education_Education_Id",
                table: "Profilings");

            migrationBuilder.DropIndex(
                name: "IX_Profilings_Education_Id",
                table: "Profilings");

            migrationBuilder.AddColumn<int>(
                name: "EducationId",
                table: "Profilings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profilings_EducationId",
                table: "Profilings",
                column: "EducationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profilings_Education_EducationId",
                table: "Profilings",
                column: "EducationId",
                principalTable: "Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
