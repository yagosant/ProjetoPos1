using Microsoft.EntityFrameworkCore;
using ProjetoPos1.Data;
using ProjetoPos1.Repositories;
using ProjetoPos1.Services;

namespace ProjetoPos1.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IPedidoService, PedidoService>();


            return services;
        }
    }
}
