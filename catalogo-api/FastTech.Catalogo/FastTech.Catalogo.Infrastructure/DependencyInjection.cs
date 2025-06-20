﻿using FastTech.Catalogo.Domain.Interfaces;
using FastTech.Catalogo.Infrastructure.Persistence.Command;
using FastTech.Catalogo.Infrastructure.Persistence.Query;
using FastTech.Catalogo.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastTech.Catalogo.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<CatalogoCommandDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddDbContext<CatalogoQueryDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ITipoRefeicaoRepository, TipoRefeicaoRepository>();

            return services;
        }
    }
}
