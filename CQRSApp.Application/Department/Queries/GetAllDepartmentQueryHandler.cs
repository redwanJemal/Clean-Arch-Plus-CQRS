using AutoMapper;
using CQRSApp.Application.Queries.QueryModels;
using CQRSApp.Domain.Repositories;
using CQRSApp.Repositories.Domain;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
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
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=CQRSApp;Trusted_Connection=True;MultipleActiveResultSets=true;";
            var query = "SELECT * from Departments ORDER BY Id OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Do some magic here
                connection.Open();
                var department = await connection.QueryAsync<DepartmentQueryModel>(query, new { Offset = (request.PageNumber - 1) * request.PageSize, PageSize = request.PageSize});
                return department.ToList();
            }
        }
    }
    public class GetAllDepartmentQuery: IRequest<List<DepartmentQueryModel>>
    {
        public int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize)?MaxPageSize:value; }
        }
        public GetAllDepartmentQuery()
        {
                
        }

    }
}
