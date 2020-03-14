using System;
using System.Reflection;

using Autofac;
using MediatR;

using Microsoft.EntityFrameworkCore;
using CQRSApp.Persistance;
using CQRSApp.Repositories.Domain;
using CQRSApp.Domain.Repositories;

namespace CQRSApp.Api.Infrastructure.AutofaceModule
{
    public class CQRSAppApplicationModule: Autofac.Module
    {
        public string QueriesConnectionString { get; }

        public CQRSAppApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;
        }

        protected override void Load(ContainerBuilder builder)
        {
            RegisterTypes(builder);

            // TODO: Use one of given commands as a base
            builder.RegisterAssemblyTypes(typeof(CQRSApp.Application.Commands.CreateCourseCommandHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));


        }
        
        private void RegisterTypes(ContainerBuilder builder)
        {

            builder.RegisterType<CQRSAppDBContext>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DepartmentRepository>()
                .As<IDepartmentRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CourseRepository>()
                .As<ICourseRepository>()
                .InstancePerLifetimeScope();

        }
    }
}