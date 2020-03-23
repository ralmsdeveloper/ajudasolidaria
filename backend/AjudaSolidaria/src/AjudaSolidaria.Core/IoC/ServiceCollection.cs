﻿using AjudaSolidaria.Domain.Entity;
using AjudaSolidaria.Domain.Request;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace AjudaSolidaria.Core.IoC
{
    public static class ServiceCollection
    {
        public static void AddServicesCore(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PessoaRequest, Pessoa>();
            });

            IMapper mapper = config.CreateMapper();

            services.AddSingleton(mapper); 
        }
    }
}
