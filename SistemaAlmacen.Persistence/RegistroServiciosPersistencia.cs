using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SistemaAlmacen.Application.Contracts.Persistence;
using SistemaAlmacen.Persistence.Repositories;

namespace SistemaAlmacen.Persistence
{
    public static class RegistroServiciosPersistencia
    {
        public static IServiceCollection RegistrarServiciosPersistencia(this IServiceCollection services)
        {
            services.AddDbContext<SistemaAlmacenDbContext>(options =>
                options.UseSqlServer("name=SistemaAlmacenConnectionString"));

            services.AddScoped<IRepositorioAlmacen, RepositorioAlmacen>();

            return services;
        }
    }
}
