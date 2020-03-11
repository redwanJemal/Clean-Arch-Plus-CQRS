using CQRSApp.Domain;
using CQRSApp.Domain.Entites;
using CQRSApp.Domain.Repositories;
using CQRSApp.Repositories.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;

namespace CQRSApp.Persistance
{
    public class CQRSAppDBContext : DbContext,IUnitOfWork
    {
        public DbSet<Course> Courses { get; set; }
        public ICourseRepository CourseRepositroy { get; set; }
        public DbSet<Department> Departments { get; set; }
        public IDepartmentRepository DepartmentRepositroy { get; set; }

        CQRSAppDBContext context;
        public CQRSAppDBContext(DbContextOptions<CQRSAppDBContext> options): base(options)
        {
            context = this;
            CourseRepositroy = new CourseRepository(context);
        }
        public void Commit()
        {
            this.SaveChanges();
        }

        public void RollBack()
        {
            context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }
    }

    public class ETLContextDesignFactory : IDesignTimeDbContextFactory<CQRSAppDBContext>
    {
        public CQRSAppDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CQRSAppDBContext>()
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=CQRSApp;Integrated Security=True");
            return new CQRSAppDBContext(optionsBuilder.Options);
        }
    }
}
