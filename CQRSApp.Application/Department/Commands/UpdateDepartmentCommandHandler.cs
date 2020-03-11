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
    public class UpdateDepartmentCommandHandler<TRequest,TResponse> : IRequestHandler<UpdateDepartmentCommand, DepartmentQueryModel>
    {
        private readonly IDepartmentRepository _repo;
        private readonly IUnitOfWork _uow;

        public UpdateDepartmentCommandHandler(IUnitOfWork Uow)
        {
            _repo = Uow.DepartmentRepositroy;
            _uow = Uow;
        }

        public async Task<DepartmentQueryModel> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            Department department = new Department(request.Name,request.SchoolName);
            var newDepartment = _repo.Add(department);

            DepartmentQueryModel departmentQ = new DepartmentQueryModel(newDepartment.Id, newDepartment.Name, newDepartment.SchoolName);

            await _uow.Commit();

            return departmentQ;
        }
    }

    public class UpdateDepartmentCommand: IRequest<DepartmentQueryModel>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SchoolName { get; set; }

        public UpdateDepartmentCommand(Guid id, string name, string schoolName)
        {
            Id = id;
            Name = name;
            SchoolName = schoolName;
        }
        public UpdateDepartmentCommand()
        {

        }
    }
}
