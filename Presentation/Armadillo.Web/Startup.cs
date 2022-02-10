using Armadillo.Application.Concrete.Cache;
using Armadillo.Application.Concrete.Serializer;
using Armadillo.Application.Discovery;
using Armadillo.Application.Navigation;
using Armadillo.Application.Vehicle;
using Armadillo.Core.Cache;
using Armadillo.Core.Contract;
using Armadillo.Core.Discovery;
using Armadillo.Core.Navigation;
using Armadillo.Core.Serializer;
using Armadillo.Core.Vehicle;
using Armadillo.Web.Core.Contract;
using Armadillo.Web.Core.Middleware;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Armadillo.Web
{
    public class Startup
    {
        private readonly AppSettings appSettings;
        private readonly IConfiguration configuration;

        public Startup(IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();

            this.configuration = builder.Build();

            appSettings = new AppSettings();
            var section = configuration.GetSection("AppSettings");
            new ConfigureFromConfigurationOptions<AppSettings>(section).Configure(appSettings);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            services.AddControllersWithViews();

            var assembly = Assembly.GetAssembly(typeof(Application.Handler.Command.Discovery.DiscoveryHandler));

            services.AddMediatR(assembly);

            services.AddSingleton(appSettings);
            services.AddSingleton<IAppSettings>(appSettings);

            services.AddTransient<ICacheManager, InMemoryCacheManager>();
            services.AddTransient<IJsonSerializer, JsonSerializer>();

            services.AddTransient<IDiscoveryAreaFactory, DiscoveryAreaFactory>();
            services.AddTransient<ISpaceVehicleFactory, SpaceVehicleFactory>();

            services.AddTransient<Navigator, EastNavigator>();
            services.AddTransient<Navigator, NorthNavigator>();
            services.AddTransient<Navigator, SouthNavigator>();
            services.AddTransient<Navigator, WestNavigator>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Armadillo API", Version = "v1" });
            });

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
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

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
