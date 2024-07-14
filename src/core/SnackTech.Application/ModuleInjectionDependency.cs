using Microsoft.Extensions.DependencyInjection;
using SnackTech.Application.Interfaces;
using SnackTech.Application.UseCases;

namespace SnackTech.Application
{
    public static class ModuleInjectionDependency
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services){
            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<IPedidoService, PedidoService>();
            services.AddTransient<IProdutoService, ProdutoService>();

            return services;
        }
    }
}