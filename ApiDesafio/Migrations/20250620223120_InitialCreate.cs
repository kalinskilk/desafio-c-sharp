using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiDesafio.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ambiente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeUnico = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ambiente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeatureToggle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeUnico = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    AtivoGlobalmente = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureToggle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguracaoAmbienteFeature",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FeatureToggleId = table.Column<int>(type: "INTEGER", nullable: false),
                    AmbienteId = table.Column<int>(type: "INTEGER", nullable: false),
                    AtivoNesteAmbiente = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracaoAmbienteFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfiguracaoAmbienteFeature_Ambiente_AmbienteId",
                        column: x => x.AmbienteId,
                        principalTable: "Ambiente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfiguracaoAmbienteFeature_FeatureToggle_FeatureToggleId",
                        column: x => x.FeatureToggleId,
                        principalTable: "FeatureToggle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracaoAmbienteFeature_AmbienteId",
                table: "ConfiguracaoAmbienteFeature",
                column: "AmbienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracaoAmbienteFeature_FeatureToggleId",
                table: "ConfiguracaoAmbienteFeature",
                column: "FeatureToggleId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureToggle_NomeUnico",
                table: "FeatureToggle",
                column: "NomeUnico",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfiguracaoAmbienteFeature");

            migrationBuilder.DropTable(
                name: "Ambiente");

            migrationBuilder.DropTable(
                name: "FeatureToggle");
        }
    }
}
