using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokemonShop.Data;

namespace PokemonShop.Extensions
{
    public static class PokemonShopExtensions
    {
        public static IServiceCollection UsePokemonShopDbContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PokemonShop");
            return services
                .AddDbContext<PokemonShopDbContext>(options =>
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        }   
    }
}