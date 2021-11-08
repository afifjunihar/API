using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addmodel_account : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_Accounts_Tb_M_Employee_NIK",
                        column: x => x.NIK,
                        principalTable: "Tb_M_Employee",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
