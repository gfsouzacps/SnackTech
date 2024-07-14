using Microsoft.Extensions.DependencyInjection;
using SnackTech.Adapter.DataBase.Repositories;
using SnackTech.Domain.Contracts;

namespace SnackTech.Adapter.DataBase
{
    public static class ModuleInjectionDependency
    {
        public static IServiceCollection AddAdapterDatabaseRepositories(this IServiceCollection services){
            
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IPedidoRepository, PedidoRepository>();
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            
            return services;
        }
    }
}