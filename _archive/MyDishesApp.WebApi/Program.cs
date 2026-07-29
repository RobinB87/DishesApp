using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyDishesApp.Repository.Data;

namespace MyDishesApp.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Migrate and seed the database.
            using (var scope = host.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetService<DishesContext>();
                    context.Database.Migrate();
                    context.EnsureSeedDataForContext();
                }
                catch (System.Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred with migrating or seeding the DB.");
                }
            }

            // Run the web app
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}