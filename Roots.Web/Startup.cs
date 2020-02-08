using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Roots.Business;
using Roots.Data;
using Roots.Web.Configuration;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.IO;

namespace Roots.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup), typeof(Business.DepencyInjection));

            services.AddDataLayer(Configuration);

            services.AddBusinessLayer();

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddControllers()
                .AddNewtonsoftJson();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Roots Api", Version = "v1" });
                options.IncludeXmlComments(Path.Combine(System.AppContext.BaseDirectory, "Roots.Web.xml"));
            });
            services.AddSwaggerGenNewtonsoftSupport();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var swaggerConfiguration = new SwaggerConfiguration();
            Configuration.GetSection(nameof(SwaggerConfiguration)).Bind(swaggerConfiguration);

            app.UseSwagger(options =>
            {
                options.RouteTemplate = swaggerConfiguration.JsonRoute;
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(swaggerConfiguration.UIEndpoint, swaggerConfiguration.Description);
                options.DefaultModelRendering(ModelRendering.Model);
                options.DefaultModelsExpandDepth(3);
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
