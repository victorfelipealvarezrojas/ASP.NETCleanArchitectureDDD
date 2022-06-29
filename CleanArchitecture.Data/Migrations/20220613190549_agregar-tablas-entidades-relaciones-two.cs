using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Data.Migrations
{
    public partial class agregartablasentidadesrelacionestwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoActor_Actor_ActorId",
                table: "VideoActor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actor",
                table: "Actor");

            migrationBuilder.RenameTable(
                name: "Actor",
                newName: "Actores");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actores",
                table: "Actores",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoActor_Actores_ActorId",
                table: "VideoActor",
                column: "ActorId",
                principalTable: "Actores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoActor_Actores_ActorId",
                table: "VideoActor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actores",
                table: "Actores");

            migrationBuilder.RenameTable(
                name: "Actores",
                newName: "Actor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actor",
                table: "Actor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoActor_Actor_ActorId",
                table: "VideoActor",
                column: "ActorId",
                principalTable: "Actor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
