using Microsoft.Extensions.DependencyInjection;
using MyDishesApp.Repository.Extensions;
using MyDishesApp.Service.Services;
using MyDishesApp.Service.Services.Interfaces;

namespace MyDishesApp.Service.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddServiceLayerWithDependencies(this IServiceCollection services, string connectionString)
        {
            services.AddRepositoryLayer(connectionString);
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IDishService, DishService>();
            services.AddScoped<IIngredientService, IngredientService>();
        }
    }
}