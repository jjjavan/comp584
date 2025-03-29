using Ah_webApiBackend.Data;
using Microsoft.EntityFrameworkCore;

namespace Ah_webApiBackend;

/// <summary>
///     The program.
/// </summary>
public static class Program
{
    /// <summary>
    ///     The main.
    /// </summary>
    /// <param name="args">
    ///     The args.
    /// </param>
    /// <returns>
    ///     The
    ///     <see>
    ///         <cref>void</cref>
    ///     </see>
    ///     .
    /// </returns>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args) ??
                      throw new ArgumentNullException("WebApplication.CreateBuilder(args)");

        // Add services to the container.
        builder.Services.AddControllers();


        // Register CRMDbContext with Dependency Injection
        builder.Services.AddDbContext<CrmDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            // Code to troubleshoot the connection string
            Console.WriteLine($"Connection String at runtime: {connectionString}");
            // Search for the word or character that is causing an issue and throw exception
            /* var keyWord = ""; // Takes keyword from the connectionString that is causing an issue
             if (connectionString.Contains(keyWord))
                 throw new Exception($"Invalid keyword {keyWord}detected in connection string: {connectionString}");*/
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My CRM API v1"));
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        // Map controllers to routes
        app.MapControllers(); // This is crucial to ensure route mapping

        app.Run();
    }
}