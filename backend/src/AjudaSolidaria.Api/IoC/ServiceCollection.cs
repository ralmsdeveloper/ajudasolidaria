using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace AjudaSolidaria.Api.IoC
{
    public static class ServiceCollection
    {
        public static void AddServiceSwaggerCustom(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Ajuda Solidaria",
                        Version = "v1",
                        Contact = new OpenApiContact
                        {
                            Name = "Ajuda Solidaria",
                            Url = new Uri("http://ajudasolidaria.com.br")
                        }
                    }); 

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                options.IncludeXmlComments(xmlPath);
            });
        }

        public static void UseApplicationSwaggerCustom(this IApplicationBuilder app)
        {
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(
                    "v1/swagger.json",
                    "Ajuda Solidaria v1");
            });
        }

        public static void AddAuthenticationJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var secretKey = Encoding.ASCII.GetBytes(configuration["SecretKey"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
