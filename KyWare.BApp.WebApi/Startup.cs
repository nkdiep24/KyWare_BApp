using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using KyWare.BApp.WebApi.Databases;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using KyWare.BApp.Data;
using KyWare.BApp.WebApi.Commons;
using KyWare.BApp.WebApi.Services;

namespace KyWare.BApp.WebApi
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            AppConfig.Configuration = configuration;
            AppConfig.ConnectionString = AppConfig.Configuration.GetConnectionString("DefaultConnectionString");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Setup Controllers
            services.AddControllers();

            // Setup Db
            SetUpDbContext(services);

            // Add Transient
            services.AddTransient<BooksService>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "KyWare_BApp_WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "KyWare_BApp_WebApi v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Db Initialize
            AppDbInitializer.Seed(app);
        }

        #region Database Stuff
        public void SetUpDbContext(IServiceCollection services)
        {

            var dbProvider = AppConfig.GetCurrentDbProvider();
            switch (dbProvider)
            {
                case EnumDatabaseProvider.SqlServer:
                    // Setup Db Context
                    services.AddDbContext<AppDbContext>(options => options.UseSqlServer(AppConfig.ConnectionString));
                    break;
                case EnumDatabaseProvider.Postgresql:
                    // Setup Db Context
                    services.AddDbContext<AppDbContext>(options => options.UseNpgsql(AppConfig.ConnectionString));
                    break;
            }
        }
        #endregion
    }
}
