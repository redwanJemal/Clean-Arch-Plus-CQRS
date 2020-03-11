using CQRSApp.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CQRSApp.Domain.Repositories
{
    public interface ICourseRepository
    {
        public Task<Course> GetById(Guid id);
    }
}
