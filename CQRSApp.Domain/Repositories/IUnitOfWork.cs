using CQRSApp.Domain;
using CQRSApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRSApp.Repositories.Domain
{
    public interface IUnitOfWork
    {
        ICourseRepository CourseRepositroy { get; set; }
        IDepartmentRepository DepartmentRepositroy { get; set; }

        void Commit();
        void RollBack();
    }
}
