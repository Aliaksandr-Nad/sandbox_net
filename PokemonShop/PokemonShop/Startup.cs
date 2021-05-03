using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PokemonShop.Extensions;
using PokemonShop.Mappers;
using PokemonShop.Services;

namespace PokemonShop
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        private string Version => _configuration.GetValue<string>("Version");

        private string Title => _configuration.GetValue<string>("Title");

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration.GetSection("PokemonShop");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = Title, Version = Version}); });

            services.Configure<AppSettings>(_configuration);

            services.UsePokemonShopDbContext(_configuration)
                .AddAutoMapper(typeof(PokemonShopMapper).Assembly);

            services.AddTransient<OrderService, OrderService>()
                .AddTransient<EmailService, EmailService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection()
                .UseRouting()
                .UseSwagger()
                .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PokemonShop v1"));

            app.UseStaticFiles();

            app.UseAuthentication()
                .UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}