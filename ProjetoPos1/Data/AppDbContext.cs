using Microsoft.EntityFrameworkCore;
using ProjetoPos1.Models;
namespace ProjetoPos1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Pedido> Pedidos { get; set; }
    }
}
