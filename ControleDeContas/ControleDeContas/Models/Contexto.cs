using Microsoft.EntityFrameworkCore;

namespace ControleDeContas.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options):base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<Produto> Produto { get; set; }

        public DbSet<Tarefa> Tarefa { get; set; }

        public DbSet<Categoria> Categoria { get; set; }
    }
}
