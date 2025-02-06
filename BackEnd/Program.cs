using BX.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("userSecretDB"); //Run this string as your secret in the local terminal
if (connectionString.IsNullOrEmpty())
{
    connectionString = Environment.GetEnvironmentVariable("BuildXpertDB");
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

app.Run();
