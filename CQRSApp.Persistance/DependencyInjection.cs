using CQRSApp.Domain.Repositories;
using CQRSApp.Repositories.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRSApp.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, CQRSAppDBContext>();

            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddDbContext<CQRSAppDBContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("CQRSAppDatabase")));

            return services;
        }
    }
}
