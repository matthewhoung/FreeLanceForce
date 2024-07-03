using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddScoped<IFormRepository, FormRepository>();
            services.AddScoped<IOrderFormRepository, OrderFormRepository>();
            services.AddScoped<ISerialNumberRepository, SerialNumberRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
