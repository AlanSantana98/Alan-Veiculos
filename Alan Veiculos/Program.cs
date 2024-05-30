using Alan_Veiculos.Properties.Context;
using Microsoft.EntityFrameworkCore;

namespace Alan_Veiculos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Conexão com Banco de Dados
            string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContextPool<AppDbContext>(options =>
                options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
