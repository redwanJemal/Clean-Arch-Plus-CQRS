using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CQRSApp.Application.Commands;
using CQRSApp.Application.Helpers;
using CQRSApp.Application.Queries.Department;
using CQRSApp.Application.Queries.QueryModels;
using CQRSApp.Domain.Entites;
using CQRSApp.Persistance;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace CQRSApp.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors(options =>
            {
                options.AddPolicy("TodoAppPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();

                });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CQRSAPP API", Version = "v1" });
            });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            services.AddSingleton<IMapper>(sp => config.CreateMapper());
            services.AddMediatR(typeof(Startup));
            services.AddTransient<IMediator, Mediator>();
            services.AddTransient(typeof(IRequestHandler<CreateCourseCommand, Course>), typeof(CreateCourseCommandHandler<CreateCourseCommand, Course>));
            services.AddTransient(typeof(IRequestHandler<GetDepartmentQuery, DepartmentQueryModel>), typeof(GetDepartmentQueryHandler<GetDepartmentQuery, DepartmentQueryModel>));
            services.AddTransient(typeof(IRequestHandler<CreateDepartmentCommand, DepartmentQueryModel>), typeof(CreateDepartmentCommandHandler<CreateDepartmentCommand, DepartmentQueryModel>));
            services.AddTransient(typeof(IRequestHandler<GetAllDepartmentQuery, List<DepartmentQueryModel>>), typeof(GetAllDepartmentQueryHandler<GetAllDepartmentQuery, List<DepartmentQueryModel>>));
            services.AddTransient(typeof(IRequestHandler<DeleteDepartmentCommand, bool>), typeof(DeleteDepartmentCommandHandler<DeleteDepartmentCommand, bool>));
            services.AddInfrastructure(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("TodoAppPolicy");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CQRSAPP API");
            });
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
