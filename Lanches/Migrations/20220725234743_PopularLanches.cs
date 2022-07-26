using Lanches.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Globalization;

#nullable disable

namespace Lanches.Migrations
{
    public partial class PopularLanches : Migration
    {
        private List<Lanche> Lanches = new List<Lanche>()
        {
            new Lanche()
            {
                CategoriaId = 1,
                DescricaoCurta = "Pão, hamburger, ovo, presunto, queijo, batata palha",
                DescricaoDetalhada = "Delicioso pão de hambúrger com ovo frito; presunto e queijo de primeira" +
                                     "qualidade acompanhado de batata palha",
                EmEstoque = true,
                ImagemThumbnailUrl = "https://www.macoratti.net/Imagens/lanches/cheesesalada1.jpg",
                ImagemUrl = "https://www.macoratti.net/Imagens/lanches/cheesesalada1.jpg",
                IsLanchePreferido = true,
                Nome = "Cheese Salada",
                Preco = 25.55m
            },
            new Lanche()
            {
               CategoriaId = 1,
               DescricaoCurta = "Pão, presunto, mussarela e tomate",
               DescricaoDetalhada = "Delicioso pão francês quentinho na chapa com presunto e " +
                                    "mussarela bem servidos com tomate preparado com carinho.",
               EmEstoque = true,
               ImagemThumbnailUrl = "http://www.macoratti.net/Imagens/lanches/mistoquente4.jpg",
               ImagemUrl = "http://www.macoratti.net/Imagens/lanches/mistoquente4.jpg",
               IsLanchePreferido = false,
               Nome = "Misto Quente",
               Preco =  8.00m
            },
            new Lanche()
            {
               CategoriaId = 2,
               DescricaoCurta = "Pão Integral, queijo branco, peito de peru, cenoura, alface, iogurte",
               DescricaoDetalhada = "Pão integral natural com queijo branco, peito de peru e cenoura ralada com alface " +
                                    "picado e iorgurte natural.",
               EmEstoque = true,
               ImagemThumbnailUrl = "http://www.macoratti.net/Imagens/lanches/lanchenatural.jpg",
               ImagemUrl = "http://www.macoratti.net/Imagens/lanches/lanchenatural.jpg",
               IsLanchePreferido = false,
               Nome = "Lanche Natural Peito Peru",
               Preco =  15.00m
            },
            new Lanche ()
            {
               CategoriaId = 1,
               DescricaoCurta = "Pão, hambúrger, presunto, mussarela e batalha palha",
               DescricaoDetalhada = "Pão de hambúrger especial com hambúrger de nossa preparação e presunto e mussarela; " +
                                    "acompanha batata palha.",
               EmEstoque = true,
               ImagemThumbnailUrl = "http://www.macoratti.net/Imagens/lanches/cheeseburger1.jpg",
               ImagemUrl = "http://www.macoratti.net/Imagens/lanches/cheeseburger1.jpg",
               IsLanchePreferido = false,
               Nome = "Cheese Burger",
               Preco =  11.00m
            }
        };

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            foreach (var lance in Lanches)
            {
                migrationBuilder.Sql("INSERT INTO Lanches(Nome, DescricaoCurta, DescricaoDetalhada, Preco, ImagemUrl, ImagemThumbnailUrl, IsLanchePreferido, EmEstoque, CategoriaId) " +
                    $"VALUES('{lance.Nome}','{lance.DescricaoCurta}', '{lance.DescricaoDetalhada}', {decimal.Parse(lance.Preco.ToString(), CultureInfo.GetCultureInfo("en-US"))}, '{lance.ImagemUrl}', '{lance.ImagemThumbnailUrl}', {Convert.ToByte(lance.IsLanchePreferido)},{Convert.ToByte(lance.EmEstoque)}, {lance.CategoriaId})"
                    );
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            foreach (var lanche in Lanches)
            {
                migrationBuilder.Sql($"DELETE FROM Lanches WERE Nome = '{lanche.Nome}'");
            }
        }
    }
}
