using FastTech.Autenticacao.Domain.Interfaces;
using FastTech.Autenticacao.Infraestructure.Persistance.Command;
using FastTech.Autenticacao.Infraestructure.Persistance.Query;
using FastTech.Autenticacao.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastTech.Autenticacao.Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AutenticacaoCommandDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddDbContext<AutenticacaoQueryDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            return services;
        }
    }
}
