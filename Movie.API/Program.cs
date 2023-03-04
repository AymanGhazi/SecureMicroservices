using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Movies.Api.Data;
using Movies.API.Data;
using Movies.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
           var host =CreateHostBuilder(args).Build();
             seedDatabase(host);
            host.Run();
        }

        private static  void seedDatabase(IHost host)
        {
           using (var scope= host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var moviesContext = services.GetRequiredService<MoviesContext>();
                 MoviesContextSeed.SeedAsync(moviesContext);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
