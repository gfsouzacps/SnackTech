using Microsoft.Extensions.DependencyInjection;
using SnackTech.Common.Interfaces.DataSources;
using SnackTech.Driver.DataBase.DataSources;
using System.Diagnostics.CodeAnalysis;

namespace SnackTech.Driver.DataBase
{
    [ExcludeFromCodeCoverage]
    public static class ModuleInjectionDependency
    {
        public static IServiceCollection AddAdapterDatabaseRepositories(this IServiceCollection services){
            
            services.AddTransient<IClienteDataSource, ClienteDataSource>();
            services.AddTransient<IPedidoDataSource, PedidoDataSource>();
            services.AddTransient<IProdutoDataSource, ProdutoDataSource>();
            
            return services;
        }
    }
}