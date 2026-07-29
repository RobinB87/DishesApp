using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MyDishesApp.Service.MappingProfiles;
using System;
using System.IO;
using System.Reflection;

namespace MyDishesApp.WebApi.Extensions
{
    /// <summary>
    /// Extension for Service Collection
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Add automapper
        /// </summary>
        /// <param name="services"></param>
        public static void AddAssembliesToAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
        }

        /// <summary>
        /// Add Swagger
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var openApiInfo = new OpenApiInfo
            {
                Title = "Dishes App Api",
                Description = "WebApi that exposes dishes",
                Version = configuration.GetValue<string>("Version")
            };

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("webapi", openApiInfo);
            });
        }
    }
}