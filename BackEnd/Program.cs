using BX.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using BX.Services.Authentication;
using BX.Business.Managers;
using BX.Data.Repository;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region Dependency Injection
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IAuthenticationJwtService, AuthenticationJwtService>();
builder.Services.AddScoped<IUserManager, UserManager>();
#endregion


#region SQL Server Connection
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
#endregion


#region Authentication configuration

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<BuildXpertContext>()
    .AddDefaultTokenProviders();

var issuer = builder.Configuration["Authentication:Issuer"];
var audience = builder.Configuration["Authentication:Audience"];
var token = builder.Configuration["Authentication:Token"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(token!)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    }).
    AddGoogle(options =>
    {
        options.ClientId = "GOOGLE_CLIENT_ID";
        options.ClientSecret = "GOOGLE_CLIENT_SECRET";
    })
    .AddFacebook(options =>
    {
        options.AppId = "FACEBOOK_APP_ID";
        options.AppSecret = "FACEBOOK_APP_SECRET";
    })
    .AddMicrosoftAccount(options =>
    {
        options.ClientId = "MICROSOFT_CLIENT_ID";
        options.ClientSecret = "MICROSOFT_CLIENT_SECRET";
    });

#endregion







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
