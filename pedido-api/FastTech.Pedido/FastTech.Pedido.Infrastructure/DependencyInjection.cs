using FastTech.Pedido.Domain.Interfaces.Command;
using FastTech.Pedido.Domain.Interfaces.Query;
using FastTech.Pedido.Infrastructure.Persistance.Command;
using FastTech.Pedido.Infrastructure.Persistance.Query;
using FastTech.Pedido.Infrastructure.Repositories.Command;
using FastTech.Pedido.Infrastructure.Repositories.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastTech.Pedido.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")
                ?? throw new InvalidOperationException("Nenhuma string de conexão encontrada para serviço de catalogo.");

            services.AddDbContext<PedidoCommandDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                        .EnableDetailedErrors()
                        .EnableSensitiveDataLogging()
                        .LogTo(msg => Debug.WriteLine(msg), LogLevel.Information));

            services.AddDbContext<PedidoQueryDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddScoped<IPedidoQueryRepository, PedidoQueryRepository>();
            services.AddScoped<IPedidoCommandRepository, PedidoCommandRepository>();

            services.AddScoped<IItemPedidoQueryRepository, ItemPedidoQueryRepository>();
            services.AddScoped<IItemPedidoCommandRepository, ItemPedidoCommandRepository>();

            services.AddScoped<IStatusPedidoHistoricoQueryRepository, StatusPedidoHistoricoQueryRepository>();
            services.AddScoped<IStatusPedidoHistoricoCommandRepository, StatusPedidoHistoricoCommandRepository>();

            return services;
        }
    }
}
