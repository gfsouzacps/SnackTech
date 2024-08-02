﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnackTech.Adapter.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class CreateClientePadrao : Migration
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

            migrationBuilder.InsertData(
                table: "Pessoa",
                columns: new[] { "Id", "Nome" },
                values: new object[] { new Guid("6ee54a46-007f-4e4c-9fe8-1a13eadf7fd1"), "Cliente Padrão" });

            migrationBuilder.InsertData(
                table: "Cliente",
                columns: new[] { "Id", "Cpf", "Email" },
                values: new object[] { new Guid("6ee54a46-007f-4e4c-9fe8-1a13eadf7fd1"), "12345678901", "cliente.padrao@gmail.com" });

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

            migrationBuilder.DeleteData(
                table: "Cliente",
                keyColumn: "Id",
                keyValue: new Guid("6ee54a46-007f-4e4c-9fe8-1a13eadf7fd1"));

            migrationBuilder.DeleteData(
                table: "Pessoa",
                keyColumn: "Id",
                keyValue: new Guid("6ee54a46-007f-4e4c-9fe8-1a13eadf7fd1"));

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
