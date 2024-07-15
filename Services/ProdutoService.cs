using Microsoft.EntityFrameworkCore;
using ProdutoProntoDigital.Data;
using ProdutoProntoDigital.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdutoProntoDigital.Services
{
    public class ProdutoService
    {
        private readonly ApplicationDbContext _context;

        public ProdutoService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Chamando procedure pra CONSULTAR produtos
        public async Task<List<Produto>> GetAllProducts()
        {
            var produtosDto = await _context.Set<ProdutoDTO>()
                .FromSqlRaw("EXEC GetAllProducts")
                .ToListAsync();

            var produtos = produtosDto.Select(dto => new Produto
            {
                PROD_ID = dto.PROD_ID,
                PROD_NOME = dto.PROD_NOME ?? string.Empty,
                PROD_PRECO = dto.PROD_PRECO,
                PROD_QTD = dto.PROD_QTD,
                CAT_ID = dto.CAT_ID,
                NomeCategoria = dto.NomeCategoria ?? string.Empty
            }).ToList();

            return produtos;
        }

        // Chamando procedure pra CRIAR produto
        public async Task CreateProduct(Produto produto)
        {
            var sql = "EXEC InsertProduto @PROD_NOME, @PROD_PRECO, @PROD_QTD, @CAT_ID";
            var parameters = new[]
            {
                new SqlParameter("@PROD_NOME", produto.PROD_NOME),
                new SqlParameter("@PROD_PRECO", produto.PROD_PRECO),
                new SqlParameter("@PROD_QTD", produto.PROD_QTD),
                new SqlParameter("@CAT_ID", produto.CAT_ID)
            };

            await _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        // Chama procedura pra CONSULTAR categorias ATIVAS pro dropdown
        public async Task<List<Categoria>> GetAllActiveCategorias()
        {
            return await _context.Categorias
                .FromSqlRaw("EXEC GetAllActiveCategorias")
                .ToListAsync();
        }

        // Chama procedure pra DELETAR produto
        public async Task DeleteProduct(int produtoId)
        {
            var sql = "EXEC DeleteProduto @PROD_ID";
            var parameters = new[] { new SqlParameter("@PROD_ID", produtoId) };
            await _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public async Task<Produto> GetProductById(int id)
        {
            var sql = "EXEC GetProductById @PROD_ID";
            var parameters = new[] { new SqlParameter("@PROD_ID", id) };

            var result = await _context.Set<ProdutoDTO>()
                .FromSqlRaw(sql, parameters)
                .ToListAsync();

            var produtoDto = result.FirstOrDefault();
            if (produtoDto == null) return null;

            return new Produto
            {
                PROD_ID = produtoDto.PROD_ID,
                PROD_NOME = produtoDto.PROD_NOME ?? string.Empty,
                PROD_PRECO = produtoDto.PROD_PRECO,
                PROD_QTD = produtoDto.PROD_QTD,
                CAT_ID = produtoDto.CAT_ID,
                NomeCategoria = produtoDto.NomeCategoria ?? string.Empty
            };
        }

        public async Task UpdateProduct(Produto produto)
        {
            var sql = "EXEC UpdateProduto @PROD_ID, @PROD_NOME, @PROD_PRECO, @PROD_QTD, @CAT_ID";
            var parameters = new[]
            {
                new SqlParameter("@PROD_ID", produto.PROD_ID),
                new SqlParameter("@PROD_NOME", produto.PROD_NOME),
                new SqlParameter("@PROD_PRECO", produto.PROD_PRECO),
                new SqlParameter("@PROD_QTD", produto.PROD_QTD),
                new SqlParameter("@CAT_ID", produto.CAT_ID)
            };

            await _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }

    }
}
