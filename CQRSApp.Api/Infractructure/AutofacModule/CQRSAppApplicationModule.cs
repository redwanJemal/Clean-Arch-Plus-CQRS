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

            //builder.Register(c =>
            //{
            //    var config = c.Resolve<Microsoft.Extensions.Configuration.IConfiguration>();
            //    var mediator = c.Resolve<IMediator>();

            //    var options = new DbContextOptionsBuilder<CQRSAppDBContext>();

            //    options.UseSqlServer(QueriesConnectionString,
            //        options: pgOptions =>
            //        {
            //            pgOptions.MigrationsAssembly(typeof(ProductCatalogContext).GetTypeInfo().Assembly.GetName().Name);
            //            pgOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
            //        });


            //    return new CQRSAppDBContext(options.Options, mediator);
            //}).InstancePerLifetimeScope();

            //builder.RegisterType<ProductCatalogRequestManager>()
            //   .As<IRequestManager>()
            //   .InstancePerLifetimeScope();

            // TODO: Use one of given commands as a base
            builder.RegisterAssemblyTypes(typeof(CQRSApp.Application.Commands.CreateCourseCommandHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));


        }
        
        private void RegisterTypes(ContainerBuilder builder)
        {

            //builder.Register(c => new Cataloging.ProductCatalog.Application.Queries.CatalogQueries.CatalogQueryPG(QueriesConnectionString))
            //    .As<Cataloging.ProductCatalog.Application.Queries.CatalogQueries.ICatalogQuery>()
            //    .InstancePerLifetimeScope();

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