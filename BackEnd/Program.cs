using BX.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = Environment.GetEnvironmentVariable("BuildXpertDB");
if (connectionString.IsNullOrEmpty())
{
    connectionString = builder.Configuration.GetConnectionString("userSecretDB"); //Run this string as your secret in the local terminal
}

builder.Services.AddDbContext<BuildXpertContext>(options =>
{
    options.UseSqlServer(connectionString,
    sqlOptions => sqlOptions.MigrationsAssembly("BackEnd"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BuildXpertContext>();
    try
    {
        // Apply pending migrations. This ensures your database is up-to-date.
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        // Print the error details to the console.
        Console.WriteLine("An error occurred while migrating the database:");
        Console.WriteLine(ex.Message);
        // Optionally print the full exception stack trace.
        Console.WriteLine(ex);
    }
}

app.Run();
