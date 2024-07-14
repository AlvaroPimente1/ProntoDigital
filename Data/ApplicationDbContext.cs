using Microsoft.EntityFrameworkCore;
using ProdutoProntoDigital.Models;

namespace ProdutoProntoDigital.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.Property(e => e.PROD_PRECO)
                    .HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<ProdutoDTO>().HasNoKey().ToTable("ProdutoDTO");
        }
    }
}
