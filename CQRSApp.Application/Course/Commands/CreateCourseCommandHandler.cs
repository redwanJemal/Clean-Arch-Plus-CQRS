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
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Course>
    {
        private readonly ICourseRepository _repo;
        private readonly IUnitOfWork _uow;

        public CreateCourseCommandHandler(ICourseRepository repo, IUnitOfWork Uow)
        {
            _repo = repo;
            _uow = Uow;
        }

        public async Task<Course> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            Course course = new Course(request.Name,request.CreditHour,request.HasPreRequest);
            var newCourse = _repo.Add(course);

            await _uow.Commit();

            return newCourse;
        }
    }

    public class CreateCourseCommand: IRequest<Course>
    {
        public string Name { get; set; }
        public int CreditHour { get; set; }
        public bool HasPreRequest { get; set; }

        public CreateCourseCommand(string name, int creditHour, bool hasPreRequest)
        {
            Name = name;
            CreditHour = creditHour;
            HasPreRequest = hasPreRequest;
        }
        public CreateCourseCommand()
        {

        }
    }
}
