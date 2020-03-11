using CQRSApp.Domain.Entites;
using CQRSApp.Repositories.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace CQRSApp.Domain.Repositories
{
    public interface IDepartmentRepository: IGenericRepository<Department>
    {
        public Task<List<Department>> GetAll();
        public Task<Department> GetById(Guid id);
    }
}
