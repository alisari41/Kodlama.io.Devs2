﻿using Kodlama.io.Devs2.Application.Services.Repositories;
using Kodlama.io.Devs2.Persistence.Contexts;
using Kodlama.io.Devs2.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs2.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BaseDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ProgrammingLanguageConnectionString")));// Projenin Adı Sonrasında ConnectionString

            #region Repository'lerin Bağlanması
            services.AddScoped<IProgrammingLanguageRepository, ProgrammingLanguageRepository>();// Eğer Biri IProgrammingLanguageRepository isterse ona ProgrammingLanguageRepository ver 
            services.AddScoped<ITechnologyRepository, TechnologyRepository>();
            #endregion

            return services;
        }
    }
}
