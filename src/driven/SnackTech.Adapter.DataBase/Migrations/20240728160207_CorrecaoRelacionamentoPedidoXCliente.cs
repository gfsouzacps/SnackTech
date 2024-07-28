using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnackTech.Adapter.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoRelacionamentoPedidoXCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Cliente_ClienteId",
                table: "Pedido");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Pedido",
                newName: "IdCliente");

            migrationBuilder.RenameIndex(
                name: "IX_Pedido_ClienteId",
                table: "Pedido",
                newName: "IX_Pedido_IdCliente");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Cliente_IdCliente",
                table: "Pedido",
                column: "IdCliente",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Cliente_IdCliente",
                table: "Pedido");

            migrationBuilder.RenameColumn(
                name: "IdCliente",
                table: "Pedido",
                newName: "ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Pedido_IdCliente",
                table: "Pedido",
                newName: "IX_Pedido_ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Cliente_ClienteId",
                table: "Pedido",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
