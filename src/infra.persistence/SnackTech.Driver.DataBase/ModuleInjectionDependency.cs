using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using SnackTech.Driver.DataBase.Repositories;
using SnackTech.Domain.Ports.Driven;
using SnackTech.Common.Interfaces;
using SnackTech.Driver.DataBase.DataSources;

namespace SnackTech.Driver.DataBase
{
    [ExcludeFromCodeCoverage]
    public static class ModuleInjectionDependency
    {
        public static IServiceCollection AddAdapterDatabaseRepositories(this IServiceCollection services){
            
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IPedidoRepository, PedidoRepository>();

            services.AddTransient<IProdutoDataSource, ProdutoDataSource>();
            
            return services;
        }
    }
}