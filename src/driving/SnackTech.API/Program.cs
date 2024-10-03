using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SnackTech.Adapter.DataBase;
using SnackTech.Adapter.DataBase.Context;
using SnackTech.API.Configuration.HealthChecks;
using SnackTech.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAdapterDatabaseRepositories();
builder.Services.AddApplicationServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.DescribeAllParametersInCamelCase();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SnackTech", Version = "v1" });
});

string dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";

if (string.IsNullOrEmpty(dbConnectionString))
{
    throw new InvalidOperationException(
        "Could not find a connection string named 'DefaultConnection'.");
}

builder.Services.AddDbContext<RepositoryDbContext>(options =>
    options.UseSqlServer(dbConnectionString));

builder.Services.AddHealthChecks()
    .ConfigureSQLHealthCheck();

var app = builder.Build();

using var scope = app.Services.CreateScope();
await using var dbContext = scope.ServiceProvider.GetRequiredService<RepositoryDbContext>();
await dbContext.Database.MigrateAsync();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SnackTech API v1");
});

//app.UseHttpsRedirection();
app.UseCustomHealthChecks();
app.UseAuthorization();
app.MapControllers();

app.Run();
