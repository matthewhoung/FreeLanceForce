using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddScoped<IGenericFormRepository, GenericFormRepository>();
            services.AddScoped<IOrderFormRepository, OrderFormRepository>();
            services.AddScoped<IAcceptanceFormRepository, AcceptanceFormRepository>();
            services.AddScoped<ISerialNumberRepository, SerialNumberRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped(provider => new List<Signature>());

            return services;
        }
    }
}
