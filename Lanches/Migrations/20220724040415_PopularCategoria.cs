using Lanches.Models;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lanches.Migrations
{
    public partial class PopularCategoria : Migration
    {
        private List<Categoria> Categorias = new List<Categoria>()
        {
            new Categoria() { CategoriaNome = "Normal", Descricao= "Lanche feito con ingredientes normais" },
            new Categoria() { CategoriaNome = "Natural", Descricao = "Lanche feito con ingredientes integrais e naturais" }
        };

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            foreach (var categoria in Categorias)
            {
                migrationBuilder.Sql("INSERT INTO Categorias(CategoriaNome, Descricao) " +
                    $"VALUES('{categoria.CategoriaNome}','{categoria.Descricao}')");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            foreach (var categoria in Categorias)
            {
                migrationBuilder.Sql($"DELETE FROM Categorias WHERE CategoriaNome = '{categoria.CategoriaNome}'");
            }
        }
    }
}
