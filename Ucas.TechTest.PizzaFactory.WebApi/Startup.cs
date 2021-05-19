using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Ucas.TechTest.PizzaFactory.Mongo;
using Ucas.TechTest.PizzaFactory.Mongo.Models;
using Ucas.TechTest.PizzaFactory.Restaurant;

namespace Ucas.TechTest.PizzaFactory.WebApi
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
            // requires using Microsoft.Extensions.Options
            services.Configure<PizzeriaDatabaseSettings>(
                Configuration.GetSection(nameof(PizzeriaDatabaseSettings)));

            services.AddSingleton<MongoDBService>();
            services.AddSingleton<IPizzeriaDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<PizzeriaDatabaseSettings>>().Value);

            services.AddSingleton<IOrderWriter>(sp =>
                sp.GetRequiredService<MongoDBService>());
            services.AddSingleton<IOrderReader>(sp =>
                sp.GetRequiredService<MongoDBService>());
            services.AddSingleton<IOrdersReader>(sp =>
                sp.GetRequiredService<MongoDBService>());
            services.AddSingleton<IPartyWriter>(sp =>
                sp.GetRequiredService<MongoDBService>());
            services.AddSingleton<IMenuReader>(sp =>
                sp.GetRequiredService<MongoDBService>());
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Ucas.TechTest.PizzaFactory.WebApi", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ucas.TechTest.PizzaFactory.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}