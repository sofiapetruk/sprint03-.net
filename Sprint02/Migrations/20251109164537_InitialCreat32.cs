using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sprint02.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreat32 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alugueis");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "T_STATUS_MOTO",
                type: "NVARCHAR2(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.CreateIndex(
                name: "IX_T_STATUS_MOTO_status",
                table: "T_STATUS_MOTO",
                column: "status",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_T_STATUS_MOTO_status",
                table: "T_STATUS_MOTO");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "T_STATUS_MOTO",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(450)");

            migrationBuilder.CreateTable(
                name: "Alugueis",
                columns: table => new
                {
                    id_aluguel = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    fk_moto_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    fk_usuario_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    dataFim = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    dataInicio = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    status = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    valorTotal = table.Column<double>(type: "BINARY_DOUBLE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alugueis", x => x.id_aluguel);
                    table.ForeignKey(
                        name: "FK_Aluguel_Moto",
                        column: x => x.fk_moto_id,
                        principalTable: "T_MOTO",
                        principalColumn: "IdMoto",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Aluguel_Usuario",
                        column: x => x.fk_usuario_id,
                        principalTable: "T_USUARIO",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alugueis_fk_moto_id",
                table: "Alugueis",
                column: "fk_moto_id");

            migrationBuilder.CreateIndex(
                name: "IX_Alugueis_fk_usuario_id",
                table: "Alugueis",
                column: "fk_usuario_id");
        }
    }
}
