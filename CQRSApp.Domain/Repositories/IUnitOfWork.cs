using CQRSApp.Domain;
using CQRSApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CQRSApp.Repositories.Domain
{
    public interface IUnitOfWork
    {
        ICourseRepository CourseRepositroy { get; set; }
        IDepartmentRepository DepartmentRepositroy { get; set; }

        Task<int> Commit();
        void RollBack();
    }
}
