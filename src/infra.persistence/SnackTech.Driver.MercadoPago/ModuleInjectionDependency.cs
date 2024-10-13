using Microsoft.Extensions.DependencyInjection;
using SnackTech.Common.Interfaces.ApiSources;
using SnackTech.Driver.MercadoPago.Services;

namespace SnackTech.Driver.MercadoPago
{
    public static class ModuleInjectionDependency
    {
        public static IServiceCollection AddMercadoPagoService(this IServiceCollection services){

            services.AddTransient<IMercadoPagoIntegration, MercadoPagoService>();

            return services;
        }
    }
}