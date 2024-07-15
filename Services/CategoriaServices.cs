using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProdutoProntoDigital.Models;
using ProdutoProntoDigital.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProdutoProntoDigital.Services
{
    public class CategoriaServices
    {
        private readonly ApplicationDbContext _context;

        public CategoriaServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Categoria>> GetAllActiveCategorias()
        {
            return await _context.Categorias
                .FromSqlRaw("EXEC GetAllActiveCategorias")
                .ToListAsync();
        }

        public async Task<List<Categoria>> GetAllInactiveCategorias()
        {
            return await _context.Categorias
                .FromSqlRaw("EXEC GetAllInactiveCategorias")
                .ToListAsync();
        }

        public async Task AddCategoria(string catNome)
        {
            var sql = "EXEC InsertCategoria @CAT_NOME";
            var parameters = new[] { new SqlParameter("@CAT_NOME", catNome) };
            await _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public async Task InactivateCategoria(int id)
        {
            var sql = "EXEC InactivateCategoria @CAT_ID";
            var parameters = new[] { new SqlParameter("@CAT_ID", id) };
            await _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public async Task ActivateCategoria(int id)
        {
            var sql = "EXEC ActivateCategoria @CAT_ID";
            var parameters = new[] { new SqlParameter("@CAT_ID", id) };
            await _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public async Task<Categoria> GetCategoriaById(int id)
        {
            var sql = "EXEC GetCategoriaById @CAT_ID";
            var parameters = new[] { new SqlParameter("@CAT_ID", id) };

            var result = await _context.Categorias
                .FromSqlRaw(sql, parameters)
                .ToListAsync(); 

            return result.FirstOrDefault();
        }


        public async Task UpdateCategoria(Categoria categoria)
        {
            var sql = "EXEC UpdateCategoria @CAT_ID, @CAT_NOME, @CAT_STATUS";
            var parameters = new[]
            {
                new SqlParameter("@CAT_ID", categoria.CAT_ID),
                new SqlParameter("@CAT_NOME", categoria.CAT_NOME),
                new SqlParameter("@CAT_STATUS", categoria.CAT_STATUS)
            };

            await _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public async Task DeleteCategoriaAndProducts(int id)
        {
            var sql = "EXEC DeleteCategoriaAndProducts @CAT_ID";
            var parameters = new[] { new SqlParameter("@CAT_ID", id) };
            await _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }


    }
}
