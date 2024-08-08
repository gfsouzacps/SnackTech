using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using SnackTech.Application.UseCases;
using SnackTech.Domain.Ports.Driving;

namespace SnackTech.Application
{
    [ExcludeFromCodeCoverage]
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