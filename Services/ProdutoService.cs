using Microsoft.EntityFrameworkCore;
using ProdutoProntoDigital.Data;
using ProdutoProntoDigital.Models;
using System.Collections.Generic;
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
            return await _context.Produtos
                .FromSqlRaw("EXEC GetAllProducts")
                .ToListAsync();
        }  
    }
}
