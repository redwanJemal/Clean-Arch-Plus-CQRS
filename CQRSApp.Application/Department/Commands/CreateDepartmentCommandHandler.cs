using CQRSApp.Application.Queries.QueryModels;
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
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, DepartmentQueryModel>
    {
        private readonly IDepartmentRepository _repo;
        private readonly IUnitOfWork _uow;

        public CreateDepartmentCommandHandler(IUnitOfWork Uow)
        {
            _repo = Uow.DepartmentRepositroy;
            _uow = Uow;
        }

        public async Task<DepartmentQueryModel> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            Department department = new Department(request.Name,request.SchoolName);
            var newDepartment = _repo.Add(department);

            DepartmentQueryModel departmentQ = new DepartmentQueryModel(newDepartment.Id, newDepartment.Name, newDepartment.SchoolName);

            await _uow.Commit();

            return departmentQ;
        }
    }

    public class CreateDepartmentCommand: IRequest<DepartmentQueryModel>
    {
        public string Name { get; set; }
        public string SchoolName { get; set; }

        public CreateDepartmentCommand(string name, string schoolName)
        {
            Name = name;
            SchoolName = schoolName;
        }
        public CreateDepartmentCommand()
        {

        }
    }
}
