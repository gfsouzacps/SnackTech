using System.Diagnostics.CodeAnalysis;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace SnackTech.API.Configuration.HealthChecks
{
    [ExcludeFromCodeCoverage]
    public static class HealthChecksExtensions
    {
        private static readonly string[] BaseTags = ["SnackTech-API","Database"];
        public static IHealthChecksBuilder ConfigureSQLHealthCheck(this IHealthChecksBuilder builder, IConfiguration configuration){
            var connectionString = configuration["ConnectionStrings:DefaultConnection"] ?? throw new ArgumentException("Connection String nao configurada.");
            builder.AddSqlServer(connectionString, 
                                                    healthQuery: "SELECT 1", 
                                                    name: "SQL Server", 
                                                    failureStatus: HealthStatus.Unhealthy, 
                                                    tags: BaseTags);
            return builder;
        }

        public static void UseCustomHealthChecks(this WebApplication app){
            app.MapHealthChecks("/api/health", new HealthCheckOptions(){
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        }

    }
}