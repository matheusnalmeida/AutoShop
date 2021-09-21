using Autofac;
using AutoShop.Infra.Data;
using DDDWebAPI.Infrastruture.CrossCutting.IOC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop
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
            ConfigureCors(services);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AutoShop", Version = "v1" });
            });

            ConfigureDatabase(services);
        }

        public void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
                options.AddDefaultPolicy(policys => policys
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()));
        }

        public void ConfigureContainer(ContainerBuilder Builder)
        {
            #region Modulo IOC

            Builder.RegisterModule(new ModuleIOC());

            #endregion
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            var server = Configuration["DefaultConnectionString:DBServer"];
            var user = Configuration["DefaultConnectionString:DBUser"];
            var port = Configuration["DefaultConnectionString:DBPort"];
            var password = Configuration["DefaultConnectionString:DBPassword"];
            var database = Configuration["DefaultConnectionString:Database"];

            var connectionString = $"Host={server};Port={port};Database={database};Username={user};Password={password};SSL Mode=Require;Trust Server Certificate=true";

            services.AddDbContext<AutoShopContext>(options =>
                options.UseNpgsql(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AutoShopContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AutoShop v1"));
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseRouting();

            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            AutoShopContextInitializer.EnsureCreate(context);
        }
    }
}
