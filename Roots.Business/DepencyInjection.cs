using Microsoft.Extensions.DependencyInjection;
using Roots.Business.Interfaces;
using Roots.Business.Services;

namespace Roots.Business
{
    public static class DepencyInjection
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
        {
            services.AddScoped<IEventService, EventService>();

            services.AddScoped<IPersonService, PersonService>();

            return services;
        }
    }
}
