using AutoMapper;
using CQRSApp.Application.Helpers;
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
    public class GetAllDepartmentQueryHandler : IRequestHandler<GetAllDepartmentQuery, PagedResults<DepartmentQueryModel>>
    {

        public GetAllDepartmentQueryHandler()
        {
        }
        public async Task<PagedResults<DepartmentQueryModel>> Handle(GetAllDepartmentQuery request, CancellationToken cancellationToken)
        {
            var results = new PagedResults<DepartmentQueryModel>();
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=CQRSApp;Trusted_Connection=True;MultipleActiveResultSets=true;";
            var query = @"SELECT COUNT(*) FROM Departments
                        SELECT * from Departments ORDER BY Id OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Do some magic here
                connection.Open();
                using (var multi = await connection.QueryMultipleAsync(query, new {
                    Offset = (request.PageNumber - 1) * request.PageSize, PageSize = request.PageSize
                }))
                {
                    results.TotalCount = multi.Read<int>().Single();
                    results.Items = multi.Read<DepartmentQueryModel>().ToList();
                }
            }
            return results;

        }
    }
    public class GetAllDepartmentQuery: IRequest<PagedResults<DepartmentQueryModel>>
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
        public GetAllDepartmentQuery(int pageSize, int pageNumber)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }

    }
}
