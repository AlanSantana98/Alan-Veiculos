using Microsoft.EntityFrameworkCore;
using Alan_Veiculos.Models;

namespace Alan_Veiculos.Properties.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ClientesViewModel> Clientes { get; set; }
    }
}
