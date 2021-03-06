﻿using AutoMapper;
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
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, DepartmentUpdateQueryModel>
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

        public async Task<DepartmentUpdateQueryModel> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {

            DepartmentUpdateQueryModel departmentQ = new DepartmentUpdateQueryModel(request.Id, request.Name);

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

    public class UpdateDepartmentCommand: IRequest<DepartmentUpdateQueryModel>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public UpdateDepartmentCommand(Guid id, string name, string schoolName)
        {
            Id = id;
            Name = name;
        }
        public UpdateDepartmentCommand()
        {

        }
    }
}
