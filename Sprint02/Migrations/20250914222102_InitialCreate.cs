using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sprint02.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_STATUS_MOTO",
                columns: table => new
                {
                    IdStatus = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    data = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_STATUS_MOTO", x => x.IdStatus);
                });

            migrationBuilder.CreateTable(
                name: "T_TIPO_MOTO",
                columns: table => new
                {
                    IdTipo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nome_tipo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_TIPO_MOTO", x => x.IdTipo);
                });

            migrationBuilder.CreateTable(
                name: "T_USUARIO",
                columns: table => new
                {
                    IdUsuario = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    email = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    senha = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_USUARIO", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "T_MOTO",
                columns: table => new
                {
                    IdMoto = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_chassi = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    placa = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    unidade = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    IdStatus = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IdTipo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_MOTO", x => x.IdMoto);
                    table.ForeignKey(
                        name: "FK_Moto_StatusMoto",
                        column: x => x.IdStatus,
                        principalTable: "T_STATUS_MOTO",
                        principalColumn: "IdStatus",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Moto_TipoMoto",
                        column: x => x.IdTipo,
                        principalTable: "T_TIPO_MOTO",
                        principalColumn: "IdTipo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_MOTO_IdStatus",
                table: "T_MOTO",
                column: "IdStatus");

            migrationBuilder.CreateIndex(
                name: "IX_T_MOTO_IdTipo",
                table: "T_MOTO",
                column: "IdTipo");

            migrationBuilder.CreateIndex(
                name: "IX_T_MOTO_nm_chassi",
                table: "T_MOTO",
                column: "nm_chassi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_MOTO_placa",
                table: "T_MOTO",
                column: "placa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_USUARIO_email",
                table: "T_USUARIO",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_MOTO");

            migrationBuilder.DropTable(
                name: "T_USUARIO");

            migrationBuilder.DropTable(
                name: "T_STATUS_MOTO");

            migrationBuilder.DropTable(
                name: "T_TIPO_MOTO");
        }
    }
}
