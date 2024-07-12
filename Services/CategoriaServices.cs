using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProdutoProntoDigital.Models;
using ProdutoProntoDigital.Data;
using Microsoft.Data.SqlClient;

namespace ProdutoProntoDigital.Services
{
    public class CategoriaServices
    {
        private readonly ApplicationDbContext _context;

        public CategoriaServices(ApplicationDbContext context) 
        {
            _context = context; 
        }

        public async Task<List<Categoria>> GetAllCategorias()
        {
            return await _context.Categorias
                .FromSqlRaw("EXEC GetAllCategorias")
                .ToListAsync();
        }

        public async Task AddCategoria(string catNome)
        {
            var sql = "EXEC InsertCategoria @CAT_NOME";
            var parameters = new[] { new SqlParameter("@CAT_NOME", catNome) };
            await _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }

    }
}
