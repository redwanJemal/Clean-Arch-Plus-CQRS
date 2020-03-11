using CQRSApp.Domain;
using CQRSApp.Domain.Entites;
using CQRSApp.Domain.Repositories;
using CQRSApp.Persistance.Repositories;
using CQRSApp.Repositories.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace CQRSApp.Persistance
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private DbSet<Department> departments;
        public DepartmentRepository(CQRSAppDBContext context): base(context)
        {
            departments = context.Departments;
        }
        public async Task<Department> GetById(Guid id)
        {
            var course = await departments.FirstOrDefaultAsync(c => c.Id == id);
            return course;
        }
        public async Task<List<Department>> GetAll()
        {
            var depar = await departments.ToListAsync();
            return depar;
        }
    }
}
