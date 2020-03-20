using CQRSApp.Domain.Entites;
using CQRSApp.Domain.Repositories;
using CQRSApp.Repositories.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSApp.Application.Commands
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, CQRSApp.Domain.Entites.Course>
    {
        private readonly ICourseRepository _repo;
        private readonly IUnitOfWork _uow;

        public CreateCourseCommandHandler(IUnitOfWork Uow)
        {
            _repo = Uow.CourseRepositroy;
            _uow = Uow;
        }

        public async Task<CQRSApp.Domain.Entites.Course> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            CQRSApp.Domain.Entites.Course course = new CQRSApp.Domain.Entites.Course(request.DepartmentId,request.Name,request.CreditHour,request.HasPreRequest);
            var newCourse = _repo.Add(course);

            await _uow.Commit();

            return newCourse;
        }
    }

    public class CreateCourseCommand: IRequest<CQRSApp.Domain.Entites.Course>
    {
        public Guid DepartmentId { get; set; }
        public string Name { get; set; }
        public int CreditHour { get; set; }
        public bool HasPreRequest { get; set; }

        public CreateCourseCommand(Guid departmentId, string name, int creditHour, bool hasPreRequest)
        {
            Name = name;
            CreditHour = creditHour;
            HasPreRequest = hasPreRequest;
            DepartmentId = departmentId;
        }
        public CreateCourseCommand()
        {

        }
    }
}
