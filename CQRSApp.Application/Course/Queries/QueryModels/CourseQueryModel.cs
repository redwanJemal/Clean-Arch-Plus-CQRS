using System;
using System.Collections.Generic;
using System.Text;

namespace CQRSApp.Application.Queries.QueryModels
{
    public class CourseQueryModel
    {
        public Guid Id { get; set; }
        public Guid DepartmentId { get; set; }
        public string Name { get; set; }
        public int CreaditHoure { get; set; }
        public bool HasPreRequest { get; set; }
    }
}
