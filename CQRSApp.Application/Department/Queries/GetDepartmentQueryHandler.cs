using AutoMapper;
using CQRSApp.Application.Queries.QueryModels;
using CQRSApp.Domain.Repositories;
using CQRSApp.Repositories.Domain;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSApp.Application.Queries.Department
{
    public class GetDepartmentQueryHandler<TRequest, TResponse> : IRequestHandler<GetDepartmentQuery, DepartmentQueryModel>
    {
        private readonly IDepartmentRepository _repo;
        private readonly IMapper _mapper;

        public GetDepartmentQueryHandler(IUnitOfWork Uow, IMapper mapper)
        {
            _repo = Uow.DepartmentRepositroy;
            _mapper = mapper;
        }

        public async Task<DepartmentQueryModel> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
        {
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=CQRSApp;Trusted_Connection=True;MultipleActiveResultSets=true;";
            var query = "SELECT * from Departments WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Do some magic here
                connection.Open();
                var department = await connection.QueryFirstOrDefaultAsync<DepartmentQueryModel>(query, new { Id = request.Id });
                return department;
            }
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
