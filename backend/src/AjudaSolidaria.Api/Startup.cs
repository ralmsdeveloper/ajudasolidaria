using AjudaSolidaria.Api.IoC;
using AjudaSolidaria.Core.IoC;
using AjudaSolidaria.Respository.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace AjudaSolidaria.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMemoryCache();
            services.AddHttpClient();
            services.AddServicesRepository(Configuration);
            services.AddServiceSwaggerCustom();
            services.AddServicesCore();
            services.AddApplicationInsightsTelemetry();
            services.AddAuthenticationJwt(Configuration);
            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseApplicationSwaggerCustom();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(policy => policy
                .SetIsOriginAllowed(x => _ = true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithExposedHeaders("Content-Disposition")
                .Build());

            app.Use(async (context, next) =>
            {
                var timer = System.Diagnostics.Stopwatch.StartNew();

                context.Response.OnStarting(() =>
                {
                    context.Response.Headers["X-Time-Process"] = timer.Elapsed.ToString();
                    return Task.CompletedTask;
                });

                await next.Invoke();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
