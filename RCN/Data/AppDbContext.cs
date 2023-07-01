using RCN.Entities;
using Microsoft.EntityFrameworkCore;

namespace RCN.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Aqui vai a connection string 
            optionsBuilder.UseSqlServer("Server=localhost;Database=RCN;Trusted_Connection=True;");
        }

        public DbSet<Pedidos> Pedidos { get; set; }
        public DbSet<Produtos> Produtos { get; set; }
        public DbSet<ItensDePedido> ItensDePedido { get; set; }
    }
}
