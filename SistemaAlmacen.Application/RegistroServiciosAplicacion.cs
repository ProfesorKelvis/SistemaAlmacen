using Microsoft.Extensions.DependencyInjection;
using SistemaAlmacen.Application.Contracts.UseCases;
using SistemaAlmacen.Application.UseCases;

namespace SistemaAlmacen.Application
{
    public static class RegistroServiciosAplicacion
    {
        public static IServiceCollection RegistrarServiciosAplicacion(this IServiceCollection services)
        {
            services.AddScoped<IGestionAlmacenUseCase, GestionAlmacenUseCase>();

            return services;
        }
    }
}
