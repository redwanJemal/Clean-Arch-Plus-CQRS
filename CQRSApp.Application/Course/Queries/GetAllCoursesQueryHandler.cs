using CQRSApp.Application.Helpers;
using CQRSApp.Application.Queries.QueryModels;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSApp.Application.Course.Queries
{
    public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, PagedResults<CourseQueryModel>>
    {
        public async Task<PagedResults<CourseQueryModel>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var results = new PagedResults<CourseQueryModel>();
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=CQRSApp;Trusted_Connection=True;MultipleActiveResultSets=true;";
            var query = @"SELECT COUNT(*) FROM Courses
                        SELECT * from Courses ORDER BY Id OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Do some magic here
                connection.Open();
                using (var multi = await connection.QueryMultipleAsync(query, new
                {
                    Offset = (request.PageNumber - 1) * request.PageSize,
                    PageSize = request.PageSize
                }))
                {
                    results.TotalCount = multi.Read<int>().Single();
                    results.Items = multi.Read<CourseQueryModel>().ToList();
                }
            }
            return results;
        }
    }


    public class GetAllCoursesQuery : IRequest<PagedResults<CourseQueryModel>>
    {
        public int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        public GetAllCoursesQuery()
        {

        }
        public GetAllCoursesQuery(int pageSize, int pageNumber)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }

    }
}
