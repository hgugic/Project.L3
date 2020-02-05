using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Project.DAL;
using Project.Repository;
using Project.Repository.Common;
using Project.Service;
using Project.Service.Common;
using AutoMapper;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Project.WebAPI.Helpers;
using Microsoft.AspNetCore.Http;

namespace Project.WebAPI
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
            services.AddCors();
            services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();

            services.AddAutoMapper(typeof(Mappings.MappingProfile).Assembly, 
                                   typeof(Project.Repository.VehicleMakeRepository).Assembly);

            services.AddScoped<IVehicleMakeService, VehicleMakeService>();
            services.AddScoped<IVehicleMakeRepository, VehicleMakeRepository>();

            services.AddScoped<IVehicleModelService, VehicleModelService>();
            services.AddScoped<IVehicleModelRepository, VehicleModelRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder => {
                    builder.Run(async httpContext => {
                        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = httpContext.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            httpContext.Response.AddApplicationError(error.Error.Message);
                            await httpContext.Response.WriteAsync(error.Error.Message);
                        }

                    });
                });
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
