using AutoMapper;
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
        private readonly IMapper _mapper;

        public UpdateDepartmentCommandHandler(IUnitOfWork Uow, IMapper mapper)
        {
            _repo = Uow.DepartmentRepositroy;
            _uow = Uow;
            _mapper = mapper;
        }

        public async Task<DepartmentQueryModel> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {

            DepartmentQueryModel departmentQ = new DepartmentQueryModel(request.Id, request.Name, request.SchoolName);

            if (request.Id != null)
            {
                try
                {
                    Department department = _mapper.Map<Department>(departmentQ);
                    _repo.Update(department);

                    await _uow.Commit();
                    return departmentQ;
                }
                catch (Exception)
                {
                    _uow.RollBack();
                    return null;
                }
            }
            return null;

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
