using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PeoplesPartnership.ApiRefactor.Database;

namespace PeoplesPartnership.ApiRefactor
{
    public class Program
    {
        public static void Main(string[] args)
        { 
            var builder = CreateHostBuilder(args);
                
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<StudioContext>();
                db.Database.Migrate();
            }
            
            app.Run();
        }
        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
