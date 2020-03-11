using CQRSApp.Domain;
using CQRSApp.Domain.Entites;
using CQRSApp.Domain.Repositories;
using CQRSApp.Persistance.Repositories;
using CQRSApp.Repositories.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CQRSApp.Persistance
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        private DbSet<Course> _courses;
        public CourseRepository(CQRSAppDBContext context): base(context)
        {

        }
        public async Task<Course> GetById(Guid id)
        {
            var course = await _courses.FirstOrDefaultAsync(c => c.Id == id);
            return course;
        }
    }
}
