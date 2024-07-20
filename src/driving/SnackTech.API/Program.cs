using SnackTech.API.Configuration.HealthChecks;
using SnackTech.Application;
using SnackTech.Adapter.DataBase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAdapterDatabaseRepositories();
builder.Services.AddApplicationServices();

builder.Services.AddHealthChecks()
                .ConfigureSQLHealthCheck(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.DescribeAllParametersInCamelCase();
});

var app = builder.Build();

app.UseCustomHealthChecks();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
