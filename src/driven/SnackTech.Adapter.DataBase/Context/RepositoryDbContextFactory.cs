using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace SnackTech.Adapter.DataBase.Context
{
    /// <summary>
    /// For local use only
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class RepositoryDbContextFactory : IDesignTimeDbContextFactory<RepositoryDbContext>
    {
        public RepositoryDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connString = configuration.GetConnectionString("DefaultConnection")
                             ?? throw new InvalidOperationException("Connection string is not set in configuration.");

            var optionsBuilder = new DbContextOptionsBuilder<RepositoryDbContext>();
            optionsBuilder.UseSqlServer(connString);

            return new RepositoryDbContext(optionsBuilder.Options);
        }
    }
}
