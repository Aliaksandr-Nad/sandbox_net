using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace PokemonShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((context, builder) =>
                    {
                        builder
                            .SetBasePath(context.HostingEnvironment.ContentRootPath)
                            .AddYamlFile("appsettings.yaml", false, true)
                            // .AddYamlFile($"appsettings.{context.HostingEnvironment.EnvironmentName.ToLower()}.yaml",
                                // true, true)
                            .AddEnvironmentVariables();
                    });

                    webBuilder.UseStartup<Startup>();
                });
    }
}