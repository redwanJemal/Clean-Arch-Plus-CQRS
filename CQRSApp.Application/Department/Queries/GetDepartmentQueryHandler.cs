using CQRSApp.Application.Queries.QueryModels;
using CQRSApp.Domain.Repositories;
using CQRSApp.Repositories.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSApp.Application.Queries.Department
{
    public class GetDepartmentQueryHandler<TRequest, TResponse> : IRequestHandler<GetDepartmentQuery, DepartmentQueryModel>
    {
        private readonly IDepartmentRepository _repo;

        public GetDepartmentQueryHandler(IUnitOfWork Uow)
        {
            _repo = Uow.DepartmentRepositroy;
        }

        public async Task<DepartmentQueryModel> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
        {
            var department = await _repo.GetById(request.Id);
            if (department == null)
                return null;
            return new DepartmentQueryModel
            {
                Id = department.Id,
                Name = department.Name,
                SchoolName = department.SchoolName
            };
        }
    }
    public class GetDepartmentQuery : IRequest<DepartmentQueryModel>
    {
        public Guid Id { get; set; }
        public GetDepartmentQuery(Guid id)
        {
            Id = id;
        }

    }
}
