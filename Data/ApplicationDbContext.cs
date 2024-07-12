using Microsoft.EntityFrameworkCore;
using ProdutoProntoDigital.Models;

namespace ProdutoProntoDigital.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
    }
}
