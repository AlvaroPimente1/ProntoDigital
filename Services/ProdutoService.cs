using Microsoft.EntityFrameworkCore;
using ProdutoProntoDigital.Data;
using ProdutoProntoDigital.Models;  
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
    }
}
