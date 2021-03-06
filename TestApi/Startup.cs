using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApi.Controllers;
using TestApi.Middlewares;

namespace TestApi
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
            services.AddSingleton<SingletonService>();
            services.AddScoped<ScopedService>();
            services.AddTransient<TransientService>();

            services.AddSingleton<BestService, BestService>();
            services.AddSingleton<BestService>();
            services.AddTransient<BestService>();

            services.AddSingleton<IBehavior, Multi>();
            services.AddSingleton<IBehavior, Multi2>();

            services.AddSingleton<IBehavior, SameMultiAsClass>();
            services.AddTransient<string>(serviceProvider =>
            {
                //var behavior = serviceProvider.GetService<IBehavior>();
                return Guid.NewGuid().ToString();
            });
            services.AddTransient<Manager>();

            services.AddSingleton<MyBestMiddleware>();
            services.AddSingleton<DeveloperExceptionPageMiddleware>();
            services.AddSingleton<FastAuthMiddleware>();
            services.AddSingleton<WeatherForecastController>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestApi", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMiddleware<DeveloperExceptionPageMiddleware>();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestApi v1"));
            }
            
            app.UseHttpsRedirection();

            app.UseMiddleware<MyBestMiddleware>();
            //app.UseMiddleware<FastAuthMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
