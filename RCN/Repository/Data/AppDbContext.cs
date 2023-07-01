using RCN.Entities;
using Microsoft.EntityFrameworkCore;

namespace RCN.Repository.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Pedidos> Pedidos { get; set; }
        public DbSet<Produtos> Produtos { get; set; }
        public DbSet<ItensDePedido> ItensDePedido { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Aqui vai a connection string 
            optionsBuilder.UseSqlServer("Data Source =.; Initial Catalog = rcn_db; Integrated Security = True");
        }
    }
}
