using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AjudaSolidaria.Domain.Entity;
using AjudaSolidaria.Respository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AjudaSolidaria.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var db = host
                .Services
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<AjudaSolidariaContext>();

            if(!db.Set<Cidade>().Any())
            {
                var file = Path.Combine(AppContext.BaseDirectory, "Util", "cidades.json");
                if(File.Exists(file))
                {
                    var json = File.ReadAllText(file);
                    var cidades = System.Text.Json.JsonSerializer.Deserialize<List<Cidade>>(json);

                    var set = db.Set<Cidade>();
                    foreach (var cidade in cidades)
                    {
                        set.Add(cidade);
                    }

                    db.SaveChanges();
                }
            }

            host.Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
