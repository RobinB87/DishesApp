using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyDishesApp.Repository.Data;
using MyDishesApp.Repository.Repositories;
using MyDishesApp.Repository.Repositories.Interfaces;

namespace MyDishesApp.Repository.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddRepositoryLayer(this IServiceCollection services, string connectionString)
        {
            //services.AddDbContext<DishesContext, DishesContext>();
            services.AddDbContext<DishesContext>(o => o.UseSqlServer(connectionString));
            services.AddScoped<IDishRepository, DishRepository>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}