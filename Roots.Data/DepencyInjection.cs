﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Roots.Data.Context;
using Microsoft.EntityFrameworkCore;
using Roots.Business.Interfaces;
using Roots.Data.Services;

namespace Roots.Data
{
    public static class DepencyInjection
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RootsDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IRootsDbContext, RootsDbContext>(provider => provider.GetService<RootsDbContext>());

            services.AddTransient<IDateTimeService, DateTimeService>();

            return services;
        }
    }
}
