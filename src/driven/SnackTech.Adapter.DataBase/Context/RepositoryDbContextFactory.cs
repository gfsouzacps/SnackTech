using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace SnackTech.Adapter.DataBase.Context
{
    /// <summary>
    /// For local use only
    /// </summary>
    public class RepositoryDbContextFactory : IDesignTimeDbContextFactory<RepositoryDbContext>
    {
        public RepositoryDbContext CreateDbContext(string[] args)
        {
            var connString = "Server=localhost,1433;Database=snack-tech;User ID=sa;Password=Sorte@2020;Trusted_Connection=False; TrustServerCertificate=True;";
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryDbContext>();
            optionsBuilder.UseSqlServer(connString);

            return new RepositoryDbContext(optionsBuilder.Options);
        }
    }
}
