using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using SnackTech.Core.Controllers;
using SnackTech.Core.Interfaces;

namespace SnackTech.Driver.API.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class DomainInjectionDependency
    {
        public static IServiceCollection AddDomainControllers(this IServiceCollection services)
        {
            services.AddTransient<IClienteController, ClienteController>();
            services.AddTransient<IProdutoController, ProdutoController>();
            services.AddTransient<IPedidoController, PedidoController>();
            services.AddTransient<IPagamentoController, PagamentoController>();

            return services;
        }
    }
}