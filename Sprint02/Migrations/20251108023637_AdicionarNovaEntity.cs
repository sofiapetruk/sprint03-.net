using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sprint02.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarNovaEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdUsuario",
                table: "T_USUARIO",
                type: "NUMBER(10)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "NUMBER(19)")
                .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1")
                .OldAnnotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "T_STATUS_MOTO",
                type: "NUMBER(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.CreateTable(
                name: "Alugueis",
                columns: table => new
                {
                    id_aluguel = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    fk_usuario_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    fk_moto_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    dataInicio = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    dataFim = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    valorTotal = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    status = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alugueis");

            migrationBuilder.AlterColumn<long>(
                name: "IdUsuario",
                table: "T_USUARIO",
                type: "NUMBER(19)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)")
                .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1")
                .OldAnnotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "T_STATUS_MOTO",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)");
        }
    }
}
