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
        public DbSet<FuncionariosViewModel> Funcionarios { get; set; }
        public DbSet<UsuariosViewModel> Usuarios { get; set; }
        public DbSet<VeiculosViewModel> Veiculos { get; set; }
        public DbSet<VendasViewModel> Vendas { get; set; }
    }
}
