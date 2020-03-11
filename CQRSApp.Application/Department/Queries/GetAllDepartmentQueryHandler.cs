using AutoMapper;
using CQRSApp.Application.Queries.QueryModels;
using CQRSApp.Domain.Repositories;
using CQRSApp.Repositories.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSApp.Application.Queries.Department
{
    public class GetAllDepartmentQueryHandler<TRequest, TResponse> : IRequestHandler<GetAllDepartmentQuery, List<DepartmentQueryModel>>
    {
        private readonly IDepartmentRepository _repo;
        private readonly IMapper _mapper;

        public GetAllDepartmentQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _repo = uow.DepartmentRepositroy;
            _mapper = mapper;
        }
        public async Task<List<DepartmentQueryModel>> Handle(GetAllDepartmentQuery request, CancellationToken cancellationToken)
        {
            var departments = await _repo.GetAll();
            List<DepartmentQueryModel> departmentsQ = _mapper.Map<List<CQRSApp.Domain.Entites.Department>, List<DepartmentQueryModel>>(departments);

            return departmentsQ;
        }
    }
    public class GetAllDepartmentQuery: IRequest<List<DepartmentQueryModel>>
    {
        public int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;

        public int PageSize
        {
            get { return pageSize = 10; }
            set { pageSize = (value > MaxPageSize)?MaxPageSize:value; }
        }
        public GetAllDepartmentQuery()
        {
                
        }

    }
}
