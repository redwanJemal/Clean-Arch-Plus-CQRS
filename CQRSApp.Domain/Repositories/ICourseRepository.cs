using CQRSApp.Domain.Entites;
using CQRSApp.Repositories.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CQRSApp.Domain.Repositories
{
    public interface ICourseRepository: IGenericRepository<Course>
    {
        public Task<Course> GetById(Guid id);
    }
}
