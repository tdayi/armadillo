using Armadillo.Application.Concrete.Cache;
using Armadillo.Application.Discovery;
using Armadillo.Application.Navigation;
using Armadillo.Application.Vehicle;
using Armadillo.Core.Cache;
using Armadillo.Core.Discovery;
using Armadillo.Core.Navigation;
using Armadillo.Core.Vehicle;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Armadillo.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            services.AddControllersWithViews();

            var assembly = Assembly.GetAssembly(typeof(Application.Handler.Command.Discovery.DiscoveryHandler));

            services.AddMediatR(assembly);

            services.AddTransient<ICacheManager, InMemoryCacheManager>();

            services.AddTransient<IDiscoveryAreaFactory, DiscoveryAreaFactory>();
            services.AddTransient<ISpaceVehicleFactory, SpaceVehicleFactory>();

            services.AddTransient<INavigator, EastNavigator>();
            services.AddTransient<INavigator, NorthNavigator>();
            services.AddTransient<INavigator, SouthNavigator>();
            services.AddTransient<INavigator, WestNavigator>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Armadillo API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Armadillo API v1"));
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
